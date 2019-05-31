using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
   
   #region variables
   [SerializeField] private  TextMeshProUGUI m_speakerName;

   [SerializeField] private  TextMeshProUGUI m_mainText;

   [SerializeField] private SpeechScriptable m_currentSpeechFull;
   
   private int m_currentSpeechIndex;
   
   private bool m_speechProgress = false;

   public static DialogueManager Instance;

   private static Action m_onFinishDialogue;
   
   //Todos os personagens que terão falas aqui
   public enum Speaker
   {
      //Jogador, computador de bordo e os inimigos
      Scavenger, Orion, Unidentifield
   }

   //Struct para criar quem fala e as falas deles
   [System.Serializable]
   public struct SpeechGroup
   {
      //Quem fala
      public Speaker m_currentSpeaker;
      [TextArea(4,10)]
      //O que fala
      public string m_speechText;
   }

   #endregion
   
  #region methods

  private void Awake()
  {
     Instance = this;
  }

  //Aqui começa o dialogo, quando triggar o evento ou alguma coisa, isso que vai começar o dialogo
  public void StartSpeech(SpeechScriptable speech, float delay, Action endDialogue)
  {
     //Invokar evento somente depois que terminar a corrotina
     m_onFinishDialogue = endDialogue;
     StartCoroutine(StartSpeechCoroutine(speech, delay));
     
  }

  //Proxima fala da conversa, caso não seja a última
  public void NextSpeech()
  {
     StopAllCoroutines();
     m_currentSpeechIndex++;

     if(m_currentSpeechFull.speechGroup.Count > m_currentSpeechIndex)
        FillSpeech();
     else
     {
        EndSpeech();
     }
  }
  
  //Final do dialogo, isso que vai chamar quando acabar ou pra pular o dialogo
  public void EndSpeech( )
  {
     HudManager.Instance.HandlePlaying();
     m_speechProgress = false;
     m_onFinishDialogue?.Invoke();
  }
  
  
  //Função para associar os textos e os nomes(quem sabe futuramente sprites)
  private void FillSpeech()
  {
     
     SpeechGroup s = m_currentSpeechFull.speechGroup[m_currentSpeechIndex];

     m_speakerName.text = s.m_currentSpeaker.ToString();
     StartCoroutine(WriteSentence(s.m_speechText));

  }
  
  //Corrotina onde seta o index e chama a proxima função
  IEnumerator StartSpeechCoroutine(SpeechScriptable speech, float delay)
  {
     
     while (m_speechProgress)
     {
        yield return null;
     }

     m_speechProgress = true;
     yield return new WaitForSeconds(delay);
     m_currentSpeechFull = speech;
     m_currentSpeechIndex = 0;
     FillSpeech();
  }

  //Escrevendo a sentença da frase letra por letra pra ficar legal
  IEnumerator WriteSentence(string sentence)
  {
     
     m_mainText.text = "";
     foreach (char letter in sentence)
     {
        m_mainText.text += letter;
        yield return new WaitForSeconds(0.02f);
     }
  }
  
  #endregion
   
}
