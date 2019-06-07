using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public int m_currentEvent;
    
    public List<MetaEvent> onDialogueFinish;
    
    [System.Serializable]
    public class MetaEvent
    {
        public string name;
        public UnityEvent m_event;

        public void Invoke()
        {
            m_event.Invoke();
        }
    }
}
