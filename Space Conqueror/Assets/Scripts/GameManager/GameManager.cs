using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
  
    #region variables
    
    public PlayerInfo m_playerInfo;
    public float m_firstDialogueTimer = 5f;
    private bool m_isDialogueOn;
    
    public bool m_isDialogueActive;
    #endregion
    
    private int m_mapController;
    private int m_invController;
    private int m_armoryController;

    

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

        if (Input.GetKey(KeyCode.Keypad1))
        {
            m_playerInfo.PrimaryShoot = m_playerInfo.FireShoot;
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
            m_playerInfo.PrimaryShoot = m_playerInfo.IceShoot;
        }

        if (Input.GetKey(KeyCode.Keypad3))
        {
            m_playerInfo.PrimaryShoot = m_playerInfo.LightningShoot;
        }
        
    }

    public int InvController
    {
        get => m_invController;
        set => m_invController = value;
    }
    
    public int MapController
    {
        get => m_mapController;
        set => m_mapController = value;
    }
    
    public int ArmoryController
    {
        get => m_armoryController;
        set => m_armoryController = value;
    }
    
    //Carrega a proxima cena quando o boss morrer
    public void RestartScene()
    {
        HudManager.m_isLoaded = false;
        m_playerInfo.CurrentLife = m_playerInfo.MaxLife;
        SceneManager.LoadScene("FirstLevel");
    }

    #endregion
}
