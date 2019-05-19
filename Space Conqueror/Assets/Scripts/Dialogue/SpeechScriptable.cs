using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/SpeechInfo")]
public class SpeechScriptable : ScriptableObject
{
   public int m_speechNumber;
   public string m_speechName;
      
   public List<DialogueManager2.SpeechGroup> speechGroup;
}
