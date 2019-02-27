using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //Aqui que eu tenho que criar as funções pra chamar os eventos

    public Dialogue dialogue;
  
   

    public void triggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

   

}
