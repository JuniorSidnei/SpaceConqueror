using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControllerMap : BaseHudBehavior
{
    private PlayerInfo m_playerInfo;
    
    public override void SetPlayerInfo(PlayerInfo playerInfo)
    {
        m_playerInfo = playerInfo;
    }
}
