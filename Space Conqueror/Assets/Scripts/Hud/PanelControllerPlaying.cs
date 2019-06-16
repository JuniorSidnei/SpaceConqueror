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
    
    private const float PLAYNG_POS_Y = -120f;
    private const float CONVERSATION_POS_Y = 540f;

    private void Update()
    {
        m_playerLifeText.text = ("" + m_playerInfo.CurrentLife);
        m_speedometerFill.DOFillAmount(m_playerInfo.Speed / 1000, 1f);
    }

    //Informações do jogador
    public override void SetPlayerInfo(PlayerInfo playerInfo)
    {
        m_playerInfo = playerInfo;
        playerInfo.OnValueChanged += UpdateHudValues;
    }


    private void UpdateHudValues(PlayerInfo playerinfo, ControlPlayer controlPlayer)
    {
        if (playerinfo == null || controlPlayer == null) return; 
        
        m_LifeBarFill.DOFillAmount((float)playerinfo.CurrentLife / playerinfo.MaxLife, 2f);
        //m_playerLifeText.text = ("" + playerinfo.CurrentLife);

        if (playerinfo.RecoveryKit <= 0)
            m_recoveryKitKey.gameObject.GetComponent<Image>().DOColor(Color.black, 1f);
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
        m_recoveryBox.gameObject.transform.DOScale(new Vector3(1, 1, 0), 1.2f);
        m_speedometerBox.gameObject.transform.DOScale(new Vector3(1, 1, 1), 1.5f);
    }
}
