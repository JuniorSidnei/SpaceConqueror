using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PanelControllerPlaying : BaseHudBehavior
{
    [SerializeField] private PlayerInfo m_playerInfo;
    private ControlPlayer m_controlPlayer;
    
    [Header("Boxes")]
    public List<GameObject> Boxes;
    
    [Header("Images")]
    [FormerlySerializedAs("m_LifeBarFill")] public Image LifeBarFill;
    [FormerlySerializedAs("m_recoveryKitKey")] public Image RecoveryKitKey;
    [FormerlySerializedAs("m_speedometerFill")] public Image SpeedometerFill;

    [Header("Texts")]
    public TextMeshProUGUI m_playerLifeText;
    [HideInInspector]
    public TextMeshProUGUI m_logText;

    [Header("Managers")]
    [SerializeField] private MapManager m_mapManager;
    [SerializeField] private InventoryManager m_inventoryManager;
    [SerializeField] private ArmoryManager m_armoryManager;
    [SerializeField] private CraftManager m_craftManager;
    [SerializeField] private PauseController m_pauseController;

    [Header("HudSettings")]
    public Image CrackedHud;

    public SpeechScriptable FireCraft;
    public SpeechScriptable IceCraft;
    public SpeechScriptable LightningCraft;
    private void Start()
    {
        m_controlPlayer = FindObjectOfType<ControlPlayer>();
    }

    private void Update()
    {
        //Se levar muuito dano, vai rachar o vidro da nave
        if(m_playerInfo.CurrentLife <= m_playerInfo.MaxLife / 3)
            CrackedHud.gameObject.SetActive(true);
        
        m_playerLifeText.text = ("" + m_playerInfo.CurrentLife);
        LifeBarFill.DOFillAmount((float)m_playerInfo.CurrentLife / m_playerInfo.MaxLife, 2f);
        
        SpeedometerFill.DOFillAmount(m_controlPlayer.GetComponent<Rigidbody2D>().velocity.sqrMagnitude, 1f);
        
        if (m_playerInfo.RecoveryKit <= 0)
            RecoveryKitKey.gameObject.GetComponent<Image>().DOColor(Color.black, 1f);
        
        //Update dos valores dos meteoritos em relação ao jogador
        m_inventoryManager.FireMeteorite.text = m_playerInfo.FireMeteoriteInGame.ToString();
        m_inventoryManager.IceMeteorite.text = m_playerInfo.IceMeteoriteInGame.ToString();
        m_inventoryManager.LightningMeteorite.text = m_playerInfo.LightningMeteoriteInGame.ToString();
        m_inventoryManager.GrayMeteorite.text = m_playerInfo.GrayMeteoriteInGame.ToString();
        
        //Update do valor de kit de reparos
        m_armoryManager.Recovery.text = m_playerInfo.RecoveryKit.ToString();
        
        //Input para ativar o mapa
        if (Input.GetKeyDown(KeyCode.Tab) && GameManager.Instance.MapController == 0) {
            m_mapManager.Show();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && GameManager.Instance.MapController == 1) {
            m_mapManager.Hide();
        }
        
        //Input para o pause
        if (Input.GetKey(KeyCode.Escape)) {
            m_pauseController.Show();
        }
    }
    
    //Informações iniciais do jogador
    public override void SetPlayerInfo(PlayerInfo playerInfo)
    {
        m_playerInfo = playerInfo;
    }
    
    public override void HandleConversation()
    {
        base.HandleConversation();
        foreach (var b in Boxes)
        {
            b.gameObject.transform.DOScale(new Vector3(0, 0,  0),1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                b.SetActive(false);
            });
        }
    }

    public override void HandlePlaying()
    {
        base.HandlePlaying();
        foreach (var b in Boxes)
        {
            b.SetActive(true);
            b.gameObject.transform.DOScale(new Vector3(1, 1,  0),1f).SetEase(Ease.Linear);
        }
    }

    public override void HandleMap()
    {
        base.HandleMap();
        foreach (var b in Boxes)
        {
            b.gameObject.transform.DOScale(new Vector3(0, 0,  0),1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                b.SetActive(false);
            });
        }
    }

    //Inventory HUD
    public void OnClickShowInventoryButton()
    {
        m_inventoryManager.Show();
    }
    
    public void OnClickHideInventoryButton()
    {
        m_inventoryManager.Hide();
    }

    //Armory HUD
    public void OnClickShowArmoryButton()
    {
        m_armoryManager.Show();
    }

    public void OnClickHideArmoryButton()
    {
        m_armoryManager.Hide();
    }

    //craft HUD
    public void OnClickShowCraftButton()
    {
        m_craftManager.Show();
    }

    public void OnClickHideCraftButton()
    {
        m_craftManager.Hide();
    }
    
    //Craft to create HUD
    public void OnClickCraftFireButton()
    {
        m_craftManager.HandleCraftFireShoot(()=>EventHandler.Instance.CallDialogue(FireCraft));
    }
    public void OnClickCraftIceButton()
    {
        m_craftManager.HandleCraftIceShoot(()=>EventHandler.Instance.CallDialogue(IceCraft));
    }
    public void OnClickCraftLightningButton()
    {
        m_craftManager.HandleCraftLightningShoot(()=>EventHandler.Instance.CallDialogue(LightningCraft));
    }
}
