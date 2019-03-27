using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorDefinitions : MonoBehaviour
{
    //Sprites dos meteoros
    public Sprite[] _sprites;
    //Materiais dos meteoros
    public Material[] _materials;

    void Start()
    {
        //Aleatorio um numerod e 0 até 4
        var rand = Random.Range(0, 4);

        //Carregando uma das sprites do array para mudar a forma do meteoro quando nasce
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _sprites[rand];
        //Carregando o mesmo material do array quando nasce
        gameObject.GetComponentInChildren<SpriteRenderer>().material = _materials[rand];

    }

    
    void Update()
    {
        //Rotacionando a sprite
        transform.Rotate(0, 0, 5);
    }
}
