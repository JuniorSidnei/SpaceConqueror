using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SpeechScriptable m_speechIntro;
    
    #region variables
    
    public PlayerInfo m_playerInfo;
    public float m_firstDialogueTimer = 5f;
    private bool m_isDialogueOn;
    private bool m_isMeteorOn;
    private bool m_isMeteorOver;
    

    #endregion

    #region methods
    void Start()
    {
        
        Instance = this;
        m_isDialogueOn = true;
        m_isMeteorOn = false;
        
        HudManager.Show(()=>
        { 
            HudManager.Instance.HandlePlaying();
        });
    }


    void Update()
    {
        //Primeiro dialogo
        if (m_isDialogueOn)
        {
            m_firstDialogueTimer -= Time.deltaTime;

            if (m_firstDialogueTimer <= 0)
            {
                m_isDialogueOn = false;
                CallDialogue(m_speechIntro);
                m_firstDialogueTimer = 5;
            }
        }
    }

    void OnEnable()
    {
        DialogueManager.EndDialogue += ActiveMeteor;
    }
    
    
    void OnDisable()
    {
        DialogueManager.EndDialogue -= ActiveMeteor;
    }

    
    //Chama a HUD e o primeiro dialogo da cena
    public void CallDialogue(SpeechScriptable speech)
    {
        HudManager.Instance.HandleConversation();
        DialogueManager.Instance.StartSpeech(speech, 0.5f);
    }

    private void ActiveMeteor()  { m_isMeteorOn = true; }

    public bool MeteorOn()  { return m_isMeteorOn;  }

    public void SetMeteorOver(bool isover)  {  m_isMeteorOver = isover; }
    
    #endregion
}
