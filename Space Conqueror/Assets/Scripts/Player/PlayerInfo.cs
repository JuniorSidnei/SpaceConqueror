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
    private float m_speed;
    [SerializeField]
    private float m_maxSpeed;
    [SerializeField]
    private float m_acceleration;
    [SerializeField]
    private float m_deceleration;
    [SerializeField]
    private float m_timeToMaxSpeed;
 
    [SerializeField]
    private int m_recoveryKit;
    [SerializeField]
    private int m_recoveryAmount;
   
    private ControlPlayer m_controlPlayer;

    //Alteram valor durante o jogo
    private int m_maxLifeInGame;
    
    private int m_currentLifeInGame;
    
    private int m_halfLifeInGame;
    
    private float m_speedInGame;

    private int m_recoveryKitInGame;

    
    public void OnBeforeSerialize()
    {
        m_maxLifeInGame = m_maxLife;
        m_currentLifeInGame = m_currentLife;
        m_speedInGame = m_speed;
        m_recoveryKitInGame = m_recoveryKit;
    }

    public void OnAfterDeserialize()
    {
        m_maxLifeInGame = m_maxLife;
        m_currentLifeInGame = m_currentLife;
        m_speedInGame = m_speed;
        m_recoveryKitInGame = m_recoveryKit;
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

    public float Speed
    {
        get => m_speedInGame;
        set => m_speedInGame = value;
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
    
    public float MaxSpeed
    {
        get => m_maxSpeed;
        set => m_maxSpeed = value;
    }

    public float Acceleration
    {
        get => m_acceleration;
        set => m_acceleration = value;
    }

    public float Deceleration
    {
        get => m_deceleration;
        set => m_deceleration = value;
    }
    public float TimeToMaxSpeed
    {
        get => m_timeToMaxSpeed;
        set => m_timeToMaxSpeed = value;
    }
}

