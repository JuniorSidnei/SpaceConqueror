using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PanelControllerConversation : BaseHudBehavior
{
    private PlayerInfo m_playerInfo;
    
    public GameObject m_boxDialogue;
    
    public override void SetPlayerInfo(PlayerInfo playerInfo)
    {
        m_playerInfo = playerInfo;
    }
    
    public override void HandleConversation()
    {
        base.HandleConversation();
        m_boxDialogue.gameObject.transform.DOScale(new Vector3(1, 1, 0), 1f);
    }

    public override void HandlePlaying()
    {
        base.HandlePlaying();
        m_boxDialogue.gameObject.transform.DOScale(new Vector3(0, 0, 0), 1f);
    }
}
