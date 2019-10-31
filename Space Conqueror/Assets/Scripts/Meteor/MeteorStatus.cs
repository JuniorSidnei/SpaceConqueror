using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeteorStatus : MonoBehaviour
{
    //Vida do meteoro, que vai ser um random de 50 e 80
    public int _meteorLife;
    public bool IsLootable;
    [Header("LootItem")]
    public GameObject LootItem;
    
    [Header("Particles Effects")]
    public GameObject _dyingMeteor;
    public GameObject _WaveExplosion;
    public GameObject _bulletHit;
    public GameObject _meteorSpread;

    

    void Update()
    {
        //Verificando a vida e destruindo o meteoro
        if (_meteorLife <= 0)
        {
            AudioManager.PlaySound("MeteorExplosion");
            //Limpando do mapa o objeto
            MapCreator.Instance.ClearUnityObject(name);
            //Explosão de efeito do meteoro
            Destroy(gameObject);
            var tempDying = Instantiate(_dyingMeteor, transform.position, Quaternion.identity);
            //Explosão de impacto
            var tempDying2 = Instantiate(_WaveExplosion, transform.position, Quaternion.Euler(-90, 0, 0));

            //spawn loot item depois de explodir
            SpawnLootItem();
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
            GameObject tempSpread = Instantiate(_meteorSpread, obj.contacts[0].point, Quaternion.identity);
            
            AudioManager.PlaySound("BulletMeteorCollision");
            
            OnCollision(obj);
            
            //Destruindo o tiro
            Destroy(obj.gameObject);
        }

        //colisão com jogador
        if (obj.gameObject.layer == 10)
        {
            OnCollisionWithPlayer(obj.gameObject.GetComponent<ControlPlayer>());

            //Destruindo meteoro
            AudioManager.PlaySound("MeteorExplosion");
            MapCreator.Instance.ClearUnityObject(name);
            Destroy(gameObject);
        }
    }


    private void SpawnLootItem()
    {
        if (IsLootable)
            Instantiate(LootItem, transform.position, Quaternion.identity);

    }

}

   
