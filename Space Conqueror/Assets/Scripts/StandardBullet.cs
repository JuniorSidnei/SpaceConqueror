using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    //Velocidade da bala
    private float _speed = 50f;
    //Dano do tiro
    public int _damage;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        //Movendo o tiro para direita
        transform.position += Vector3.right * _speed * Time.deltaTime;
        //Destruindo caso não atinga nada
        Destroy(gameObject, 3f);
    }

    //Colisões
    private void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.CompareTag("Meteor"))
        {

            //Destruindo o tiro
            Destroy(gameObject);
            //Aplicando dano na vida do meteoro
            obj.gameObject.GetComponent<MeteorStatus>()._meteorLife -= _damage;
        }
    }
}
