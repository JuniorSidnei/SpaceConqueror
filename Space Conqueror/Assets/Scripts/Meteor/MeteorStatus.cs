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
    public GameObject _meteorSpread;
  

    void Update()
    {

        //Movendo o meteoro
        transform.position += _meteorSpeed * Time.deltaTime * Vector3.left;


        //Verificando a vida e destruindo o meteoro
        if (_meteorLife <= 0 && !gameObject.CompareTag("MiningMeteor"))
        {
            AudioManager.PlaySound("MeteorExplosion");
            //Explosão de efeito do meteoro
            Destroy(gameObject);
            var tempDying = Instantiate(_dyingMeteor, transform.position, Quaternion.identity);
            //Explosão de impacto
            var tempDying2 = Instantiate(_WaveExplosion, transform.position, Quaternion.Euler(-90, 0, 0));
            
            MapCreator.Instance.ClearUnityObject(name);
        }  
    }


    protected abstract void OnCollision(Collision2D obj);
    protected abstract void OnCollisionWithPlayer(ControlPlayer player);
    
    //Colisão com o tiro
    private void OnCollisionEnter2D(Collision2D obj)
    {
        //Tiro do jogador
        if(obj.gameObject.layer == 11)
        {
            //Aplicando dano na vida do meteoro
            _meteorLife -= obj.gameObject.GetComponent<StandardBullet>()._damage;
            
            GameObject tempSpread = Instantiate(_meteorSpread, obj.contacts[0].point, Quaternion.identity);
            
            AudioManager.PlaySound("BulletMeteorCollision");
            
            OnCollision(obj);
            
            //Destruindo o tiro
            Destroy(obj.gameObject);
//           
        }

        //colisão com jogador
        if (obj.gameObject.layer == 10 && !gameObject.CompareTag("MiningMeteor"))
        {
            OnCollisionWithPlayer(obj.gameObject.GetComponent<ControlPlayer>());

            //Destruindo meteoro
            AudioManager.PlaySound("MeteorExplosion");
            Destroy(gameObject);
        }
    }



}

   
