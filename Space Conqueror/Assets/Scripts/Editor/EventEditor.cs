using System.Collections;
using System.Collections.Generic;
using DG.DOTweenEditor.UI;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(EventManager))]
public class EventEditor : Editor
{
    private EventManager m_event;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if(m_event == null)
            m_event = (EventManager)target;

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onDialogueFinish"), false);

        if(EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
