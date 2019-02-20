using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehavior : MonoBehaviour
{
    //Posição e limite X de spawn
    public float _spawnX;
    //Posição e limite Y de spawn
    public float _spawnY;
    //Tempo de spawn
    private float _spawnTimer = 1f;
    //Objeto do meteoro
    public GameObject _meteor;
    

    
    void Update()
    {
        

        //Tempo de spawn de um meteoro pra outro que tbm vai somar com o tempo
        _spawnTimer -= Time.deltaTime;

        if(_spawnTimer <= 0)
        {
            SpawnMeteor();
            _spawnTimer = 1f;
        }

    }

    //Função de spawn do meteoro
    void SpawnMeteor()
    {
        
        //Instanciando o meteoro
        GameObject tempoMeteor = Instantiate(_meteor, new Vector2(_spawnX, Random.Range(-_spawnY, _spawnY)), Quaternion.identity, transform);
        //Destruindo o meteoro
        Destroy(tempoMeteor, 5f);
    }

}
