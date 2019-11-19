using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using DG.Tweening;
using UnityEngine;
using UnityEngineInternal.Input;
// ReSharper disable All

public class ControlPlayer : MonoBehaviour
{
    #region variables 

    [Header("Sprite settigns")] public Transform m_spriteTransform;

    [Header("Player settings")]
    //Aceleração
    public float m_acceleration;

    //Float de posição do personagem
    [HideInInspector] public float m_moveInput;

    //rigidbody do personagem
    private Rigidbody2D m_rb;

    public float m_delay;

    [SerializeField]
    private float m_timeToMaxSpeed;

    private float m_timeAccelerating;

    private bool isAccelerating;

    [Header("Shoot Settings")]
    //Posição do tiro
    public Transform _shotPos;

    //Tempo de recarga
    private float _reloadTime = 0.5f;

    //Pode atirar
    private bool _canShoot;

    //Status do player
    [Header("Player Status")] public PlayerInfo m_playerInfo;

    //Vários estados do jogador
    List<PlayerEffects> m_currentEffects;
    
    //Menos da metade da vida do jogador
    private bool m_dyingAlertSound;
    private bool m_smokeOn;

    [Header("Collision settings")] [SerializeField]
    public LayerMask _colisionLayer;

    [Header("Effects")]
    public GameObject m_smokeEffect;

    public AnimationCurve m_accelerationCuver;

    #endregion

    #region methods

    private void Awake()
    {
        m_playerInfo.SetControlPlayer(this);
    }

    void Start()
    {
        m_currentEffects = new List<PlayerEffects>();
        m_rb = GetComponent<Rigidbody2D>();
        //_shoot = m_playerInfo.PrimaryShoot;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Move();
        }
        else
        {
            Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.m_isDialogueActive)
            Rotate();

        ReloadTimer();
        Shoot();
        RecoveryKit();
        ApplyEffect();

        CameraController.Instance.ZoomOut(isAccelerating);
        
        //Se estiver morrendo vai ficar com o alerta e piscando a nave e soltando fumaça
        if (m_playerInfo.CurrentLife <= (m_playerInfo.MaxLife / 3) && m_dyingAlertSound == false)
        {
            LowLife();
            m_smokeOn = true;
        }

        if (m_playerInfo.CurrentLife >= (m_playerInfo.MaxLife / 3))
        {
            AudioManager.PauseSound("PlayerDyingAlert");
            m_dyingAlertSound = false;
            m_smokeOn = false;
        }

        SmokeOn();
    }

    private void Move()
    {
        m_timeAccelerating += Time.deltaTime;

        float acce = m_accelerationCuver.Evaluate(m_timeAccelerating/m_timeToMaxSpeed);
        
        m_rb.drag = 4.5f;
        m_rb.AddForce(m_acceleration * acce *Time.deltaTime * transform.right, ForceMode2D.Force);
        isAccelerating = true;
    }

    private void Stop()
    {
        m_timeAccelerating = 0;
        isAccelerating = false;
        m_rb.drag = 1;
    }

    private void Rotate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float x, y;
        if (mousePos.x < transform.position.x)
        {
            y = -1;
        }
        else
        {
            y = 1;
        }
        m_spriteTransform.localScale = new Vector3(1, y, 1);
        
        Vector2 direction = Time.deltaTime * new Vector2((mousePos.x - transform.position.x) - m_delay, (mousePos.y - transform.position.y) - m_delay);
        transform.right = direction;
    }
    
    //Função de tiro do personagem
    private void Shoot()
    {
        if (!Input.GetKey(KeyCode.Q) || !_canShoot) return;
        AudioManager.PlaySound("PlayerShoot");
        var tempBullet = Instantiate(m_playerInfo.PrimaryShoot, _shotPos.position, Quaternion.identity);
        tempBullet.transform.right = transform.right;

        _canShoot = false;
    }
    
    private void ReloadTimer()
    {
        _reloadTime -= Time.deltaTime;

        if (_reloadTime <= 0)
        {
            _canShoot = true;
            _reloadTime = 0.8f;
        }
    }

    #region LifeIssues
    //Função de dano
    public void ApplyDamage(int damage)
    {
        var tmpSpt = gameObject.GetComponentInChildren<SpriteRenderer>();
        tmpSpt.DOColor(Color.red, 0.5f);
        tmpSpt.DOFade(0.8f, 0.5f).OnComplete(()=>tmpSpt.DOColor(Color.white, 0.5f));
        
        m_playerInfo.CurrentLife -= damage;

        HudManager.Instance.HandleOnDamage();
        HudManager.Instance.HandleLogMessages(LogMessageController.MessageType.DamageTaken);

        //Se a vida zerar
        if (m_playerInfo.CurrentLife <= 0)
            GameManager.Instance.RestartScene();
    }


    //Usando kit de reparos
    private void RecoveryKit()
    {
        if (Input.GetKeyDown(KeyCode.R) && m_playerInfo.RecoveryKit >= 1)
        {
            //Se ainda tiver kit pra usar, pode usar
            HudManager.Instance.HandleLogMessages(LogMessageController.MessageType.Recovery);
            AudioManager.PlaySound("RecoveryUse");
            m_playerInfo.CurrentLife += m_playerInfo.RecoveryAmount;
            
            //Se a vida chegar ao máximo quando recuperar, fica no máximo
            if (m_playerInfo.CurrentLife >= m_playerInfo.MaxLife)
                { m_playerInfo.CurrentLife = m_playerInfo.MaxLife;} 
            
            m_playerInfo.RecoveryKit--;

            if (m_playerInfo.RecoveryKit <= 0)
                { m_playerInfo.RecoveryKit = 0;}
        }
        else if(Input.GetKeyDown(KeyCode.R) && m_playerInfo.RecoveryKit <= 0)
        {
            AudioManager.PlaySound("RecoveryOut");
            HudManager.Instance.HandleLogMessages(LogMessageController.MessageType.RecoveryOut);
        }
    }
    
    //Para quando o jogador estiver morrendo e/ou com pouca vida
    private void LowLife()
    {
        AudioManager.PlaySound("PlayerDyingAlert");
        HudManager.Instance.HandleLogMessages(LogMessageController.MessageType.PlayerAlert);
        m_dyingAlertSound = true;
    }

    private void SmokeOn()
    {
        if (m_smokeOn)
        {var tmpSmk = Instantiate(m_smokeEffect, transform.position, Quaternion.identity);}
    }
    #endregion
    
    #region Effects
    private void ApplyEffect()
    {
        for (int i = 0; i < m_currentEffects.Count; i++)
        {
            m_currentEffects[i].RunEffect(this, m_playerInfo);
        }
    }
    
    //Aplicar efeito
    public void AddEffect(PlayerEffects nextEffect)
    {
        //Entrando no novo estado
        nextEffect.EnterEffect(this, m_playerInfo);

        //Rodando o novo efeito
        m_currentEffects.Add(nextEffect);
    }

    //Remover efeito
    public void RemoveEffect(PlayerEffects removeEffect)
    {
        //Saindo do efeito
        removeEffect.ExitEffect(this, m_playerInfo);
        m_currentEffects.Remove(removeEffect);
    }
    
    public void AddMeteorite(PickUpItem.MeteoriteType type, int amount)
    {
        switch (type)
        {
            case PickUpItem.MeteoriteType.Ice:
                m_playerInfo.IceMeteoriteInGame += amount;
                break;
            case PickUpItem.MeteoriteType.Fire:
                m_playerInfo.FireMeteoriteInGame += amount;
                break;
            case PickUpItem.MeteoriteType.Lightinng:
                m_playerInfo.LightningMeteoriteInGame += amount;
                break;
        }
    }
    
    #endregion
    #endregion
}
