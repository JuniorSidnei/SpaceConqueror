using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public GameObject _DialogueBox;
    public ControlPlayer _player;
    public DialogueTrigger _dialogue;
    [HideInInspector]
    public bool _spawnOn = false;

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
