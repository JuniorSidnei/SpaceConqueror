using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorStatus : MonoBehaviour
{
    //Velocidade do meteoro
    public float _meteorSpeed;
    //Tempo do jogo em segundos pra somar coma  velocidade do meteoro
    private float _timer;
    //Vida do meteoro, que vai ser um random de 50 e 80
    public int _meteorLife;
    //Vetor temporário para a escala do meteoro
    private Vector3 _tempScale;
    //Referencia ao rigidbody
    //private Rigidbody2D _meteorRb;
    //Quantidade de dano de cada meteoro
    public int _meteorDamage = 20;

    void Start()
    {
        
        //Vida dos meteoros quando nascem
        _meteorLife = 100;
        

    }


    void Update()
    {
        

        //Rotacionando e movendo o meteoro
        transform.position += Vector3.left * _meteorSpeed * Time.deltaTime;
        transform.Rotate(0, 0, Random.Range(2, 6));

        //Verificando a vida e destruindo o meteoro
        if (_meteorLife <= 0)
            Destroy(gameObject);

        Debug.Log("Vida do meteoro: " + _meteorLife);
            
    }

    //Função que retorna o valor do meteoro para o dano
    public int getDamage() { return _meteorDamage; }
        

}

   
