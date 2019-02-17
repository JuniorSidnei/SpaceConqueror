using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    //Velocidade da nave
    public float _speed;
    //Joystick de mobile
    public Joystick joystick;
    //Float de posição do personagem
    private Vector2 _moveInput;
    //Vetor de input somado com tempo e velocidade
    private Vector3 _moveVelocity;
    //Referência ao rigidbody
    private Rigidbody2D _playerRb;
    //Posição do tiro
    public Transform _shotPos;
    //Objeto de tiro
    public GameObject _shoot;
    //Tempo de recarga
    private float _fireTime;
    
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput =  new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
        _moveVelocity = _moveInput * _speed * Time.deltaTime;

        _playerRb.MovePosition(transform.position + _moveVelocity);
        
        
    }

    public void Shoot()
    {
       GameObject tempBullet =  Instantiate(_shoot, _shotPos.position, Quaternion.identity, transform);
        
    }
}
