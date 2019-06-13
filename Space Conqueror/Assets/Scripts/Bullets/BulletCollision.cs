using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esse script serve para que o objeto verifique a colisão com quem desejar e instanciar uma particula e se destruir
public class BulletCollision : MonoBehaviour
{
   
    public GameObject m_particleCollision;
    public string m_tagObject;
    public int m_numberLayer;

    private void OnCollisionEnter2D(Collision2D obj)
    {
        //Tiro do Boss no player
        if (m_numberLayer == 10)
        {
            if (obj.gameObject.CompareTag(m_tagObject))
            {
                obj.gameObject.GetComponent<ControlPlayer>().ApplyDamage(gameObject.GetComponent<StandardBullet>()._damage);
                InstantiateAndDestroy(m_particleCollision, transform);
            }
        }

        //Tiro do player no Boss
        if(m_numberLayer == 13)
        {
            //if (obj.gameObject.CompareTag(m_tagObject))
                InstantiateAndDestroy(m_particleCollision, transform);
        }
    }

    private void InstantiateAndDestroy(GameObject tempptc, Transform transfom)
    {
        tempptc = Instantiate(m_particleCollision, transform.position, Quaternion.identity);
        Destroy(this);
    }
}
