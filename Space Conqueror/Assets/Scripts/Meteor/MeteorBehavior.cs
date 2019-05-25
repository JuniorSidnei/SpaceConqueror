using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MeteorBehavior : MonoBehaviour
{
    
    //Posição e limite X de spawn
    public float _spawnX;
    //Posição e limite Y de spawn
    public float _spawnY;
    //Tempo de spawn
    public float _spawnTimer = 2.5f;
    //Gerenciador
    //public DialogueManager _dialoguemng;
    //Objeto do meteoro
    [FormerlySerializedAs("_meteor")] public GameObject[] _meteors;
    //Temporizador para ir diminuindo o tempo de spawn
    private float _timer;
    //Contador de meteoros
    private int _meteorCount = 0;
    


    void Update()
    {
        
        if (GameManager.Instance.MeteorOn())
        {
            //Tempo do jogo
            _timer += Time.deltaTime;
            //Tempo de spawn de um meteoro pra outro que tbm vai somar com o tempo
            _spawnTimer -= Time.deltaTime;

            //Se o spawn zerar, cria um meteoro
            if (_spawnTimer <= 0)
            {
                //Chamando função do meteoro
                SpawnMeteor();

                //Reduzindo o tempo de spawn do meteoro a cada segundo 
                _spawnTimer = 2.5f - (_timer * 0.025f);

                //Se o tempo de spawn for menor que 0.8, fica em 0.8
                if (_spawnTimer <= 0.8f)
                    _spawnTimer = 0.8f;
            }
        }
    }

    //Função de spawn do meteoro
    void SpawnMeteor()
    {
       
        _meteorCount++;

        //Só meteoros pequenos
        if (_meteorCount <= 50)
        {
            var randPos = Random.Range(0, 4);
            //Instanciando o meteoro
            GameObject tempoMeteor = Instantiate(_meteors[randPos], new Vector2(_spawnX, Random.Range(-_spawnY, _spawnY)), Quaternion.identity, transform);
            //Destruindo o meteoro
            Destroy(tempoMeteor, 10f);
        }
        else
        {
            GameManager.Instance.SetMeteorOver(true);
        }
    }
}

