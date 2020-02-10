using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class HudManager : MonoBehaviour
{
    //Panel controller
    [FormerlySerializedAs("m_panelController")] [SerializeField]
    private PanelControllerPlaying m_PanelControllerPlaying;

    [SerializeField] private PanelControllerConversation m_PanelControllerConversation;

    [SerializeField] private PanelControllerMap m_PanelControllerMap;
    
    [SerializeField] private AmunnitionManager m_ammunitionManager;
    
    public Image m_Hud;
    
    public static bool m_isLoaded = false;

    [SerializeField] private Image m_transition;
    

    //Instancia da HUD
    public static HudManager Instance;
    
    //Ação quando termina de inicializar
    private static Action m_onFinishedInitialize;
    
    //Função que inicia a HUD como cena aditiva
    public static void Show(Action finishedInitializeCallback) {

        m_onFinishedInitialize = finishedInitializeCallback;
       if (!m_isLoaded)
       {
           SceneManager.LoadSceneAsync("HUD", LoadSceneMode.Additive);
           m_isLoaded = true;
       }
    }

    private void Awake()
    {

        //End transition
        m_transition.DOFade(0, 5f);
        m_transition.raycastTarget = false;

        //Declarando a instancia
        Instance = this;

        //Se for diferente de nulo, chama a ação
        if (m_onFinishedInitialize != null)
            m_onFinishedInitialize();
    }
    
    public void InitializeHudInfo(PlayerInfo playerInfo) { 
        m_PanelControllerPlaying.SetPlayerInfo(playerInfo);
        m_PanelControllerConversation.SetPlayerInfo(playerInfo);
        m_PanelControllerMap.SetPlayerInfo(playerInfo);
    }

    public void HandleConversation() {
        m_PanelControllerConversation.HandleConversation();
        m_PanelControllerPlaying.HandleConversation();
        m_PanelControllerMap.HandleConversation();
    }

    public void HandlePlaying() {
        m_PanelControllerConversation.HandlePlaying();
        m_PanelControllerPlaying.HandlePlaying();
        m_PanelControllerMap.HandlePlaying();
    }

    public void HandleMap() {
        m_PanelControllerConversation.HandleMap();
        m_PanelControllerPlaying.HandleMap();
        m_PanelControllerMap.HandleMap();
    }
    
    public void HandleOnDamage() {
        m_Hud.DOFade(0.8f, 0.2f).OnComplete(() => { m_Hud.DOFade(0, 0.2f);});
        CameraController.Instance.ScreenShake();
    }

    public void HandleLogMessages(LogMessageController.MessageType type) {
        m_PanelControllerPlaying.m_logText.text = LogMessageController.Instance.GetLogMessage(type);
        m_PanelControllerPlaying.m_logText.DOFade(0, 8f).OnComplete(HandleOffLogMessage);
    }
    
    private void HandleOffLogMessage() {
        m_PanelControllerPlaying.m_logText.text = "";
        m_PanelControllerPlaying.m_logText.DOFade(1, 0.1f);
    }

    public void HandleChangeAmmunitionStandard() {
        m_ammunitionManager.ShowActualAmmunition("Standard", AmunnitionManager.AmmunitionType.Standard);
    }
    
    public void HandleChangeAmmunitionFire() {
        m_ammunitionManager.ShowActualAmmunition("Fire", AmunnitionManager.AmmunitionType.Fire);
    }
    
    public void HandleChangeAmmunitionIce() {
        m_ammunitionManager.ShowActualAmmunition("Ice", AmunnitionManager.AmmunitionType.Ice);
    }
    
    public void HandleChangeAmmunitionLightning() {
        m_ammunitionManager.ShowActualAmmunition("Lightning", AmunnitionManager.AmmunitionType.Lightning);
    }
}
