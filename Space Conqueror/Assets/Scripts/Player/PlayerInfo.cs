using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/PlayerInfo")]
public class PlayerInfo : ScriptableObject, ISerializationCallbackReceiver
{
    public event Action<PlayerInfo, ControlPlayer> OnValueChanged;

    //Não alteram valor
    [SerializeField]  private int m_maxLife;
    [SerializeField]  private int m_currentLife;
    [SerializeField]  private int m_recoveryKit;
    [SerializeField]  private int m_recoveryAmount;
    [SerializeField]  private int m_redMeteorite;
    [SerializeField]  private int m_blueMeteorite;
    [SerializeField]  private int m_yellowMeteorite;
    [SerializeField]  private int m_grayMeteorite;
    [SerializeField]  private bool m_fireShootCrafted;
    [SerializeField]  private bool m_iceShootCrafted;
    [SerializeField]  private bool m_lightningShootCrafted;

    [Header("Shoots")]
    [SerializeField]  private GameObject m_Shoot;
    [SerializeField]  private GameObject m_StandardShoot;
    [SerializeField]  private GameObject m_FireShoot;
    [SerializeField]  private GameObject m_IceShoot;
    [SerializeField]  private GameObject m_LightningShoot;
    
    private ControlPlayer m_controlPlayer;

    //Alteram valor durante o jogo
    private int m_maxLifeInGame;
    
    private int m_currentLifeInGame;

    private float m_speedInGame;

    private int m_recoveryKitInGame;
    
    private int m_loadInGame;

    private int m_fireMeteoriteInGame;

    private int m_iceMeteoriteInGame;

    private int m_lightningMeteoriteInGame;
    
    private int m_grayMeteoriteInGame;

    private bool m_isFireCraftedInGame;
  
    private bool m_isIceCraftedInGame;
    
    private bool m_isLightningCraftedInGame;

    private GameObject m_shootInGame;

    //Valores iniciais
    public void OnBeforeSerialize()
    {
        m_maxLife = 1000;
        m_currentLife = m_maxLife;
        m_recoveryKit = 2;
        m_redMeteorite = 0;
        m_blueMeteorite = 0;
        m_yellowMeteorite = 0;
        m_fireShootCrafted = false;
        m_iceShootCrafted = false;
        m_lightningShootCrafted = false;
        m_Shoot = m_StandardShoot;
    }

    public void OnAfterDeserialize()
    {
        m_maxLifeInGame = m_maxLife;
        m_currentLifeInGame = m_currentLife;
        m_recoveryKitInGame = m_recoveryKit;
        m_fireMeteoriteInGame = m_redMeteorite;
        m_iceMeteoriteInGame = m_blueMeteorite;
        m_lightningMeteoriteInGame = m_yellowMeteorite;
        m_grayMeteoriteInGame = m_grayMeteorite;
        m_isFireCraftedInGame = m_fireShootCrafted;
        m_isIceCraftedInGame = m_iceShootCrafted;
        m_isLightningCraftedInGame = m_lightningShootCrafted;
        m_shootInGame = m_StandardShoot;
    }
    
    public void SetControlPlayer(ControlPlayer controlPlayer)
    {
        m_controlPlayer = controlPlayer;
    }
    
    //if the player craft the shoot
    public bool IsFireCrafted
    {
        get => m_isFireCraftedInGame;
        set => m_isFireCraftedInGame = value;
    }

    //if the player craft the shoot
    public bool IsIceCrafted
    {
        get => m_isIceCraftedInGame;
        set => m_isIceCraftedInGame = value;
    }

    //if the player craft the shoot
    public bool IsLightningCrafted
    {
        get => m_isLightningCraftedInGame;
        set => m_isLightningCraftedInGame = value;
    }

    
    //Current life during gameplay
    public int CurrentLife
    {
        get => m_currentLifeInGame;
        set
        {
            m_currentLifeInGame = value;
            OnValueChanged?.Invoke(this, m_controlPlayer);
        }
    }

    //Maxlife to access during the start game
    public int MaxLife
    {
        get => m_maxLifeInGame;
        set => m_maxLifeInGame = value;
    }
    
    //The total kits int the game
    public int RecoveryKit
    {
        get => m_recoveryKitInGame;
        set => m_recoveryKitInGame = value;
    }

    //The amount that every kit recover
    public int RecoveryAmount => m_recoveryAmount;

    //the initial shoot
    public GameObject Shoot
    {
        get => m_shootInGame;
        set => m_shootInGame = value;
    }
    
    //The crafted fireshoot
    public GameObject FireShoot => m_FireShoot;

    //The crafted iceeshoot
    public GameObject IceShoot => m_IceShoot;

    //The crafted lightningshoot
    public GameObject LightningShoot => m_LightningShoot;
    
    //The standard shoot
    public GameObject StandardShoot => m_StandardShoot;


    //The drops of fire
    public int FireMeteoriteInGame
    {
        get => m_fireMeteoriteInGame;
        set => m_fireMeteoriteInGame = value;
    }

    //The drops of ice
    public int IceMeteoriteInGame
    {
        get => m_iceMeteoriteInGame;
        set => m_iceMeteoriteInGame = value;
    }

    //The drops of lightning
    public int LightningMeteoriteInGame
    {
        get => m_lightningMeteoriteInGame;
        set => m_lightningMeteoriteInGame = value;
    }
    //The drops of normal
    public int GrayMeteoriteInGame
    {
        get => m_grayMeteoriteInGame;
        set => m_grayMeteoriteInGame = value;
    }
}

