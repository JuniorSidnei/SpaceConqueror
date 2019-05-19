using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechTrigger : MonoBehaviour
{
    [SerializeField] private SpeechScriptable m_speech;
    private PlayerInfo m_playerInfo;
    
    public void SpeechTest()
    {
        HudManager.Show(()=>
            HudManager.Instance.InitializeHudDialogueInfo(m_playerInfo));
        
        DialogueManager2.Instance.StartSpeech(m_speech, 1f);
    }
}
