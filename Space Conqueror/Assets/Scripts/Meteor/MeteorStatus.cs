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
            AudioManager.PlaySound("MeteorExplosion");
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
                    
                    AudioManager.PlaySound("BulletMeteorCollision");
                    GameObject tempHit = Instantiate(_bulletHit, transform.position, Quaternion.identity);
                    break;

                //Fogo
                case MeteorType.Fire:

                    AudioManager.PlaySound("BulletMeteorCollision");
                    GameObject tempHit2 = Instantiate(_bulletHit, transform.position, Quaternion.identity);
                    break;

                //Raio
                case MeteorType.Thunder:

                    AudioManager.PlaySound("BulletMeteorCollision");
                    GameObject tempHit3 = Instantiate(_bulletHit, transform.position, Quaternion.identity);
                    break;
                //Normal
                case MeteorType.Normal:

                    AudioManager.PlaySound("BulletMeteorCollision");
                    GameObject tempHit4 = Instantiate(_bulletHit, transform.position, Quaternion.identity);
                    break;
            }

            //Destruindo o tiro
            Destroy(obj.gameObject);
        }

        //colisão com jogador
        if (obj.gameObject.layer == 10)
        { 
            OnCollisionWithPlayer(obj.gameObject.GetComponent<ControlPlayer>());
            
            //Destruindo meteoro
            AudioManager.PlaySound("MeteorExplosion");
            Destroy(gameObject);
            
        }
    }

    protected abstract void OnCollisionWithPlayer(ControlPlayer player);

}

   
