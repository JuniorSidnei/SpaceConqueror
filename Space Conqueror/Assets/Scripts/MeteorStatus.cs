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
    //Sprites dos meteoros
    public Sprite[] _sprites;
    //Materiais dos meteoros
    public Material[] _materials;
   

    void Start()
    {
        //Aleatorio um numerod e 0 até 4
        var rand = Random.Range(0,4);

        //Carregando uma das sprites do array para mudar a forma do meteoro quando nasce
        gameObject.GetComponent<SpriteRenderer>().sprite = _sprites[rand];
        //Carregando o mesmo material do array quando nasce
        gameObject.GetComponent<SpriteRenderer>().material = _materials[rand];

    }


    void Update()
    {

        //Rotacionando e movendo o meteoro
        transform.position += Vector3.left * _meteorSpeed * Time.deltaTime;
        transform.Rotate(0, 0, 5);

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

   
