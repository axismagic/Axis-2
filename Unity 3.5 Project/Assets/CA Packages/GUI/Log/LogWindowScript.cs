using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogWindowScript : MonoBehaviour
{
    public class LogWindowMsg
    {
        public string Message = "";
        public float Timeout = 10.0f;
        public float FadeoutTime = 5.0f;
        public float CurrentTimer = 0.0f;
        public bool MarkForDelete = false;

        public void update()
        {
            CurrentTimer += Time.deltaTime;

            if (CurrentTimer > Timeout + FadeoutTime)
                MarkForDelete = true;
        }
    }

    public float DefaultMessageFadeoutTimeout = 1.0f;
    public float DefaultMessageTimeout = 3.0f;
    public int MaxNumberOfMessages = 10;

    private Rect LogRect;

    private List<LogWindowMsg> LogMessages = new List<LogWindowMsg>();

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
	}

    public List<LogWindowScript.LogWindowMsg> GetMessages()
    {
        return LogMessages;
    }
	
	// Update is called once per frame
	void Update () {
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

        LogWindowMsg Msg = new LogWindowMsg();
        Msg.MarkForDelete = false;
        Msg.Message = Message;
        Msg.Timeout = DefaultMessageTimeout;
        Msg.FadeoutTime = DefaultMessageFadeoutTimeout;

        LogMessages.Insert(0, Msg);
    }
}
