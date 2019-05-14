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


    void Update()
    {
        //Debug.Log("Objeto existe?" + m_playerInfo);
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
        m_LifeBarFill.fillAmount = playerinfo.CurrentLife / playerinfo.MaxLife;
        m_playerLifeText.text = ("" + playerinfo.CurrentLife + " / " + playerinfo.MaxLife);
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
