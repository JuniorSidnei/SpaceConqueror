using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelControllerConversation : BaseHudBehavior
{
    private PlayerInfo m_playerInfo;
    [SerializeField] private RectTransform m_rect;

    private const float PLAYNG_POS_Y = -120f;
    private const float CONVERSATION_POS_Y = 540f;

    public Animator m_boxDialogueAnim;

//    private void Start()
//    {
//        m_boxDialogueAnim = GetComponentInChildren<Animator>();
//    }

    public override void SetPlayerInfo(PlayerInfo playerInfo)
    {
        m_playerInfo = playerInfo;
    }
    
    public override void HandleConversation()
    {
        m_boxDialogueAnim.SetTrigger("ShowUp");
        base.HandleConversation();
        m_rect.anchoredPosition = new Vector2(0, CONVERSATION_POS_Y);
        m_boxDialogueAnim.SetTrigger("StandBy");
    }

    public override void HandlePlaying()
    {
        m_boxDialogueAnim.SetTrigger("ShowDown");
        base.HandlePlaying();
        m_rect.anchoredPosition = new Vector2(0, PLAYNG_POS_Y);
        m_boxDialogueAnim.SetTrigger("Default");
    }
}
