using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Events
    {
        Meteors, FirstLevelBoss, enemysFirstWave, enemysSecondWave, EndFirstLevel
    }
    
    public static GameManager Instance;
    public SpeechScriptable m_speechIntro;
    public SpeechScriptable m_speechBoss;
    public SpeechScriptable m_speechEnd;
    #region variables
    
    public PlayerInfo m_playerInfo;
    public float m_firstDialogueTimer = 5f;
    private bool m_isDialogueOn;

    //private EventManager m_event;
    //public int current;
    #endregion

    
    
    #region methods

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
        
        m_isDialogueOn = true;
        
        AudioManager.FadeIn("MainTheme", 0.2f, 2f);
        
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

                //HudManager.Instance.HandleConversation();
                //DialogueManager.Instance.StartSpeech(m_speechIntro, 1.5f, ActiveMeteor);
                DialogueManager.Instance.StartSpeech(m_speechIntro, 1.5f,
                    () => { EventManager.Instance.onDialogueFinish[EventManager.Instance.m_currentEvent].Invoke(); });

                m_firstDialogueTimer = 5;
            }
        }
    }

//        //Ativa dialogo do Boss, depois dos meteoros
//        if (MeteorBehavior.GetMeteorOver)
//        {
//            HudManager.Instance.HandleConversation();
//            //DialogueManager.Instance.StartSpeech(m_speechBoss, 1.5f, ActiveBoss);
//         
//        }
//
//        //Se o boss morrer
//        if (!KrasLosnas.isBossAlive)
//        {
//            HudManager.Instance.HandleConversation();
//            DialogueManager.Instance.StartSpeech(m_speechEnd, 1.5f, RestartScene);
//            
//        }
    
    

    //Ativa os meteoros
//    private void ActiveMeteor()
//    { MeteorBehavior.SetMeteorActive(true);  }
    
//    public void OnDialogueFinish(){
//        m_eventList[next++].Invoke();
//    }
    
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
        AudioManager.FadeOut("MainTheme", 2f);
        AudioManager.PlaySound("KrasLonasTheme");
    }

    //Carrega a proxima cena quando o boss morrer
    public void RestartScene()
    {
        Application.Quit();
    }

    #endregion
}
