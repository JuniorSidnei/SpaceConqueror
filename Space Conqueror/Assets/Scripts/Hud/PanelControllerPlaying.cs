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
    [SerializeField] private Image m_LifeBarFill;
    [SerializeField] private TextMeshProUGUI m_playerLifeText;
    [SerializeField] private RectTransform m_rect;
    
    private const float PLAYNG_POS_Y = -120f;
    private const float CONVERSATION_POS_Y = 540f;

    [FormerlySerializedAs("m_playBox")] public GameObject m_lifeBox;
    
    void Update()
    {
        UpdateHudValues(m_playerInfo);
    }

    //Informações do jogador
    public override void SetPlayerInfo(PlayerInfo playerInfo)
    {
        m_playerInfo = playerInfo;
        playerInfo.OnValueChanged += UpdateHudValues;
    }

    private void UpdateHudValues(PlayerInfo playerinfo)
    {
        if (playerinfo == null) return;
        m_LifeBarFill.fillAmount = (float)playerinfo.CurrentLife / playerinfo.MaxLife;
        m_playerLifeText.text = ("" + playerinfo.CurrentLife);
    }


    public override void HandleConversation()
    {
        base.HandleConversation();
        m_lifeBox.gameObject.transform.DOScale(new Vector3(0, 0,  0),1f);
        //m_rect.anchoredPosition = new Vector2(0, CONVERSATION_POS_Y);
    }

    public override void HandlePlaying()
    {
        base.HandlePlaying();
        m_lifeBox.gameObject.transform.DOScale(new Vector3(1, 1,  0),1f);
        //m_rect.anchoredPosition = new Vector2(0, PLAYNG_POS_Y);
    }
}
