﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorStatus : MonoBehaviour
{
    //Descrição do tipo de meteoro
    public enum MeteorType
    {
        Fire, Ice, Thunder, Normal
    };

    //Velocidade do meteoro
    public float _meteorSpeed;
    //Tempo do jogo em segundos pra somar coma  velocidade do meteoro
    private float _timer;
    //Vida do meteoro, que vai ser um random de 50 e 80
    public int _meteorLife;
    //Tipo de meteoro
    public MeteorType type;

    void Update()
    {

        //Movendo o meteoro
        transform.position += Vector3.left * _meteorSpeed * Time.deltaTime;
       

        //Verificando a vida e destruindo o meteoro
        if (_meteorLife <= 0)
            Destroy(gameObject);

        if(_meteorLife <= 0 && gameObject.CompareTag("BigMeteor"))
        {
            FindObjectOfType<DialogueManager>()._secondDialogue = true;
        }

      
            
    }
   

    //Colisão com o jogador
    private void OnCollisionEnter2D(Collision2D obj)
    {
        //Tiro do jogador
        if(obj.gameObject.layer == 11)
        {
            //Aplicando dano na vida do meteoro
            _meteorLife -= obj.gameObject.GetComponent<StandardBullet>()._damage;

            //Destruindo o tiro
            Destroy(gameObject);  
        }

        //colisão com jogador
        if (obj.gameObject.layer == 10)
        {
                //Verificando o tipo de meteoro para aplicar o efeito correto
                switch (type)
                {
                    //Gelo
                    case MeteorType.Ice:

                        obj.gameObject.GetComponent<ControlPlayer>().AddEffect(new FreezingEffect());
                        break;

                    //Fogo
                    case MeteorType.Fire:

                        obj.gameObject.GetComponent<ControlPlayer>().AddEffect(new FlamingEffect());
                    break;

                    //Raio
                    case MeteorType.Thunder:

                        obj.gameObject.GetComponent<ControlPlayer>().AddEffect(new LightningEffect());
                    break;
                }

                //Destruindo meteoro
                Destroy(gameObject);
            
        }
    }


}

   