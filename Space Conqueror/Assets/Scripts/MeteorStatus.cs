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
    //Quantidade de dano de cada meteoro
    public int _meteorDamage = 20;

    void Update()
    {

        //Movendo o meteoro
        transform.position += Vector3.left * _meteorSpeed * Time.deltaTime;
       

        //Verificando a vida e destruindo o meteoro
        if (_meteorLife <= 0)
            Destroy(gameObject);

        if(_meteorLife <= 0 && gameObject.CompareTag("BigMeteor"))
        {
            FindObjectOfType<DialogueManager>()._secondDialogue = true;
        }

      
            
    }

    //Função que retorna o valor do meteoro para o dano
    public int getDamage() { return _meteorDamage; }

    

}

   
