using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Messages
{
    public string m_name;
    [TextArea(2,5)]
    public List<string> m_myMessages;
}


public class LogMessageController : MonoBehaviour
{
    public enum MessageType
    {
        DamageTaken = 0, Recovery = 1, RecoveryOut = 2
    }
    
    public static LogMessageController Instance;

    public List<Messages> m_OnMessages;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        
    }
    
    public string GetLogMessage(MessageType type)
    {
        return m_OnMessages[type.GetHashCode()].m_myMessages[UnityEngine.Random.Range(0, m_OnMessages[type.GetHashCode()].m_myMessages.Count - 1)];
    }
}



