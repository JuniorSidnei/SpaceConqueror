using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioFile
{
    //Nome do clipe
    public string audioName;
    //O arquivo de som
    public AudioClip audioClip;
    
    //Volume
    [Range(0f,1f)]
    public float volume;
    
    //A source que fica escondida
    [HideInInspector]
    public AudioSource source;
    
    //Se é loop ou não
    public bool isLooping;
    
    //Se toca quando começar ou não
    public bool playOnAwake;
    
}

public class AudioManager : MonoBehaviour
{
    #region Variables

    //Instancia publica
    public static AudioManager Instance;
    
    //Array da classe para os diversos sons na cena toda
    public AudioFile[] m_audioFile;
    
    //Para resetar o som
    private float m_timeReset;
    
    //Se o tempo foi setado ou não
    private bool m_timerSet = false;
    
    //Nome temporario
    private string m_tempName;
    
    //Volume temporário
    private float m_tempVol;
    
    //Volume baixo ou não
    private bool m_isLowered = false;
    
    //FadeOut
    private bool m_fadeOut = false;
    
    //FadeIn
    private bool m_fadeIn = false;
    
    private string m_fadeInUsedString;
    
    private string m_fadeOutUsedString;

    #endregion

    
    #region Methods
    void Awake()
    {
        
        
        //Validação da instancia do audio manager
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        //Não destroi quando carrega
        DontDestroyOnLoad(gameObject);
        
        //Atibuindo os valores a classe de audios ao audio source do objeto
        foreach (var s in m_audioFile)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.loop = s.isLooping;

            if (s.playOnAwake)
                s.source.Play();
        }
    }

    //Função para tocar som
    public static void PlaySound(string name)
    {
        Debug.Log("TOCA SOM CARALHO");
        //Verifica a primeira ocorrência de match, que seria o nome passado na função
        AudioFile s = Array.Find(Instance.m_audioFile, AudioFile => AudioFile.audioName == name);
        
        //Se for nulo, dispara um erro, se não toca o som
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " Not found!");
            return;
        }

        Debug.Log("    vaaaaaaaaaaiiiiiiiiiiiiiiiiiiiiiiiiiiii");
        s.source.Play();
        
    }

    //Função para parar som
    public static void StopSound(string name)
    {
        Debug.Log("VOCE TA INDO JUNTO CARALHO?!?!");
        //Verifica a primeira ocorrência de match, que seria o nome passado na função
        AudioFile s = Array.Find(Instance.m_audioFile, AudioFile => AudioFile.audioName == name);
        
        //Se for nulo, dispara um erro, se não toca o som
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " Not found!");
            return;
        }
        else
        {
            s.source.Stop();
        }
    }
    
    //Função de pausar
    public static void PauseSound(string name)
    {
        //Verifica a primeira ocorrência de match, que seria o nome passado na função
        AudioFile s = Array.Find(Instance.m_audioFile, AudioFile => AudioFile.audioName == name);
        
        //Se for nulo, dispara um erro, se não toca o som
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " Not found!");
            return;
        }
        else
        {
            s.source.Pause();
        }
    }
    //Função para despausar
    public void UnpauseSound(string name)
    {
        //Verifica a primeira ocorrência de match, que seria o nome passado na função
        AudioFile s = Array.Find(Instance.m_audioFile, AudioFile => AudioFile.audioName == name);
        
        //Se for nulo, dispara um erro, se não toca o som
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " Not found!");
            return;
        }
        else
        {
            s.source.UnPause();
        }
    }
    //Função para abaixar o volume
    public static void LowerSoundVolume(string name, float duration)
    {
        //Se não estiver com volume baixo
        if (!Instance.m_isLowered)
        {
            //Verifica a primeira ocorrência de match, que seria o nome passado na função
            AudioFile s = Array.Find(Instance.m_audioFile, AudioFile => AudioFile.audioName == name);

            //Se for nulo, dispara um erro, se não seta as variaveis temporarias e o volume por 1/3
            if (s == null)
            {
                Debug.LogError("Sound: " + name + " Not found!");
                return;
            }
            else
            {
                Instance.m_tempName = name;
                Instance.m_tempVol = s.volume;
                Instance.m_timeReset = Time.time + duration;
                Instance.m_timerSet = true;
                s.source.volume /= 3;
            }
            Instance.m_isLowered = true;
        }
    }
    //Função para fadeOut no som
    public static void FadeOut(String name, float duration)
    {
        Instance.StartCoroutine(Instance.IFadeOut(name, duration));
    }

    //Função para fadeIn no som
    public static void FadeIn(String name, float targetVolume, float duration)
    {
        Instance.StartCoroutine(Instance.IFadeIn(name, targetVolume, duration));
    }
    
    
    //Corrtina do fadeOut
    IEnumerator IFadeOut(string name, float duration)
    {
        //Verifica a primeira ocorrência de match, que seria o nome passado na função
        AudioFile s = Array.Find(Instance.m_audioFile, AudioFile => AudioFile.audioName == name);
        
        //Se for nulo dispara um erro, se não diz que fadeOut é verdadeiro, seta o volume e enquanto ele
        //for maior que 0 diminui o volume, depois para musica
        if (s == null)
        {
            Debug.LogError("Sound: " + name + "Not found!");
            yield return null;
        }
        else
        {
            if (!m_fadeOut)
            {
                m_fadeOut = true;
                var startVol = s.source.volume;
                m_fadeOutUsedString = name;

                while (s.source.volume > 0)
                {
                    s.source.volume -= startVol * Time.deltaTime / duration;
                    yield return null;
                }
                
                s.source.Stop();
                yield return new WaitForSeconds(duration);
                m_fadeOut = false;
            }
            else
            {
                Debug.Log("Could not handle two fade outs at once : " + name + " , " + m_fadeOutUsedString +"! Stopped the music " + name);
                StopSound(name);
            }
        }
    }

    
    
    //Corrotina do fadeIn
    IEnumerator IFadeIn(string name, float duration, float targetVolume)
    {
        //Verifica a primeira ocorrência de match, que seria o nome passado na função
        AudioFile s = Array.Find(Instance.m_audioFile, AudioFile => AudioFile.audioName == name);

        //Se for nulo dispara um erro, se não diz que fadeIn é verdadeiro, seta o volume para 0 e enquanto ele
        //for menor que targetVolume aumenta o volume, depois inicia musica
        if (s == null)
        {
            Debug.LogError("Sound name" + name + "not found!");
            yield return null;
        }
        else
        {
            if (!m_fadeIn)
            {
                m_fadeIn = true;
                m_fadeInUsedString = name;
                s.source.volume = 0f;
                s.source.Play();

                while (s.source.volume < targetVolume)
                {
                    s.source.volume += Time.deltaTime / duration;
                    yield return null;
                }
                
                yield return new WaitForSeconds(duration);
                m_fadeIn = false;
            }
            else
            {
                Debug.Log("Could not handle two fade ins at once: " + name + " , " + m_fadeInUsedString+ "! Played the music " + name);
                StopSound(m_fadeInUsedString);
                PlaySound(name);
            }
        }
    }
    
    //Reseta o volume 
    void ResetSoundVolume()
    {
        AudioFile s = Array.Find(Instance.m_audioFile, AudioFile => AudioFile.audioName == m_tempName);
        s.source.volume = m_tempVol;
        m_isLowered = false;

    }

    void Update()
    {
        //Verifica o tempo que a musica está baixa e depois resta o volume
        if (Time.deltaTime >= m_timeReset && m_timerSet)
        {
            ResetSoundVolume();
            m_timerSet = false;
        }
    }
    #endregion
}
