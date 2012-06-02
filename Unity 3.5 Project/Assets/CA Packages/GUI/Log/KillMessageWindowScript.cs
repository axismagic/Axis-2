using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KillMessageWindowScript : MonoBehaviour
{
    public float DefaultMessageFadeoutTimeout = 1.0f;
    public float DefaultMessageTimeout = 3.0f;
    public int MaxNumberOfMessages = 10;

    private List<LogWindowScript.LogWindowMsg> LogMessages = new List<LogWindowScript.LogWindowMsg>();

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public List<LogWindowScript.LogWindowMsg> GetMessages()
    {
        return LogMessages;
    }
	
	// Update is called once per frame
    void Update()
    {
        // Check the messages for delete
        for (int n = LogMessages.Count - 1; n >= 0; n--)
        {
            LogMessages[n].update();

            if (LogMessages[n].MarkForDelete)
                LogMessages.Remove(LogMessages[n]);
        }
    }

    public void AddMessage(string Message)
    {
        if (LogMessages.Count == MaxNumberOfMessages)
            LogMessages.RemoveAt(0);

        LogWindowScript.LogWindowMsg Msg = new LogWindowScript.LogWindowMsg();
        Msg.MarkForDelete = false;
        Msg.Message = Message;
        Msg.Timeout = DefaultMessageTimeout;
        Msg.FadeoutTime = DefaultMessageFadeoutTimeout;

        LogMessages.Insert(0, Msg);
    }
}
