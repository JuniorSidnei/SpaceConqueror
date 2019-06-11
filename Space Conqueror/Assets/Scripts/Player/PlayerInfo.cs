using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/PlayerInfo")]
public class PlayerInfo : ScriptableObject, ISerializationCallbackReceiver
{
    public event Action<PlayerInfo> OnValueChanged;

    //Não alteram valor
    [SerializeField]
    private int m_maxLife;
    [SerializeField]
    private int m_currentLife;
    [SerializeField]
    private int m_halfLife;
    [SerializeField]
    private float m_speed = 700;
    [SerializeField]
    private int m_recoveryKit;
    [SerializeField]
    private int m_recoveryAmount;

    

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

    
    public int CurrentLife
    {
        get => m_currentLifeInGame;
        set
        {
            m_currentLifeInGame = value;
            OnValueChanged?.Invoke(this);
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
}

