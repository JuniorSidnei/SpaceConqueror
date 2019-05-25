using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class HudManager : MonoBehaviour
{
    //Panel controller
    [FormerlySerializedAs("m_panelController")] [SerializeField]
    private PanelControllerPlaying m_PanelControllerPlaying;

    [SerializeField]
    private PanelControllerConversation m_PanelControllerConversation;

    private static bool m_isLoaded = false;
    
    //Instancia da HUD
    public static HudManager Instance;
    
    //Ação quando termina de inicializar
    private static Action m_onFinishedInitialize;
    
    //Função que inicia a HUD como cena aditiva
    public static void Show(Action finishedInitializeCallback)
    {
       m_onFinishedInitialize = finishedInitializeCallback;
       if (!m_isLoaded)
       {
           SceneManager.LoadSceneAsync("HUD", LoadSceneMode.Additive);
           m_isLoaded = true;
       }
    }

    void Awake()
    {
        //Declarando a instancia
        Instance = this;

        //Se for diferente de nulo, chama a ação
        if (m_onFinishedInitialize != null)
            m_onFinishedInitialize();
    }
    
    public void InitializeHudInfo(PlayerInfo playerInfo)
    { 
        m_PanelControllerPlaying.SetPlayerInfo(playerInfo);
        m_PanelControllerConversation.SetPlayerInfo(playerInfo);
    }

    public void HandleConversation()
    {
        
        m_PanelControllerConversation.HandleConversation();
        m_PanelControllerPlaying.HandleConversation();
    }

    public void HandlePlaying()
    {
        m_PanelControllerConversation.HandlePlaying();
        m_PanelControllerPlaying.HandlePlaying();
    }
    
}
