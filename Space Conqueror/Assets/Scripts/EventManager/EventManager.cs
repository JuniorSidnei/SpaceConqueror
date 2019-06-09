using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[System.Serializable]
public class EventManager : MonoBehaviour
{
    public List<MetaEvent> onDialogueFinish;


    [System.Serializable]
    public class MetaEvent
    {
        public string name;
        [FormerlySerializedAs("m_event")] public UnityEvent m_metaEvent;
        
        public void Invoke()
        {
            m_metaEvent?.Invoke();
        }
    }
}

