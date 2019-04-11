using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeteorStatus : MonoBehaviour
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
    //Destruindo o meteoro
    public GameObject _dyingMeteor;
    public GameObject _WaveExplosion;
    public GameObject _bulletHit;
  

    void Update()
    {

        //Movendo o meteoro
        transform.position += Vector3.left * _meteorSpeed * Time.deltaTime;


        //Verificando a vida e destruindo o meteoro
        if (_meteorLife <= 0)
        {
            //Explosão de efeito do meteoro
            Destroy(gameObject);
            GameObject tempDying = Instantiate(_dyingMeteor, transform.position, Quaternion.identity); 
            Destroy(tempDying, 1f);
            //Explosão de impacto
            GameObject tempDying2 = Instantiate(_WaveExplosion, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(tempDying2, 1f);
        }  
    }
   

    //Colisão com o tiro
    private void OnCollisionEnter2D(Collision2D obj)
    {
        //Tiro do jogador
        if(obj.gameObject.layer == 11)
        {
            //Aplicando dano na vida do meteoro
            _meteorLife -= obj.gameObject.GetComponent<StandardBullet>()._damage;
          
            switch (type)
            {
                //Gelo
                case MeteorType.Ice:

                    GameObject tempHit = Instantiate(_bulletHit, transform.position, Quaternion.identity);
                    Destroy(tempHit, 1f);
                    break;

                //Fogo
                case MeteorType.Fire:

                    GameObject tempHit2 = Instantiate(_bulletHit, transform.position, Quaternion.identity);
                    Destroy(tempHit2, 1f);
                    break;

                //Raio
                case MeteorType.Thunder:

                    GameObject tempHit3 = Instantiate(_bulletHit, transform.position, Quaternion.identity);
                    Destroy(tempHit3, 1f);
                    break;
                //Normal
                case MeteorType.Normal:

                    GameObject tempHit4 = Instantiate(_bulletHit, transform.position, Quaternion.identity);
                    Destroy(tempHit4, 1f);
                    break;
            }

            //Destruindo o tiro
            Destroy(obj.gameObject);
        }

        //colisão com jogador
        if (obj.gameObject.layer == 10)
        {
            OnCollisionWithPlayer(obj.gameObject.GetComponent<ControlPlayer>());
           
                ////Verificando o tipo de meteoro para aplicar o efeito correto
                //switch (type)
                //{
                //    //Gelo
                //    case MeteorType.Ice:

                //        obj.gameObject.GetComponent<ControlPlayer>().AddEffect(new FreezingEffect());
                //        break;

                //    //Fogo
                //    case MeteorType.Fire:

                //        obj.gameObject.GetComponent<ControlPlayer>().AddEffect(new FlamingEffect());
                //    break;

                //    //Raio
                //    case MeteorType.Thunder:

                //        obj.gameObject.GetComponent<ControlPlayer>().AddEffect(new LightningEffect());
                //    break;
                //}

                //Destruindo meteoro
                Destroy(gameObject);
            
        }
    }

    protected abstract void OnCollisionWithPlayer(ControlPlayer player);

}

   
