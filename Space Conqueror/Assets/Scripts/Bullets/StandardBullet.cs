using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    //Velocidade da bala
    public float _speed;
    //Dano do tiro
    public int _damage;
    
    void Update()
    {
        //Movendo o tiro para direita
        transform.position += Vector3.right * _speed * Time.deltaTime;
        //Destruindo caso não atinga nada
        Destroy(gameObject, 3f);
    }
}
