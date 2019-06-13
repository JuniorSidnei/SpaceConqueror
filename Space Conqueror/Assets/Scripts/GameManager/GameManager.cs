﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
  
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
                EventHandler.Instance.CallDialogueAndEvent();
                m_firstDialogueTimer = 5;
            }
        }
    }
    
    //Carrega a proxima cena quando o boss morrer
    public void RestartScene()
    {
        //SceneManager.LoadScene("FirstLevel");
        Application.Quit();
    }

    #endregion
}
