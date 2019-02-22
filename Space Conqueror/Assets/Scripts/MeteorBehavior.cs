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
    public float _spawnTimer = 2.5f;
    //Objeto do meteoro
    public GameObject _meteor;
    //Temporizador para ir diminuindo o tempo de spawn
    public float _timer;



    void Update()
    {



        //Tempo de spawn de um meteoro pra outro que tbm vai somar com o tempo
        _spawnTimer -= Time.deltaTime;
        //tempo do jogo
        _timer += Time.deltaTime; 

        //Se o spawn zerar, cria um meteoro
        if (_spawnTimer <= 0)
        {
            //Chamando função do meteoro
            SpawnMeteor();
            //Reduzindo o tempo de spawn do meteoro a cada segundo 
            _spawnTimer = 2.5f - (_timer * 0.025f);

            //Se o tempo de spawn for menor que 0.2, fica em 0.2
            if (_spawnTimer <= 0.2f)
                _spawnTimer = 0.2f;

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

