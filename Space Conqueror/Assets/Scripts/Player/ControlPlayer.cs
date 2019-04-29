using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    ///<Eventos>
    public delegate void DamageDelegate(float currentLife);
    public  event DamageDelegate DamageEvent;
    /// </Eventos>

    ///<Variáveis do jogador>
    //Velocidade da nave
    private float _speed = 15f;
    //Joystick de mobile
    public Joystick joystick;
    //Float de posição do personagem
    private Vector2 _moveInput;
    //Vetor de input somado com tempo e velocidade
    private Vector3 _moveVelocity;
    //Posição do tiro
    public Transform _shotPos;
    //Objeto de tiro
    public GameObject _shoot;
    //Tempo de recarga
    private float _reloadTime;
    //Vida do jogdor
    public float _life;
    //Vida maxima do jogador
    public int _maxLife = 500;
    //1/3 vida do jogador
    private int _halfLife;
    //Kit de reparos
    [SerializeField]public bool _repairUsed;
    //Vários estados do jogador
    List<PlayerEffects> m_currentEffects;
    ///</Variáveis do jogador>

    ///<Layer para colisões>
    //layer publica
    [SerializeField]
    public LayerMask _colisionLayer;
    ///</Layer>
        

    void Start()
    {
        _life = _maxLife;
        _halfLife = _maxLife / 3;
        _repairUsed = true;

       m_currentEffects = new List<PlayerEffects>();

    }

    // Update is called once per frame
    void Update()
    {
        //Movimento do jogador
        _moveInput =  new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
        _moveVelocity = _moveInput * _speed * Time.deltaTime;

        //Movendo o jogador
        transform.position += _moveVelocity;

       
        //Aplicando os efeitos ao jogador
        for (int i = 0; i < m_currentEffects.Count; i++)
        {
            m_currentEffects[i].RunEffect(this);
        }


        if (_life <= _halfLife)
        {
            //Chamando a função que vai dar o debuff
            ErrorSystem();
        }
    }

    //Função de tiro do personagem
    public void Shoot()
    { GameObject tempBullet =  Instantiate(_shoot, _shotPos.position, Quaternion.identity, transform); }

   

    //Função de dano
    public void ApplyDamage(float damage)
    {
        _life -= damage;

        //var halfLife = _maxLife / 2;

        //Chamada do evento de dano
        if (DamageEvent != null)
            DamageEvent.Invoke((float)_life / _maxLife);
        
        //Se a vida zerar
        if (_life <= 0)
            Destroy(gameObject); 
    }

    //Usando kit de reparos
    public void RepairKit(int recover)
    {
        _life += recover;

        //Se a vida chegar ao máximo quando recuperar, fica no máximo
        if (_life >= _maxLife)
            _life = _maxLife;

        //Chamada do evento de dano
        if (DamageEvent != null)
            DamageEvent.Invoke((float)_life / _maxLife);

        //Chamando evento de reparos
        _repairUsed = false;
    }

    public void ErrorSystem()
    {
        //Diminuir a velocidade
        _speed = 10f;
        //E aplicar um efeito de animação de vidro quebrado pra dificultar o personagem a enxergar
        //além da mensagem de que a nave está com problemas técnicos
    }

   

    //Aplicar efeito
    public void AddEffect(PlayerEffects nextEffect)
    {
        //Entrando no novo estado
        nextEffect.EnterEffect(this);

        //Rodando o novo efeito
        m_currentEffects.Add(nextEffect);
    }

    //Remover efeito
    public void RemoveEffect(PlayerEffects removeEffect)
    {
        //Saindo do efeito
        removeEffect.ExitEffect(this);
        m_currentEffects.Remove(removeEffect);

    }

    //Setando speed
    public void SetSpeed(float speed)
    {  _speed = speed;  }

    //Colisões do jogador
    private void OnCollisionEnter2D(Collision2D obj)
    {
        //Layer 13, tudo referente ao boss
        if(obj.gameObject.layer == 13)
        {
            //Tiro do Boss
            if(obj.gameObject.CompareTag("BossBullet"))
            {
                ApplyDamage(obj.gameObject.GetComponent<StandardBullet>()._damage);
                Destroy(obj.gameObject);
            }

            //Corpo do Boss
            if(obj.gameObject.CompareTag("Boss"))
            {
                ApplyDamage(30);
            }
        }
    }
}
