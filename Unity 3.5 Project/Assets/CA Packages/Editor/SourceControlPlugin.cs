using UnityEditor;
using UnityEngine;

using System;
using System.IO; 
using System.Collections.Generic;
using System.Threading;

public class SourceControlPlugin : EditorWindow {
	public string Server 		= "Change Me!";
	public string User 			= "Change Me!";
	public string Workspace 	= "Change Me!";
	
    [MenuItem ("Perforce/Show Settings")]
	static void ShowSettings() {
		EditorWindow.GetWindow(typeof(SourceControlPlugin));
	}
	
	private FileSystemWatcher m_SceneFileWatcher;			// This watches to see if we have saved the level
	private FileSystemWatcher m_SceneDirectoryWatcher;  // This watches to see if we have altered anything with the asset directory
	static private Dictionary<String, byte> CreateList = new Dictionary<String, byte>(); // Use a dictionary, faster search times.
	static private Dictionary<String, byte> RemoveList = new Dictionary<String, byte>(); // Use a dictionary, faster search times.
	
	void OnSceneFileWatcher_Changed(object sender, FileSystemEventArgs e)
	{				
		if(CreateList.Count != 0 || RemoveList.Count != 0) {		
			
			List<String> CreateFiles = new List<String>();
			foreach(KeyValuePair<String, byte> kvp in CreateList)
			{
				CreateFiles.Add(kvp.Key);
			}
			if(CreateFiles.Count != 0)
				MakeAndExecAddCommand(CreateFiles);
			
			List<String> RemoveFiles = new List<String>();
			foreach(KeyValuePair<string, byte> kvp in RemoveList)
			{
				RemoveFiles.Add(kvp.Key);
			}
			if(RemoveFiles.Count != 0) {
				// Revert and remove.
				MakeAndExecRevertCommand(RemoveFiles);
				MakeAndExecRemoveCommand(RemoveFiles);
			}
		}
	
		// Clear our lists.
		CreateList.Clear();
		RemoveList.Clear();
	}
	
	void OnSceneDirWatcher_Created(object sender, FileSystemEventArgs e)
	{		
		// Check if this is in the delete list.
		if(RemoveList.ContainsKey(e.FullPath)) {
			RemoveList.Remove(e.FullPath);
		}
		
		CreateList.Add(e.FullPath, 0);
	}
	void OnSceneDirWatcher_Deleted(object sender, FileSystemEventArgs e)
	{		
		// If this is in the add list, remove it, we don't need to mark it for delete.
		if(CreateList.ContainsKey(e.FullPath)) {
			CreateList.Remove(e.FullPath);
			
			return;
		}
		
		RemoveList.Add(e.FullPath, 0);
	}
	
	// Use this for initialization
	void OnEnable () {		
		m_SceneDirectoryWatcher = new FileSystemWatcher(Path.GetFullPath("Assets"));
		m_SceneDirectoryWatcher.Filter = "*.*";
		m_SceneDirectoryWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
		m_SceneDirectoryWatcher.EnableRaisingEvents = true;
		m_SceneDirectoryWatcher.IncludeSubdirectories = true;
		
		m_SceneDirectoryWatcher.Created += new FileSystemEventHandler(OnSceneDirWatcher_Created);
		m_SceneDirectoryWatcher.Deleted += new FileSystemEventHandler(OnSceneDirWatcher_Deleted);
		
		m_SceneFileWatcher = new FileSystemWatcher(Path.GetFullPath("Assets"), "*.unity");
		m_SceneFileWatcher.Filter = "*.*";
		m_SceneFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
		m_SceneFileWatcher.EnableRaisingEvents = true;
		
		m_SceneFileWatcher.Changed += new FileSystemEventHandler(OnSceneFileWatcher_Changed);
	}
	
	void OnFocus() {
		Server 			= EditorPrefs.GetString("Server");
		User 				= EditorPrefs.GetString("User");
		Workspace 		= EditorPrefs.GetString("Workspace");
	}
	
	void OnLostFocus() {
		EditorPrefs.SetString("Server", Server);
		EditorPrefs.SetString("User", User);
		EditorPrefs.SetString("Workspace", Workspace);
	}
	
	void OnDestroy() {
		EditorPrefs.SetString("Server", Server);
		EditorPrefs.SetString("User", User);
		EditorPrefs.SetString("Workspace", Workspace);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
        GUILayout.Label ("Settings", EditorStyles.boldLabel);
		Server			= EditorGUILayout.TextField ("Server",  		Server);
		User 				= EditorGUILayout.TextField ("User",  			User);
		Workspace 		= EditorGUILayout.TextField ("Workspace",  	Workspace);
    }
	
	string BuildCommand() {		
		string Command = "p4";
		
		Command += (" -p " + Server);
		Command += (" -u " + User);
		Command += (" -c " + Workspace);
		
		return Command;
	}

    [MenuItem ("Assets/Get Latest Revision")]
	static void Sync() {
		SourceControlPlugin thisWindow = EditorWindow.GetWindow(typeof(SourceControlPlugin)) as SourceControlPlugin;
		
		List<String> Filenames = new List<String>();
		
		Debug.Log("//ProjectAxis/UnityProjectFolder/");
		
		foreach(UnityEngine.Object Obj in Selection.objects) {
			String sFullPath = "//ProjectAxis/UnityProjectFolder/";
			String sMetaFullPath = sFullPath;
			
			String sFilename = "";
			sFilename = AssetDatabase.GetAssetPath(Obj);
			sFullPath += sFilename;
						
			sMetaFullPath = sFullPath + ".meta";
			
			Filenames.Add(sFullPath);
			Filenames.Add(sMetaFullPath);
		}
		
		thisWindow.MakeAndExecGetLatestCommand(Filenames);
	}

    [MenuItem ("Assets/Check Out")]
	static void CheckOut() {
		SourceControlPlugin thisWindow = EditorWindow.GetWindow(typeof(SourceControlPlugin)) as SourceControlPlugin;
		
		List<String> Filenames = new List<String>();
		
		foreach(UnityEngine.Object Obj in Selection.objects) {
			String sFullPath = "//ProjectAxis/UnityProjectFolder/";
			String sMetaFullPath = sFullPath;
			
			String sFilename = "";
			sFilename = AssetDatabase.GetAssetPath(Obj);
			sFullPath += sFilename;
						
			sMetaFullPath = sFullPath + ".meta";
			
			Filenames.Add(sFullPath);
			Filenames.Add(sMetaFullPath);
		}
		
		thisWindow.MakeAndExecCheckOutCommand(Filenames);
	}
	
	[MenuItem ("Assets/Mark For Add")]
	static void Add() {
		SourceControlPlugin thisWindow = EditorWindow.GetWindow(typeof(SourceControlPlugin)) as SourceControlPlugin;
		
		List<String> Filenames = new List<String>();
		
		foreach(UnityEngine.Object Obj in Selection.objects) {
			String sFullPath = "//ProjectAxis/UnityProjectFolder/";
			String sMetaFullPath = sFullPath;
			
			String sFilename = "";
			sFilename = AssetDatabase.GetAssetPath(Obj);
			sFullPath += sFilename;
						
			sMetaFullPath = sFullPath + ".meta";
			
			Filenames.Add(sFullPath);
			Filenames.Add(sMetaFullPath);
		}
		
		thisWindow.MakeAndExecAddCommand(Filenames);
	}
	
	[MenuItem ("Assets/Mark For Delete")]
	static void Delete() {
		SourceControlPlugin thisWindow = EditorWindow.GetWindow(typeof(SourceControlPlugin)) as SourceControlPlugin;
		
		List<String> Filenames = new List<String>();
		
		foreach(UnityEngine.Object Obj in Selection.objects) {
			String sFullPath = "//ProjectAxis/UnityProjectFolder/";
			String sMetaFullPath = sFullPath;
			
			String sFilename = "";
			sFilename = AssetDatabase.GetAssetPath(Obj);
			sFullPath += sFilename;
						
			sMetaFullPath = sFullPath + ".meta";
			
			Filenames.Add(sFullPath);
			Filenames.Add(sMetaFullPath);
		}
		
		thisWindow.MakeAndExecRemoveCommand(Filenames);
	}


    [MenuItem ("Assets/Lock")]
	static void Lock() {
		SourceControlPlugin thisWindow = EditorWindow.GetWindow(typeof(SourceControlPlugin)) as SourceControlPlugin;
		
		// We must check out before locking
		CheckOut();
		
		string Command = thisWindow.BuildCommand();
		
		Command += " lock ";
		
		foreach(UnityEngine.Object Obj in Selection.objects) {
			String sFullPath = "//ProjectAxis/UnityProjectFolder/";
			String sMetaFullPath = sFullPath;
			
			String sFilename = "";
			sFilename = AssetDatabase.GetAssetPath(Obj);
			sFullPath += sFilename;
						
			sMetaFullPath = sFullPath + ".meta";
			
			try {
				if(Path.GetExtension(sFullPath) == "" ) {
					sFullPath += "/...";
				}
				
				Command += "\"" + sFullPath + "\" ";
				Command += "\"" + sMetaFullPath + "\" ";
			}
			catch (System.Exception objException) {
				Debug.Log(objException);
			}
		}
		
		thisWindow.ExecuteCommandAsync(Command);
	}
	
	
	public void MakeAndExecGetLatestCommand(List<String> Filenames) {
		
		string Command = BuildCommand();
		Command += " sync ";
		
		foreach(String File in Filenames) {			
			try {
				String FinalFile = File;
				if(Path.GetExtension(FinalFile) == "" ) {
					FinalFile += "/...";
				}
				
				Command += "\"" + FinalFile + "\"#head ";
			}
			catch (System.Exception objException) {
				Debug.Log(objException);
			}
		}
		
		ExecuteCommandAsync(Command);
	}
	
	public void MakeAndExecCheckOutCommand(List<String> Filenames) {
		
		string Command = BuildCommand();
		
		Command += " edit ";
		
		Command += "-c default ";
		
		foreach(String File in Filenames) {			
			try {
				String FinalFile = File;
				if(Path.GetExtension(FinalFile) == "" ) {
					FinalFile += "/...";
				}
				
				Command += "\"" + FinalFile + "\" ";
			}
			catch (System.Exception objException) {
				Debug.Log(objException);
			}
		}
		
		ExecuteCommandAsync(Command);
	}
	
	public void MakeAndExecAddCommand(List<String> Filenames) {
		
		string Command = BuildCommand();
		
		Command += " add ";
		
		Command += "-c default ";
		
		foreach(String File in Filenames) {			
			try {
				String FinalFile = File;				
				Command += "\"" + FinalFile + "\" ";
			}
			catch (System.Exception objException) {
				Debug.Log(objException);
			}
		}
		
		ExecuteCommandAsync(Command);
	}
		
	public void MakeAndExecRemoveCommand(List<String> Filenames) {
		
		string Command = BuildCommand();
		
		Command += " delete ";
		
		Command += "-c default ";
		
		foreach(String File in Filenames) {			
			try {
				String FinalFile = File;				
				if(Path.GetExtension(FinalFile) == "" ) {
					FinalFile += "/...";
				}
				Command += "\"" + FinalFile + "\" ";
			}
			catch (System.Exception objException) {
				Debug.Log(objException);
			}
		}
		
		ExecuteCommandAsync(Command);
	}
		
	public void MakeAndExecRevertCommand(List<String> Filenames) {
		
		string Command = BuildCommand();
		
		Command += " revert ";
		
		Command += "-c default ";
		
		foreach(String File in Filenames) {			
			try {
				String FinalFile = File;				
				if(Path.GetExtension(FinalFile) == "" ) {
					FinalFile += "/...";
				}
				Command += "\"" + FinalFile + "\" ";
			}
			catch (System.Exception objException) {
				Debug.Log(objException);
			}
		}
		
		ExecuteCommandAsync(Command);
	}
	
	public void ExecuteCommandSync(object command)
	{
		try
		{
			// create the ProcessStartInfo using "cmd" as the program to be run,
			// and "/c " as the parameters.
			// Incidentally, /c tells cmd that we want it to execute the command that follows,
			// and then exit.
			System.Diagnostics.ProcessStartInfo procStartInfo =
			new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

			// The following commands are needed to redirect the standard output.
			// This means that it will be redirected to the Process.StandardOutput StreamReader.
			procStartInfo.RedirectStandardOutput = true;
			procStartInfo.UseShellExecute = false;
			
			// Do not create the black window.
			procStartInfo.CreateNoWindow = true;
			
			// Now we create a process, assign its ProcessStartInfo and start it
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.StartInfo = procStartInfo;
			proc.Start();
			
			// Get the output into a string
			string result = proc.StandardOutput.ReadToEnd();
			
			if(result.Length != 0)
				Debug.Log(result);
		}
		catch (System.Exception objException)
		{
			Debug.Log(objException);
		}
	}
	
	public void ExecuteCommandAsync(string command)
	{	
		Debug.Log(command);
		try
		{
			//Asynchronously start the Thread to process the Execute command request.
			Thread objThread = new Thread(new ParameterizedThreadStart(ExecuteCommandSync));
			//Make the thread as background thread.
			objThread.IsBackground = true;
			//Set the Priority of the thread.
			objThread.Priority = System.Threading.ThreadPriority.AboveNormal;
			//Start the thread.
			objThread.Start(command);
	   }
	   catch (System.Exception objException)
	   {
			Debug.Log(objException);
	   }
	}
} 
