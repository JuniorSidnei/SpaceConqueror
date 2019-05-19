using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelControllerConversation : BaseHudBehavior
{
    private PlayerInfo m_playerInfo;
    //public TextMeshProUGUI m_conversationText;
    //public TextMeshProUGUI m_NameNPC;
   
    
    public override void SetPlayerInfo(PlayerInfo playerInfo)
    {
        m_playerInfo = playerInfo;
    }
    
    public override void HandleConversation()
    {
        base.HandleConversation();
    }

    public override void HandlePlaying()
    {
        base.HandlePlaying();
    }
}
