using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esse script serve para que o objeto verifique a colisão com quem desejar e instanciar uma particula e se destruir
public class BulletCollision : MonoBehaviour
{
   
    public GameObject m_particleCollision;
    

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.layer == 10)
        {
            if(obj.gameObject.CompareTag("Player"))
            {
                obj.gameObject.GetComponent<ControlPlayer>().ApplyDamage(gameObject.GetComponent<StandardBullet>()._damage);
                GameObject tempptc = Instantiate(m_particleCollision, transform.position, Quaternion.identity);
                Destroy(this);
            }
        }
    }
}
