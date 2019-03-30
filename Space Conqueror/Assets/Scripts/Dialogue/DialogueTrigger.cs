using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue []dialogue;
    public DialogueManager _dialogueMng;
    private LayerMask _layerCollision;

    void Update()
    {
        if (_dialogueMng._secondDialogue)
        {
            _dialogueMng.StartDialogue(dialogue[1]);
            _dialogueMng._secondDialogue = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D obj)
    {
        //Verifica a layer. no caso a default
        if(obj.gameObject.layer == 10)
        {
            //Colisão com o jogador
            if (obj.gameObject.CompareTag("Player"))
            {
                _dialogueMng.StartDialogue(dialogue[0]);
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                Destroy(gameObject.GetComponent<BoxCollider2D>());
            }
        }
    }

}
