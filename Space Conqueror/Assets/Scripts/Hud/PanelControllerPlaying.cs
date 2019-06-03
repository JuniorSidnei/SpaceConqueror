using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelControllerPlaying : BaseHudBehavior
{
    [SerializeField] private PlayerInfo m_playerInfo;
    [SerializeField] private Image m_LifeBarFill;
    [SerializeField] private TextMeshProUGUI m_playerLifeText;
    [SerializeField] private RectTransform m_rect;
    
    private const float PLAYNG_POS_Y = -120f;
    private const float CONVERSATION_POS_Y = 540f;

    public Animator m_playAnim;

//    private void Start()
//    {
//        m_playAnim = GetComponentInChildren<Animator>();
//    }

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
        m_playAnim.SetTrigger("ShowDown");
        base.HandleConversation();
        m_rect.anchoredPosition = new Vector2(0, CONVERSATION_POS_Y);
        //m_playAnim.SetTrigger("Default");
    }

    public override void HandlePlaying()
    {
        m_playAnim.SetTrigger("ShowUp");
        base.HandlePlaying();
        m_rect.anchoredPosition = new Vector2(0, PLAYNG_POS_Y);
        m_playAnim.SetTrigger("StandBy");
    }
}
