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
    public bool m_isDialogueActive;
    
    private int m_mapController;
    
    #endregion
    

    
    #region methods

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        AudioManager.FadeIn("MainTheme", 0.2f, 2f);
        
        HudManager.Show(()=>
        {
            HudManager.Instance.HandlePlaying();
        });
    }
    
    public int MapController
    {
        get => m_mapController;
        set => m_mapController = value;
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
