using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    ///<Eventos>
    public delegate void DamageDelegate(float currentLife);
    public  event DamageDelegate DamageEvent;

    //Evento do primeiro dialogo do jogo
    public delegate void DialogueDelegate();
    public  event DialogueDelegate DialogueEvent;
    /// </Eventos>

    ///<Variáveis do jogador>
    //Velocidade da nave
    private float _speed;
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
    private float _fireTime;
    //Vida do jogdor
    public int _life;
    //Vida maxima do jogador
    private int _maxLife = 500;
    //Tempo de gelo
    private float _statusTimer = 0;
    //Se esta congelado
    private bool _freeze = false;
    //Se está com fogo
    private bool _flame = false;
    //Se está com raio
    private bool _lightning = false;
    ///</Variáveis do jogador>


    void Start()
    {
        _life = _maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimento do jogador
        _moveInput =  new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
        _moveVelocity = _moveInput * _speed * Time.deltaTime;

        //Movendo o jogador
        transform.position += _moveVelocity;

        //Se for atingido pelos meteoros de gelo, fogo ou raio
        if (_freeze)
            FreezingStatus();
        else if (_flame)
            FlamingStatus();
        else if (_lightning)
            LightningStatus();
        else
            NormalStatus();


    }

    //Função de tiro do personagem
    public void Shoot()
    {
        //Instancia copiada de tiro
       GameObject tempBullet =  Instantiate(_shoot, _shotPos.position, Quaternion.identity, transform);
        
    }

    //Colisão
    private void OnCollisionEnter2D(Collision2D obj)
    {
        //Se o jogador colidir com o meteoro
        if(obj.gameObject.CompareTag("Meteor"))
        {
            //Nome do meteoro
            var _name = obj.gameObject.name;

            //Meteoro de gelo
            if (_name.Equals("FreezingMeteor(Clone)"))
                _freeze = true;
               
            //Meteoro de fogo
            if (_name.Equals("FlamingMeteor(Clone)"))
                _flame = true;
               
            //Meteoro de raio
            if (_name.Equals("LightnigMeteor(Clone)"))
                _lightning = true;
               

            //Aplicando o valor de dano quando bate no meteoro
            ApplyDamage(obj.collider.GetComponent<MeteorStatus>().getDamage());
            Destroy(obj.gameObject);
        }

        //Evento de primeiro dialogo do jogo
        if (obj.gameObject.CompareTag("DialogueTrigger"))
        {
            //chamando o evento de dialogo
            if (DialogueEvent != null)
            {
                DialogueEvent.Invoke();
            }

            //Destruindo os componentes do objeto
            Destroy(obj.gameObject.GetComponent<BoxCollider2D>());
            Destroy(obj.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    


    //Função de dano
    public void ApplyDamage(int damage)
    {
        _life -= damage;
        
        //Chamada do evento de dano
        if (DamageEvent != null)
            DamageEvent.Invoke((float)_life / _maxLife);
        
        //Se a vida zerar
        if (_life <= 0)
            Destroy(gameObject); 
    }

    //Status normal do jogador
    void NormalStatus()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        _speed = 10f;
    }

    ///Função quando atingido por meteoros que dão efeitos
    //Vai deixar o jogador lento por dois segundos
    void FreezingStatus()
    {
        //Debuff visual e de velocidade, começar animação de gelo
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        _speed = 5f;

        //Tirando o tempo de dois segundos
        _statusTimer += Time.deltaTime;


        //Enquanto for maior que o tempo vai ficar com debuff, terminar animação de gelo
        if (_statusTimer >= 2f)
        {
            _freeze = false;
            _statusTimer = 0;
        }
    }

    ///Função quando atingido pelo meteoro de raio
    //Vai deixar a nave parada por um segundo
    void LightningStatus()
    {
        //Começar animação de raio
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        _speed = 0;

        //Contando o tempo
        _statusTimer += Time.deltaTime;

        //Terminar animação de raio
        if (_statusTimer >= 1f)
        {
            _lightning = false;
            _statusTimer = 0;
        }
    }

    ///Função quando atingido pelo meteoro de fogo
    //Vai dar dano de fogo por dois segundos
    void FlamingStatus()
    {
        //Deixando a sprite vermelha por causa do fogo, deixar a animação de fogo começando aqui
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;

        //Contando o tempo
        _statusTimer += Time.deltaTime;

        //Dps de fogo
        if (_statusTimer >= 0.5f)
        {
            StartCoroutine(FlamingDamage());
        }


    }

    IEnumerator FlamingDamage()
    {
        _statusTimer = 0;
        _life -= 5;
        if (DamageEvent != null)
            DamageEvent.Invoke((float)_life / _maxLife);

        yield return new WaitForSeconds(0.5f);
        _life -= 5;
        if (DamageEvent != null)
            DamageEvent.Invoke((float)_life / _maxLife);

        _flame = false;
       

    }

}
