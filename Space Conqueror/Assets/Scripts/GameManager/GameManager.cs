using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    public GameObject map;

    private int m_mapController = 0;
    
    
    #region methods

    private void Awake()
    {
        Instance = this;
        map.SetActive(false);
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
        
        //Input para ativar o mapa
        if (Input.GetKeyDown(KeyCode.M) && m_mapController == 0)
        {
            MapShowUp();
        }
        else if (Input.GetKeyDown(KeyCode.M) && m_mapController == 1)
        {
            MapShowDown();
        }
    }
    
    private void MapShowUp()
    {
        HudManager.Instance.HandleMap();
        
//        map.gameObject.transform.DOScale(new Vector3(1, 1, 1), 2f).OnComplete(()=>
//        {
//            m_mapController++;
//        });
        map.SetActive(true);
        map.gameObject.transform.DOPunchScale(new Vector3(0.2f,0.2f,0),1f,1,0).OnComplete(() =>
        {
            m_mapController++;
        });    
    }

    private void MapShowDown()
    {
        map.gameObject.transform.DOScale(new Vector3(0, 0, 0), 1f).OnComplete(() =>
        {
            map.SetActive(false);
            m_mapController--;
            HudManager.Instance.HandlePlaying();
            map.gameObject.transform.DOScale(new Vector3(1, 1, 0), 0.1f);
        });

    }
    
    //Carrega a proxima cena quando o boss morrer
    public void RestartScene()
    {
        HudManager.m_isLoaded = false;
        m_playerInfo.CurrentLife = m_playerInfo.MaxLife;
        SceneManager.LoadScene("FirstLevel");
        //Application.Quit();
    }

    #endregion
}
