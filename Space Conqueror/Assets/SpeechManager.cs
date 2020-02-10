using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : MonoBehaviour
{
    public SpeechScriptable FirstDialogueMessage;
    public SpeechScriptable SecondDialogueMessage;
    public SpeechScriptable FireMeteorWarningMessage;
    public SpeechScriptable IceMeteorWarningMessage;
    public SpeechScriptable LightningMeteorWarningMessage;

    public float m_firstDialogueTimer = 5f;
    private bool m_isDialogueOn = true;
    
    private void Update() {
        
        //Primeiro dialogo
        if (m_isDialogueOn) {
            m_firstDialogueTimer -= Time.deltaTime;

            if (m_firstDialogueTimer <= 0) {
                
                m_isDialogueOn = false;
                EventHandler.Instance.CallDialogue(FirstDialogueMessage);
                m_firstDialogueTimer = 5;
            }
        }
    }

    public void FireMeteorWarning(GameObject other) {
        EventHandler.Instance.CallDialogue(FireMeteorWarningMessage);
        Destroy(other);
    }
    
    public void IceMeteorWarning(GameObject other) {
        EventHandler.Instance.CallDialogue(IceMeteorWarningMessage);
        Destroy(other);
    }
    
    public void LightningMeteorWarning(GameObject other) {
        EventHandler.Instance.CallDialogue(LightningMeteorWarningMessage);
        Destroy(other);
    }
}
