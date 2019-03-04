﻿using System.Collections;
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
    //Gerenciador
    public GameController _gamectr;
    //Objeto do meteoro
    public GameObject[] _meteor;
    //Temporizador para ir diminuindo o tempo de spawn
    private float _timer;
    //Contador de meteoros
    public int _meteorCount = 0;
    //Controlar o meteoro grande
    private bool _bigMeteor = false;
    


    void Update()
    {

       
        //Assim que o dialogo acabar, os meteoros começam
        if (_gamectr._spawnOn)
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

                //Se o meteoro grande aparecer, não tem mais meteoro e fim da fase
                if (_bigMeteor)
                    _gamectr._spawnOn = false;

                //Reduzindo o tempo de spawn do meteoro a cada segundo 
                _spawnTimer = 2.5f - (_timer * 0.025f);

                //Se o tempo de spawn for menor que 0.2, fica em 0.2
                if (_spawnTimer <= 0.5f)
                    _spawnTimer = 0.5f;
            }

           
        }

    }

    //Função de spawn do meteoro
    void SpawnMeteor()
    {
        //Contando os meteoros para o segundo dialogo
        _meteorCount++;

        //Só meteoros pequenos
        if(_meteorCount <= 5)
        {
            var randPos = Random.Range(0, 4);
            //Instanciando o meteoro
            GameObject tempoMeteor = Instantiate(_meteor[randPos], new Vector2(_spawnX, Random.Range(-_spawnY, _spawnY)), Quaternion.identity, transform);
            //Destruindo o meteoro
            Destroy(tempoMeteor, 5f);
        }
        else
        {
            GameObject tempBigMeteor = Instantiate(_meteor[4], new Vector2(10, 0), Quaternion.identity, transform);
            _bigMeteor = true;
        }
        
       
    }
}

