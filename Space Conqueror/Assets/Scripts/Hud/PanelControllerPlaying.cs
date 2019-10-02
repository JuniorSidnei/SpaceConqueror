﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PanelControllerPlaying : BaseHudBehavior
{
    public enum TypeObject
    {
        Collectable, Armory
    }
    
    [SerializeField] private PlayerInfo m_playerInfo;
    private ControlPlayer m_controlPlayer;
    
    [Header("Boxes")]
    [FormerlySerializedAs("m_lifeBox")]  public GameObject _lifeBox;
    [FormerlySerializedAs("m_recoveryBox")] public GameObject _recoveryBox;
    [FormerlySerializedAs("m_speedometerBox")] public GameObject _speedometerBox;

    [Header("Objects")]
    public GameObject Map;
    public GameObject Collectables;
    public GameObject Armory;
        
    [Header("Images")]
    public Image m_LifeBarFill;
    public Image m_recoveryKitKey;
    public Image m_speedometerFill;
    
    [Header("Texts")]
    public TextMeshProUGUI m_playerLifeText;
    [SerializeField]
    public TextMeshProUGUI m_logText;
    public TextMeshProUGUI FireMeteoriteAmount;
    public TextMeshProUGUI LightningMeteoriteAmount;
    public TextMeshProUGUI IceMeteoriteAmount;
    public TextMeshProUGUI RecoveryAmount;

    [Header("Rects")]
    public RectTransform CollectableRect;
    public RectTransform ArmoryRect;
    
    [Header("HudSettings")]
    public Image CrackedHud;

    private TypeObject m_typeObject;
    private void Start()
    {
        m_controlPlayer = FindObjectOfType<ControlPlayer>();
    }

    private void Update()
    {
        //Se levar muuito dano, vai rachar o vidro da nave
        if(m_playerInfo.CurrentLife <= m_playerInfo.MaxLife / 3)
            CrackedHud.gameObject.SetActive(true);
        
        m_playerLifeText.text = ("" + m_playerInfo.CurrentLife);
        m_LifeBarFill.DOFillAmount((float)m_playerInfo.CurrentLife / m_playerInfo.MaxLife, 2f);
        
        m_speedometerFill.DOFillAmount(m_controlPlayer.GetComponent<Rigidbody2D>().velocity.sqrMagnitude, 1f);
        if (m_playerInfo.RecoveryKit <= 0)
            m_recoveryKitKey.gameObject.GetComponent<Image>().DOColor(Color.black, 1f);
        
        //Update dos valores de textos em relação ao jogador
        FireMeteoriteAmount.text = m_playerInfo.FireMeteoriteInGame.ToString();
        IceMeteoriteAmount.text = m_playerInfo.IceMeteoriteInGame.ToString();
        LightningMeteoriteAmount.text = m_playerInfo.LightningMeteoriteInGame.ToString();
        RecoveryAmount.text = m_playerInfo.RecoveryAmount.ToString();
        
        //Input para ativar o mapa
        if (Input.GetKeyDown(KeyCode.Tab) && GameManager.Instance.MapController == 0)
        {
            ShowMap();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && GameManager.Instance.MapController == 1)
        {
            HideMap();
        }
    }

    //Informações iniciais do jogador
    public override void SetPlayerInfo(PlayerInfo playerInfo)
    {
        m_playerInfo = playerInfo;
    }
    
    public override void HandleConversation()
    {
        base.HandleConversation();
        _lifeBox.gameObject.transform.DOScale(new Vector3(0, 0,  0),1f);
        _recoveryBox.gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        _speedometerBox.gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
    }

    public override void HandlePlaying()
    {
        base.HandlePlaying();
        _lifeBox.gameObject.transform.DOScale(new Vector3(1, 1,  0),1f);
        _recoveryBox.gameObject.transform.DOScale(new Vector3(1, 1, 0), 2f);
        _speedometerBox.gameObject.transform.DOScale(new Vector3(1, 1, 1), 3f);
    }

    public override void HandleMap()
    {
        base.HandleMap();
        HandleConversation();
    }

    //Map Hud
    private void ShowMap()
    {
        AudioManager.PlaySound("MapShowUp");
        Map.SetActive(true);
        Map.gameObject.transform.DOPunchScale(new Vector3(0.2f,0.2f,0),1f,1,0).OnComplete(() =>
        {
            GameManager.Instance.MapController++;
        });
    }

    private void HideMap()
    {
        AudioManager.PlaySound("MapShowDown");
        Map.gameObject.transform.DOScale(new Vector3(0, 0, 0), 1f).OnComplete(() =>
        {
            Map.SetActive(false);
            GameManager.Instance.MapController--;
            HandlePlaying();
            Map.gameObject.transform.DOScale(new Vector3(37.92f, 37.92f, 0), 0.1f);
        });  
    }
    
    //Collectables HUD
    public void OnClickShowCollectableButton()
    {
        AudioManager.PlaySound("MapShowUp");
        CollectableRect.DOAnchorPos(new Vector3(350, -560, 0),1f).OnComplete(() =>
        {
            GameManager.Instance.InvController++;
        });
    }

    public void OnClickHideCollectableButton()
    {
        AudioManager.PlaySound("MapShowDown");
        CollectableRect.DOAnchorPos(new Vector3(1260, -560,0),1f).OnComplete(() =>
        {
            GameManager.Instance.InvController--;
            HandlePlaying();  
        });
    }

    //Armory HUD
    public void OnClickShowArmoryButton()
    {
        AudioManager.PlaySound("MapShowUp");
        ArmoryRect.DOAnchorPos(new Vector3(-350, -560, 0),1f).OnComplete(() =>
        {
            GameManager.Instance.ArmoryController++;
        });
    }

    public void OnClickHideArmoryButton()
    {
        AudioManager.PlaySound("MapShowDown");
        ArmoryRect.DOAnchorPos(new Vector3(-1248,-560,0), 1f).OnComplete(() =>
        {
            GameManager.Instance.ArmoryController--;
            HandlePlaying();  
        });
    }
    
}
