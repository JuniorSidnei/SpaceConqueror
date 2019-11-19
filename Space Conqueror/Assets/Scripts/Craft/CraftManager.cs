using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    //Buttons to craft the itens
    [Header("Buttons")]
    public  Button CraftFireBtn;
    public  Button CraftIceBtn;
    public  Button CraftLightningBtn;

    
    //Texts to update on hud
    [Header("Texts")]
    [SerializeField]
    private  TextMeshProUGUI FireAmountCraft;
    [SerializeField]
    private  TextMeshProUGUI IceAmountCraft;
    [SerializeField]
    private  TextMeshProUGUI LightningAmountCraft;
    
    //The amount to craft
    private const int AmountToCraft = 3; 
    
    //Player
    [SerializeField]
    [Header("Player info")]
    private  PlayerInfo m_playerInfo;
    
    private  void Start()
    {
        //Botões inativos quando começa
        CraftFireBtn.enabled = false;
        CraftIceBtn.enabled = false;
        CraftLightningBtn.enabled = false;
    }

    private  void Update()
    {
        UpdateTextUi();
        CheckAmount();
    }

    
    //Atualiza os valores na hud
    private  void UpdateTextUi()
    {
        FireAmountCraft.text = m_playerInfo.FireMeteoriteInGame + " / 25";
        IceAmountCraft.text = m_playerInfo.IceMeteoriteInGame + " / 25";
        LightningAmountCraft.text = m_playerInfo.LightningMeteoriteInGame + " / 25";    
    }
    
    //Checar se o jogador já coletou todos os loots e ativar o botão de craft
    private  void CheckAmount()
    {
        CraftFireBtn.enabled = m_playerInfo.FireMeteoriteInGame >= AmountToCraft;
        
        CraftIceBtn.enabled = m_playerInfo.IceMeteoriteInGame >= AmountToCraft;
        
        CraftLightningBtn.enabled = m_playerInfo.LightningMeteoriteInGame >= AmountToCraft;
    }

    //Craft tiro de fogo
    public  void HandleCraftFireShoot()
    {
        Debug.Log("CRIANDO TIRO DE FOGO");
        m_playerInfo.IsFireCrafted = true;

      
    }

    //Craft tiro de gelo
    public  void HandleCraftIceShoot()
    {
        Debug.Log("CRIANDO TIRO DE GELO");
        m_playerInfo.IsIceCrafted = true;

    }

    //Craft tiro de raio
    public  void HandleCraftLightningShoot()
    {
        Debug.Log("CRIANDO TIRO DE RAIO");
        m_playerInfo.IsLightningCrafted = true;
        
        m_playerInfo.LightningMeteoriteInGame = 0;
    }
}

