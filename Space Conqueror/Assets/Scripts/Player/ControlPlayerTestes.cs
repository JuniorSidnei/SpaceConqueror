using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayerTestes : MonoBehaviour
{
    //Aceleração
    public float m_acceleration;
    
    //Vetor de input somado com tempo e velocidade
    [HideInInspector] public Vector2 _moveVelocity;
    
    //rigidbody do personagem
    private Rigidbody2D m_rb;
    
    
    //The limit rotation
    private float m_Limit = 45f;
    
    //The vector of rotation
    private Vector3 m_currRotation;

    public float delay = 2f;
    //Limits calculated
    private float m_maxZ;
    private float m_minZ;
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_currRotation = transform.eulerAngles;

        m_maxZ = m_currRotation.z + m_Limit;
        m_minZ = m_currRotation.z - m_Limit;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    public void Move()
    {
        //Movimento do jogador
        var _moveInput = Input.GetAxisRaw("Horizontal");
        
        m_rb.drag = Math.Abs(_moveInput) > 0 ? 10 : 1;
            
        m_rb.AddForce(m_acceleration * Time.deltaTime * _moveInput * transform.right, ForceMode2D.Force);
    }

    public void Rotate()
    {
        Vector3 mousPos = Input.mousePosition;
        mousPos = Camera.main.ScreenToWorldPoint(mousPos);
        Vector2 direction = Time.deltaTime * new Vector2((mousPos.x - transform.position.x) - delay, (mousPos.y - transform.position.y) - delay);
        transform.right = direction;
    }
}
