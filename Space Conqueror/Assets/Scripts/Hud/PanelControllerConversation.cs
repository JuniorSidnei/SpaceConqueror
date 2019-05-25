using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelControllerConversation : BaseHudBehavior
{
    private PlayerInfo m_playerInfo;
    [SerializeField] private RectTransform m_rect;

    private const float PLAYNG_POS_Y = -229.5f;
    private const float CONVERSATION_POS_Y = 530f;
   
   
    
    public override void SetPlayerInfo(PlayerInfo playerInfo)
    {
        m_playerInfo = playerInfo;
    }
    
    public override void HandleConversation()
    {
        base.HandleConversation();
        m_rect.anchoredPosition = new Vector2(0, CONVERSATION_POS_Y);
    }

    public override void HandlePlaying()
    {
        base.HandlePlaying();
        m_rect.anchoredPosition = new Vector2(0, PLAYNG_POS_Y);
    }
}
