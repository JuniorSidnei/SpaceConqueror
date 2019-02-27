using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //Referencia pra UI
    public Text _nameText;
    public Text _sentenceText;

    //Sentenças da caixa de texto
    public Queue<string> sentences;

    //Gamecontroller
    private GameController _gamectr;

    void Start()
    {
        sentences = new Queue<string>();
    }

   public void StartDialogue(Dialogue dialogue)
   {

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

    void EndDialogue()
    {
        _gamectr._DialogueBox.SetActive(false);
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
