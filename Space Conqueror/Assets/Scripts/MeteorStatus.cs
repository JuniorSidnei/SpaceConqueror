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
        //Pegando rigidbody
        // _meteorRb = GetComponent<Rigidbody2D>();
        //Vida dos meteoros quando nascem
        _meteorLife = Random.Range(50, 80);
        //Atribuindo o valor da escala ao vetor temporario
        _tempScale = transform.localScale;
        //Atribuindo uma escala aleátoria para o meteoro
        _tempScale.x = Random.Range(0.4f, 1f);
        _tempScale.y = Random.Range(0.4f, 1f);
        //Retornando o valor alterado para a escala do objeto
        transform.localScale = _tempScale;


    }


    void Update()
    {
        //Tempo do jogo em segundos
        _timer += Time.deltaTime;

        //Rotacionando e movendo o meteoro
        transform.position += Vector3.left * _meteorSpeed * Time.deltaTime;
        transform.Rotate(0, 0, Random.Range(2, 6));

        //Movendo o meteoro 
        //_meteorRb.MovePosition(transform.position + transform.right * -_meteorSpeed * Time.deltaTime * _timer/2);
    }

    //Função que retorna o valor do meteoro para o dano
    public int getDamage() { return _meteorDamage; }
        

}

   
