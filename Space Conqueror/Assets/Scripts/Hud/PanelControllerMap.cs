using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PanelControllerMap : BaseHudBehavior
{
    private PlayerInfo m_playerInfo;

    [SerializeField] private GameObject m_map;
    
    public override void SetPlayerInfo(PlayerInfo playerInfo)
    {
        m_playerInfo = playerInfo;
    }
    
    public override void HandleConversation()
    {
        base.HandleConversation();
        m_map.gameObject.transform.DOScale(new Vector3(0, 0, 0), 1f).OnComplete(()=>m_map.SetActive(false));
    }

    public override void HandlePlaying()
    {
        base.HandlePlaying();
        m_map.gameObject.transform.DOScale(new Vector3(0, 0, 0), 1f).OnComplete(()=>m_map.SetActive(false));
    }

    public override void HandleMap()
    {
        base.HandleMap();
        gameObject.SetActive(true);
        m_map.gameObject.transform.DOScale(new Vector3(1, 1, 0), 1f);
    }
}
