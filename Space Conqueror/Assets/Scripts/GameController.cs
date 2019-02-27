using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public GameObject _DialogueBox;


    public DialogueTrigger _dialogue;

    public ControlPlayer _player;

    void Start()
    {
        //Deixando a caixa de dialogo desativada
        _DialogueBox.SetActive(false);

    }



    //Ativando o evento
    void OnEnable()
    {
        _player.DialogueEvent += DialogueOn;
    }

    //Desativando o evento
    void OnDisable()
    {
        _player.DialogueEvent -= DialogueOn;
    }

    void DialogueOn()
    {
        _DialogueBox.SetActive(true);
        _dialogue.triggerDialogue();
    }



}
