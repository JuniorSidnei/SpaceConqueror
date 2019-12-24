using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    public static EventHandler Instance;

    private EventManager m_event;
    
    private void Awake()
    {
        Instance = this;
        
        if (m_event == null)
        {
            m_event = GetComponent<EventManager>();
        }
    }

    public void CallDialogue(SpeechScriptable speech)
    {
        GameManager.Instance.m_isDialogueActive = true;
        HudManager.Instance.HandleConversation();
        DialogueManager.Instance.StartSpeech(speech, () =>
        {
            GameManager.Instance.m_isDialogueActive = false;
        }); 
    }

}
