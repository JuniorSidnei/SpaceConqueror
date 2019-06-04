using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PanelControllerConversation : BaseHudBehavior
{
    private PlayerInfo m_playerInfo;
    [SerializeField] private RectTransform m_rect;

    private const float PLAYNG_POS_Y = -120f;
    private const float CONVERSATION_POS_Y = 540f;

    //public Animator m_boxDialogueAnim;
    public GameObject m_boxDialogue;
    
    public override void SetPlayerInfo(PlayerInfo playerInfo)
    {
        m_playerInfo = playerInfo;
    }
    
    public override void HandleConversation()
    {
        base.HandleConversation();
        //m_boxDialogueAnim.SetTrigger("ShowUp");
        m_boxDialogue.gameObject.transform.DOScale(new Vector3(1, 1, 0), 1f).OnComplete(() =>
        {
            m_rect.anchoredPosition = new Vector2(0, CONVERSATION_POS_Y);
        });
        
    }

    public override void HandlePlaying()
    {
        base.HandlePlaying();
        //m_boxDialogueAnim.SetTrigger("ShowDown");
        m_boxDialogue.gameObject.transform.DOScale(new Vector3(0, 0, 0), 1f).OnComplete(() =>
        {
            m_rect.anchoredPosition = new Vector2(0, PLAYNG_POS_Y);
        });
        
    }
}
