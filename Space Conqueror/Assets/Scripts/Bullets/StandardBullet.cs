using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    //Velocidade da bala
    public float _speed = 50f;
    //Dano do tiro
    public int _damage;
    
    void Update()
    {
        //Movendo o tiro para direita
        transform.position += Vector3.right * _speed * Time.deltaTime;
        //Destruindo caso não atinga nada
        Destroy(gameObject, 3f);
    }

    //Colisões
    //private void OnCollisionEnter2D(Collision2D obj)
    //{
    //    if(obj.gameObject.layer == 8)
    //    {
    //        //Meteoro de gelo
    //        if (obj.gameObject.CompareTag("FreezingMeteor"))
    //            Onhit(Color.blue);

    //        //Meteoro de fogo
    //        if (obj.gameObject.CompareTag("FlamingMeteor"))
    //            Onhit(Color.red);

    //        //Meteoro de raio
    //        if (obj.gameObject.CompareTag("LightningMeteor"))
    //            Onhit(Color.yellow);

    //        //Meteoro normal
    //        if (obj.gameObject.CompareTag("Meteor"))
    //            Onhit(Color.gray);

    //        //Destruindo o tiro
    //        Destroy(gameObject);

    //        //Aplicando dano na vida do meteoro
    //        obj.gameObject.GetComponent<MeteorStatus>()._meteorLife -= _damage;
    //    }
    //}

    ////Define os parametros para isntanciar a particula de colisão
    //void Onhit(Color color)
    //{
    //    //Instancia
    //    GameObject tempptc =  Instantiate(_meteorHit, transform.position, Quaternion.identity);
    //    //Muda a cor
    //    tempptc.gameObject.GetComponent<ParticleSystem>().startColor = color;
        
    //    //Destroi
    //    Destroy(tempptc, 1f);
    //}
}
