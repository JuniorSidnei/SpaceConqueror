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
        if(m_event == null)
            m_event = (EventManager)target;

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onDialogueFinish"), true);

        if(EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
