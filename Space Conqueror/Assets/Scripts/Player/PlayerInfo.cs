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
    
    //Alteram valor durante o jogo
    private int m_maxLifeInGame;
    
    private int m_currentLifeInGame;
    
    private int m_halfLifeInGame;
    
    private float m_speedInGame;

    
    public void OnBeforeSerialize()
    {
        m_maxLifeInGame = m_maxLife;
        m_currentLifeInGame = m_currentLife;
        m_halfLifeInGame = m_halfLife;
        m_speedInGame = m_speed;
    }

    public void OnAfterDeserialize()
    {
        m_maxLifeInGame = m_maxLife;
        m_currentLifeInGame = m_currentLife;
        m_halfLifeInGame = m_halfLife;
        m_speedInGame = m_speed;
    }

    
    public int CurrentLife
    {
        get => m_currentLifeInGame;
        set
        {
            m_currentLifeInGame = value;

            if (OnValueChanged != null)
                OnValueChanged(this);
        }
    }

    public int MaxLife
    {
        get => m_maxLifeInGame;
        set => m_maxLifeInGame = value;
    }

    public float Speed
    {
        get => m_speed;
        set => m_speed = value;
    }

    public int HalfLife
    {
        get => m_halfLife;
        set => m_halfLife = value;
    }
}

