using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/PlayerInfo")]
public class PlayerInfo : ScriptableObject, ISerializationCallbackReceiver
{
    public event Action<PlayerInfo, ControlPlayer> OnValueChanged;

    //Não alteram valor
    [SerializeField]
    private int m_maxLife;
    [SerializeField]
    private int m_currentLife;
    [SerializeField]
    private int m_halfLife;
    [SerializeField]
    private int m_recoveryKit;
    [SerializeField]
    private int m_recoveryAmount;
    [SerializeField]
    private float m_fuel;
    [SerializeField]
    private int m_load;
    [SerializeField]
    private int m_maxLoad;

    [Header("Shoots")]
    [SerializeField]
    private GameObject m_primaryShoot;
    [SerializeField]
    private GameObject m_secondaryShoot;
   
    private ControlPlayer m_controlPlayer;

    //Alteram valor durante o jogo
    private int m_maxLifeInGame;
    
    private int m_currentLifeInGame;
    
    private int m_halfLifeInGame;
    
    private float m_speedInGame;

    private int m_recoveryKitInGame;

    private float m_fuelInGame;

    private int m_loadInGame;

    public void OnBeforeSerialize()
    {
        m_maxLifeInGame = m_maxLife;
        m_currentLifeInGame = m_currentLife;
        m_recoveryKitInGame = m_recoveryKit;
        m_fuelInGame = m_fuel;
        m_loadInGame = m_load;
    }

    public void OnAfterDeserialize()
    {
        m_maxLifeInGame = m_maxLife;
        m_currentLifeInGame = m_currentLife;
        m_recoveryKitInGame = m_recoveryKit;
        m_fuelInGame = m_fuel;
        m_loadInGame = m_load;
    }

    public void SetControlPlayer(ControlPlayer controlPlayer)
    {
        m_controlPlayer = controlPlayer;
    }

    public int CurrentLife
    {
        get => m_currentLifeInGame;
        set
        {
            m_currentLifeInGame = value;
            OnValueChanged?.Invoke(this, m_controlPlayer);
        }
    }

    public int MaxLife
    {
        get => m_maxLifeInGame;
        set => m_maxLifeInGame = value;
    }
    
    public int RecoveryKit
    {
        get => m_recoveryKitInGame;
        set => m_recoveryKitInGame = value;
    }

    public int RecoveryAmount
    {
        get => m_recoveryAmount;
    }
    
    public GameObject PrimaryShoot
    {
        get => m_primaryShoot;
        set => m_primaryShoot = value;
    }

    public GameObject SecondaryShoot
    {
        get => m_secondaryShoot;
        set => m_secondaryShoot = value;
    }
    
    public float FuelInGame
    {
        get => m_fuelInGame;
        set => m_fuelInGame = value;
    }

    public int LoadInGame
    {
        get => m_loadInGame;
        set => m_loadInGame = value;
    }
    public int MaxLoad => m_maxLoad;
    
}

