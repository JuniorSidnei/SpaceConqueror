using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
//    ///<Eventos>
//    public delegate void DamageDelegate(float currentLife);
//    public  event DamageDelegate DamageEvent;
//    /// </Eventos>

    ///<Controle do jogador>
    //Joystick de mobile
    //public Joystick joystick;
    //Float de posição do personagem
    private Vector2 _moveInput;
    //Vetor de input somado com tempo e velocidade
    private Vector3 _moveVelocity;
    //Posição do tiro
    public Transform _shotPos;
    //Objeto de tiro
    public GameObject _shoot;
    //Tempo de recarga
    private float _reloadTime = 0.5f;
    //Pode atirar
    private bool _canShoot;
    //Status do player
    public PlayerInfo m_playerInfo;
    
    //Vários estados do jogador
    List<PlayerEffects> m_currentEffects;
    //Particula quando atira
    public GameObject m_ptcShooting;

    public GameObject m_player;
    ///</Variáveis do jogador>

    ///<Layer para colisões>
    //layer publica
    [SerializeField]
    public LayerMask _colisionLayer;

    private bool m_iddleStop;

    ///</Layer>
        

    void Start()
    {
        m_currentEffects = new List<PlayerEffects>();
        //AudioManager.PlaySound("PlayerEngine");
    }

    // Update is called once per frame
    void Update()
    {
        
        
        Move();
        ReloadTimer();
        
        if(Input.GetKey(KeyCode.Space) && _canShoot)
            Shoot();

//        if (Input.GetKey(KeyCode.R) && m_playerInfo.RecoveryKit >= 0)
//        {
//            Debug.Log("Kit de reparos antes do uso: " + m_playerInfo.RecoveryKit);
//            RepairKit(m_playerInfo.RecoveryAmount);
//        }
        
        
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
        transform.position += _moveVelocity * Time.deltaTime;

       
//        if (_moveVelocity.magnitude <= 0)
            //DoIddle();
            
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
//    private void RepairKit(int recover)
//    {
//        
//        //Se ainda tiver kit pra usar, pode usar
//        m_playerInfo.CurrentLife += recover;
//        m_playerInfo.RecoveryKit--;
//        Debug.Log("Kit de reparos depois do uso: " + m_playerInfo.RecoveryKit);
//           
//        
//        
//        //Se a vida chegar ao máximo quando recuperar, fica no máximo
//        if (m_playerInfo.CurrentLife >= m_playerInfo.MaxLife)
//            m_playerInfo.CurrentLife = m_playerInfo.MaxLife;
//    }

    
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
        if(obj.gameObject.layer == 13)
        {
            //Tiro do Boss
            if(obj.gameObject.CompareTag("BossBullet"))
            {
                AudioManager.PlaySound("BossCollision");
                ApplyDamage(obj.gameObject.GetComponent<StandardBullet>()._damage);
                Destroy(obj.gameObject);
            }

            //Corpo do Boss
            if(obj.gameObject.CompareTag("Boss"))
            {
                AudioManager.PlaySound("BossCollision");
                ApplyDamage(obj.gameObject.GetComponent<KrasLosnas>().GetBodyDamage());
            }
        }
    }
}
