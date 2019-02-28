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
    public float _speed;
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
    private int _life = 300;

    ///</Variáveis do jogador>

  

    // Update is called once per frame
    void Update()
    {
        //Movimento do jogador
        _moveInput =  new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
        _moveVelocity = _moveInput * _speed * Time.deltaTime;

        //Movendo o jogador
        transform.position += _moveVelocity;

        
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
            DamageEvent.Invoke((float)_life / 200);
        
        //Se a vida zerar
        if (_life <= 0)
            Destroy(gameObject); 
    }
}
