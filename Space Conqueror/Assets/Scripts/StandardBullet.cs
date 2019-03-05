using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    //Velocidade da bala
    private float _speed = 50f;
    //Dano do tiro
    public int _damage;
    //Particula de colisão com meteoro
    public GameObject _meteorHit;
    
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
            GameObject tempHit = Instantiate(_meteorHit, transform.position, Quaternion.identity);
            Debug.Log("CARAAAAAAALHHHHHHHHHHOOOOOOOOOOOOOOOOOOOOOO" + tempHit != null);
            Destroy(tempHit, 1f);
            //Destruindo o tiro
            Destroy(gameObject);
            //Aplicando dano na vida do meteoro
            obj.gameObject.GetComponent<MeteorStatus>()._meteorLife -= _damage;
        }
    }
}
