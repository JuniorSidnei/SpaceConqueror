using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using DG.Tweening;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    
    ///<Controle do jogador>
    //Float de posição do personagem
    private Vector2 _moveInput;
    //Vetor de input somado com tempo e velocidade
    [HideInInspector]
    public Vector3 _moveVelocity;
    //rigidbody do personagem
    private Rigidbody2D m_rb;
    
    [Header("Shoot Settings")]
    //Posição do tiro
    public Transform _shotPos;
    //Objeto de tiro
    public GameObject _shoot;
    //Tempo de recarga
    private float _reloadTime = 0.5f;
    //Pode atirar
    private bool _canShoot;
    
    //Status do player
    [Header("Player settings")]
    public PlayerInfo m_playerInfo;
    
    //Vários estados do jogador
    List<PlayerEffects> m_currentEffects;
    ///</Variáveis do jogador>

    ///<Layer para colisões>
    [Header("Collision settings")]
    [SerializeField]
    public LayerMask _colisionLayer;

    private void Awake()
    {
        m_playerInfo.SetControlPlayer(this);
    }

    void Start()
    {
        m_currentEffects = new List<PlayerEffects>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
        ReloadTimer();
        
        if(Input.GetKey(KeyCode.Space) && _canShoot)
            Shoot();

        if (Input.GetKeyDown(KeyCode.R))
            RecoveryKit();   
        
        
        //Aplicando os efeitos ao jogador
        for (int i = 0; i < m_currentEffects.Count; i++)
        {
            m_currentEffects[i].RunEffect(this, m_playerInfo);
        }
    }

    public void Move()
    {
        //Movimento do jogador
        _moveInput  = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        _moveVelocity = m_playerInfo.Speed * Time.deltaTime * _moveInput;
        
        //Movendo o jogador
        m_rb.MovePosition(transform.position + (_moveVelocity * Time.deltaTime));
    }
    
    //Função de tiro do personagem
    public void Shoot()
    {
        AudioManager.PlaySound("PlayerShoot");
        GameObject tempBullet =  Instantiate(_shoot, _shotPos.position, Quaternion.identity);
        tempBullet.transform.right = Vector3.right;
        
        _canShoot = false;
    }

    private void ReloadTimer()
    {
        _reloadTime -= Time.deltaTime;

        if (_reloadTime <= 0)
        {
            _canShoot = true;
            _reloadTime = 0.5f;
        }
    }

    //Função de dano
    public void ApplyDamage(int damage)
    {
        m_playerInfo.CurrentLife -= damage;

        //Se a vida zerar
        if (m_playerInfo.CurrentLife <= 0)
            GameManager.Instance.RestartScene();
    }


    //Usando kit de reparos
    private void RecoveryKit()
    {
        if (m_playerInfo.RecoveryKit >= 1)
        {
            //Se ainda tiver kit pra usar, pode usar
            m_playerInfo.CurrentLife += m_playerInfo.RecoveryAmount;
            m_playerInfo.RecoveryKit--;
            
            if (m_playerInfo.RecoveryKit <= 0)
                m_playerInfo.RecoveryKit = 0;
        }
        else
        {
            Debug.Log("ACABOU O KIT SE FUDEU: " + m_playerInfo.RecoveryKit);
        }

        //Se a vida chegar ao máximo quando recuperar, fica no máximo
        if (m_playerInfo.CurrentLife >= m_playerInfo.MaxLife)
            m_playerInfo.CurrentLife = m_playerInfo.MaxLife;
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
    

    //Colisões do jogador
    private void OnCollisionEnter2D(Collision2D obj)
    {
        //Layer 13, tudo referente ao boss
        if (obj.gameObject.layer == 13)
        {
            //Tiro do Boss
            if (!obj.gameObject.CompareTag("BossBullet"))
            {
                CameraController.Instance.ScreenShake();
                HudManager.Instance.HandleOnDamage();
                AudioManager.PlaySound("BossCollision");
                ApplyDamage(obj.gameObject.GetComponent<StandardBullet>()._damage);
                Destroy(obj.gameObject);
            }

            //Corpo do Boss
            if (!obj.gameObject.CompareTag("Boss"))
            {
                CameraController.Instance.ScreenShake();
                HudManager.Instance.HandleOnDamage();
                AudioManager.PlaySound("BossCollision");
                ApplyDamage(obj.gameObject.GetComponent<KrasLosnas>().GetBodyDamage);
            }
        }
    }
}
