using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Messages
{
    [TextArea(4,10)]
    public string message;
}


public class LogMessageController : MonoBehaviour
{

    public static LogMessageController Instance;

    public List<Messages> m_OnHitMessages;
    public List<Messages> m_OnRecoveryMesasges;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

//    public string GetLogDamage(int log)
//    {
//        Random rdn = new Random();
//        var index = rdn.Next(0, m_OnHitMessages.Count);
//        
//        switch (log)
//        {
//            case 1:
//                return m_OnHitMessages[index].message;
//            case 2:
//                return m_OnRecoveryMesasges[index].message;
//        }
//    }
    public string GetLogDamageMessage()
    {
        return m_OnHitMessages[UnityEngine.Random.Range(0,m_OnHitMessages.Count)].message;
    }
    
    public string GetLogRecoveryMessage()
    {
        return m_OnRecoveryMesasges[UnityEngine.Random.Range(0,m_OnRecoveryMesasges.Count)].message;
    }
}



