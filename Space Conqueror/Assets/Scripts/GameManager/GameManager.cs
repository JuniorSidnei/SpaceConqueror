using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SpeechScriptable m_speechIntro;
    public SpeechScriptable m_speechBoss;
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
        m_isMeteorOver = false;
        
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

//        if (m_isMeteorOver)
//        {
//            CallDialogue(m_speechBoss);
//            m_isMeteorOver = false;
//        }
    }

    void OnEnable()
    {
        DialogueManager.EndDialogue += ActiveMeteor;
        DialogueManager.EndDialogue += ActiveBoss;
    }
    
    
    void OnDisable()
    {
        DialogueManager.EndDialogue -= ActiveMeteor;
        DialogueManager.EndDialogue -= ActiveBoss;
    }

    public void CallDialogue(SpeechScriptable speech)
    {
        HudManager.Instance.HandleConversation();
        DialogueManager.Instance.StartSpeech(speech, 0.5f);
    }

   
    
    
    private void ActiveMeteor()  { m_isMeteorOn = true; }

    private void ActiveBoss()
    {
        Debug.Log("Chamou boss: " + m_isMeteorOver);
        if (m_isMeteorOver)
        {
            FindObjectOfType<KrasLosnas>().GetComponent<Animator>().SetTrigger("BossOn");
        }
    }

    public bool MeteorOn()  { return m_isMeteorOn;  }

    public void SetMeteorOver(bool isover)  {  m_isMeteorOver = isover; }

   // public bool MeteorOver() { return m_isMeteorOver;  }

    #endregion
}
