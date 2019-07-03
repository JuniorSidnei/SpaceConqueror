using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PanelControllerPlaying : BaseHudBehavior
{
    [SerializeField] private PlayerInfo m_playerInfo;
    private ControlPlayer m_controlPlayer;
    
    [Header("Boxes")]
    public GameObject m_lifeBox;
    public GameObject m_recoveryBox;
    public GameObject m_speedometerBox;
    
    
    [Header("Images")]
    public Image m_LifeBarFill;
    public Image m_recoveryKitKey;
    public Image m_speedometerFill;
    
    [Header("Texts")]
    public TextMeshProUGUI m_playerLifeText;
    [SerializeField]
    public TextMeshProUGUI m_logText;

    [Header("HudSettings")]
    public Image m_crackedHud;

    private void Start()
    {
        m_controlPlayer = FindObjectOfType<ControlPlayer>();
    }

    private void Update()
    {
        //Se levar muuito dano, vai rachar o vidro da nave
        if(m_playerInfo.CurrentLife <= m_playerInfo.MaxLife / 3)
            m_crackedHud.gameObject.SetActive(true);
        
        m_playerLifeText.text = ("" + m_playerInfo.CurrentLife);
        m_LifeBarFill.DOFillAmount((float)m_playerInfo.CurrentLife / m_playerInfo.MaxLife, 2f);
        
        m_speedometerFill.DOFillAmount(m_controlPlayer.m_moveInput, 1f);
        
        if (m_playerInfo.RecoveryKit <= 0)
            m_recoveryKitKey.gameObject.GetComponent<Image>().DOColor(Color.black, 1f);
    }

    //Informações iniciais do jogador
    public override void SetPlayerInfo(PlayerInfo playerInfo)
    {
        m_playerInfo = playerInfo;
    }
    
    public override void HandleConversation()
    {
        base.HandleConversation();
        m_lifeBox.gameObject.transform.DOScale(new Vector3(0, 0,  0),1f);
        m_recoveryBox.gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        m_speedometerBox.gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
    }

    public override void HandlePlaying()
    {
        base.HandlePlaying();
        m_lifeBox.gameObject.transform.DOScale(new Vector3(1, 1,  0),1f);
        m_recoveryBox.gameObject.transform.DOScale(new Vector3(1, 1, 0), 2f);
        m_speedometerBox.gameObject.transform.DOScale(new Vector3(1, 1, 1), 3f);
    }
}
