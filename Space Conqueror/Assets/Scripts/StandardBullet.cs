using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    //Velocidade da bala
    private float _speed = 50f;
    //Referência rigidbody
    private Rigidbody2D bulletRb;

    
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        bulletRb.MovePosition(transform.position + transform.right * _speed * Time.deltaTime);
        Destroy(gameObject, 3f);
    }
}
