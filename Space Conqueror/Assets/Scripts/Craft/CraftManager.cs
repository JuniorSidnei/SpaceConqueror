using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class CraftManager
{
    //Buttons to craft the itens
    [Header("Buttons")]
    public static Button FireCraftBtn;
    public static Button IceCraftBtn;
    public static Button LightningCraftBtn;

    //Texts to update on hud
    [Header("Texts")]
    public static TextMeshProUGUI FireAmountCraft;
    public static TextMeshProUGUI IceAmountCraft;
    public static TextMeshProUGUI LightningAmountCraft;
    
    //The amount to craft
    private const int AmountToCraft = 3; 
    
    //Player
    [SerializeField]
    private static PlayerInfo m_playerInfo;
    
    private static void Start()
    {
        //Botões inativos quando começa
        FireCraftBtn.enabled = false;
        IceCraftBtn.enabled = false;
        LightningCraftBtn.enabled = false;
    }

    private static void Update()
    {
        UpdateTextUi();
        CheckAmount();
    }

    
    //Atualiza os valores na hud
    private static void UpdateTextUi()
    {
        FireAmountCraft.text = m_playerInfo.FireMeteoriteInGame + " / 25";
        IceAmountCraft.text = m_playerInfo.IceMeteoriteInGame + " / 25";
        LightningAmountCraft.text = m_playerInfo.LightningMeteoriteInGame + " / 25";    
    }
    
    //Checar se o jogador já coletou todos os loots e ativar o botão de craft
    private static void CheckAmount()
    {
        if (m_playerInfo.FireMeteoriteInGame >= AmountToCraft)
            FireCraftBtn.enabled = true;
        if (m_playerInfo.IceMeteoriteInGame >= AmountToCraft)
            IceCraftBtn.enabled = true;
        if (m_playerInfo.LightningMeteoriteInGame >= AmountToCraft)
            LightningCraftBtn.enabled = true;
    }

    //Craft tiro de fogo
    public static void HandleCraftFireShoot()
    {
        Debug.Log("CRIANDO TIRO DE FOGO");
    }

    //Craft tiro de gelo
    public static void HandleCraftIceShoot()
    {
        Debug.Log("CRIANDO TIRO DE GELO");
    }

    //Craft tiro de raio
    public static void HandleCraftLightningShoot()
    {
        Debug.Log("CRIANDO TIRO DE RAIO");
    }
}

