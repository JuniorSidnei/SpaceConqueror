using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SpeechScriptable m_speechIntro;
    public SpeechScriptable m_speechBoss;
    public SpeechScriptable m_speechEnd;
    #region variables
    
    public PlayerInfo m_playerInfo;
    public float m_firstDialogueTimer = 5f;
    private bool m_isDialogueOn;
//    private bool m_isMeteorOn;
//    private bool m_isMeteorOver;
    //private AudioManager m_audioManager;
    
    #endregion

    
    
    #region methods

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
        
        m_isDialogueOn = true;
        
        AudioManager.PlaySound("MainTheme");

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
                HudManager.Instance.HandleConversation();
                DialogueManager.Instance.StartSpeech(m_speechIntro, 0.5f, ActiveMeteor);
                m_firstDialogueTimer = 5;
                
            }
        }
    
        
        //Ativa dialogo do Boss, depois dos meteoros
        if (MeteorBehavior.GetMeteorOver)
        {
            HudManager.Instance.HandleConversation();
            DialogueManager.Instance.StartSpeech(m_speechBoss, 0.5f, ActiveBoss);
        }

        //Se o boss morrer
        if (!KrasLosnas.isBossAlive)
        {
            HudManager.Instance.HandleConversation();
            DialogueManager.Instance.StartSpeech(m_speechEnd, 0.5f, RestartScene);
        }
    }
    

    //Ativa os meteoros
    private void ActiveMeteor()
    {  /*m_isMeteorOn = true;*/ MeteorBehavior.SetMeteorActive(true);  }
    
    
//    //Seta se meteoros começam ou acabam
//    public void SetMeteor(bool isOver)  {  m_isMeteorOver = isOver; }
//    
//    //Pega a variavel de meteoros
//    public bool GetMeteor()  { return m_isMeteorOn;  }
    
    //Ativa o Boss
    private void ActiveBoss()
    {
        FindObjectOfType<KrasLosnas>().GetComponent<Animator>().SetTrigger("BossOn");
        MeteorBehavior.SetMeteorOver(false);
    }

    //Carrega a proxima cena quando o boss morrer
    public void RestartScene()
    {
        Application.Quit();
    }

    #endregion
}
