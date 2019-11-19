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
    [SerializeField]  private bool FireShootCrafted;
    [SerializeField]  private bool IceShootCrafted;
    [SerializeField]  private bool LightningShootCrafted;

    [Header("Shoots")]
    [SerializeField]  private GameObject m_primaryShoot;
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

    private bool m_isFireCrafted;
  
    private bool m_isIceCrafted;
    
    private bool m_isLightningCrafted;

    //Valores iniciais
    public void OnBeforeSerialize()
    {
        m_maxLife = 1000;
        m_currentLife = m_maxLife;
        m_recoveryKit = 2;
        m_redMeteorite = 0;
        m_blueMeteorite = 0;
        m_yellowMeteorite = 0;
        m_isFireCrafted = false;
        m_isIceCrafted = false;
        m_isLightningCrafted = false;
    }

    public void OnAfterDeserialize()
    {
        m_maxLifeInGame = m_maxLife;
        m_currentLifeInGame = m_currentLife;
        m_recoveryKitInGame = m_recoveryKit;
        m_fireMeteoriteInGame = m_redMeteorite;
        m_iceMeteoriteInGame = m_blueMeteorite;
        m_lightningMeteoriteInGame = m_yellowMeteorite;
        m_isFireCrafted = false;
        m_isIceCrafted = false;
        m_isLightningCrafted = false;
    }
    
    public void SetControlPlayer(ControlPlayer controlPlayer)
    {
        m_controlPlayer = controlPlayer;
    }
    
    //if the player craft the shoot
    public bool IsFireCrafted
    {
        get => m_isFireCrafted;
        set => m_isFireCrafted = value;
    }

    //if the player craft the shoot
    public bool IsIceCrafted
    {
        get => m_isIceCrafted;
        set => m_isIceCrafted = value;
    }

    //if the player craft the shoot
    public bool IsLightningCrafted
    {
        get => m_isLightningCrafted;
        set => m_isLightningCrafted = value;
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
    public int RecoveryAmount
    {
        get => m_recoveryAmount;
    }
    
    //The initial shoot
    public GameObject PrimaryShoot
    {
        get => m_primaryShoot;
        set => m_primaryShoot = value;
    }
    
    //The crafted fireshoot
    public GameObject FireShoot
    {
        get => m_FireShoot;
        set => m_FireShoot = value;
    }

    //The crafted iceeshoot
    public GameObject IceShoot
    {
        get => m_IceShoot;
        set => m_IceShoot = value;
    }

    //The crafted lightningshoot
    public GameObject LightningShoot
    {
        get => m_LightningShoot;
        set => m_LightningShoot = value;
    }

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
}

