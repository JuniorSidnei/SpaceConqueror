using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //Referencia pra UI
    public TextMeshProUGUI _nameText;
    public TextMeshProUGUI _sentenceText;

    //Sentenças da caixa de texto
    public Queue<string> sentences;

    public bool _dialogueFinished = false;
    public bool _secondDialogue = false;

    
    //Caixa de dialogo
    public GameObject _dialogueBox;
   
    void Start()
    {
        sentences = new Queue<string>();
        _dialogueBox.SetActive(false);
    }

   public void StartDialogue(Dialogue dialogue)
   {
        Debug.Log("eita porra");
        //Ativando a caixa de animação
        _dialogueBox.SetActive(true);

        //Da pra colcoar animação aqui pra mudar na transição a tal
        _nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        //Da pra colcoar animação aqui pra mudar na transição a tal
        if(sentences.Count == 0)
        {
           
            EndDialogue();
            return;
        }

        string sentece = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentece));
    }

    //Encerrando o dialogo
    public void EndDialogue()
    {
        //Desativar a caixa de dialogo
        _dialogueBox.SetActive(false);
        //Ativando os meteoros
        _dialogueFinished = true;
    }

    IEnumerator TypeSentence(string sentence)
    {
        _sentenceText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            _sentenceText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

}
