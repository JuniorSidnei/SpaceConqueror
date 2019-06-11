using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    public static EventHandler Instance;

    private EventManager m_event;

    public int m_current;
    
    public List<SpeechScriptable> m_speechs;

    private void Awake()
    {
        Instance = this;
        
        if (m_event == null)
        {
            m_event = GetComponent<EventManager>();
        }
    }

    public void CallDialogueAndEvent()
    {
        HudManager.Instance.HandleConversation();
        DialogueManager.Instance.StartSpeech(m_speechs[m_current], 1.5f, () =>
        {
            m_event.onDialogueFinish[m_current].Invoke();
            m_current++;
        }); 
    }

}
