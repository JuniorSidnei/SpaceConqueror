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
//    [FormerlySerializedAs("m_lifeBox")]  public GameObject _lifeBox;
//    [FormerlySerializedAs("m_recoveryBox")] public GameObject _recoveryBox;
//    [FormerlySerializedAs("m_speedometerBox")] public GameObject _speedometerBox;
//    public GameObject AmmunitionBox;

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
    public MapManager MapManager;
    public InventoryManager InventoryManager;
    public ArmoryManager ArmoryManager;
    public CraftManager CraftManager;

    [Header("HudSettings")]
    public Image CrackedHud;

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
        InventoryManager.FireMeteorite.text = m_playerInfo.FireMeteoriteInGame.ToString();
        InventoryManager.IceMeteorite.text = m_playerInfo.IceMeteoriteInGame.ToString();
        InventoryManager.LightningMeteorite.text = m_playerInfo.LightningMeteoriteInGame.ToString();
        
        //Update do valor de kit de reparos
        ArmoryManager.Recovery.text = m_playerInfo.RecoveryKit.ToString();
        
        //Input para ativar o mapa
        if (Input.GetKeyDown(KeyCode.Tab) && GameManager.Instance.MapController == 0)
        {
            MapManager.Show();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && GameManager.Instance.MapController == 1)
        {
            MapManager.Hide();
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
        
//        _lifeBox.gameObject.transform.DOScale(new Vector3(0, 0,  0),1f).SetEase(Ease.Linear).OnComplete(() =>
//        {
//            _lifeBox.SetActive(false);
//        });
//        _recoveryBox.gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() =>
//        {
//            _recoveryBox.SetActive(false);
//        });
//        _speedometerBox.gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() =>
//        {
//            _speedometerBox.SetActive(false);
//        });
//        AmmunitionBox.gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() =>
//        {
//            AmmunitionBox.SetActive(false);
//        });
    }

    public override void HandlePlaying()
    {
        base.HandlePlaying();
        foreach (var b in Boxes)
        {
            b.SetActive(true);
            b.gameObject.transform.DOScale(new Vector3(1, 1,  0),1f).SetEase(Ease.Linear);
        }
//        _lifeBox.gameObject.transform.DOScale(new Vector3(1, 1,  0),1f).SetEase(Ease.Linear);
//        _recoveryBox.gameObject.transform.DOScale(new Vector3(1, 1, 0), 2f).SetEase(Ease.Linear);
//        _speedometerBox.gameObject.transform.DOScale(new Vector3(1, 1, 0), 3f).SetEase(Ease.Linear);
//        AmmunitionBox.gameObject.transform.DOScale(new Vector3(1, 1, 0), 2f).SetEase(Ease.Linear);
    }

    public override void HandleMap()
    {
        base.HandleMap();
        HandleConversation();
    }

    //Inventory HUD
    public void OnClickShowInventoryButton()
    {
        InventoryManager.Show();
    }
    
    public void OnClickHideInventoryButton()
    {
        InventoryManager.Hide();
    }

    //Armory HUD
    public void OnClickShowArmoryButton()
    {
        ArmoryManager.Show();
    }

    public void OnClickHideArmoryButton()
    {
        ArmoryManager.Hide();
    }

    //craft HUD
    public void OnClickShowCraftButton()
    {
        CraftManager.Show();
    }

    public void OnClickHideCraftButton()
    {
        CraftManager.Hide();
    }
    //Craft to create HUD
    public void OnClickCraftFireButton()
    {
        CraftManager.HandleCraftFireShoot();
    }
    public void OnClickCraftIceButton()
    {
        CraftManager.HandleCraftIceShoot();
    }
    public void OnClickCraftLightningButton()
    {
        CraftManager.HandleCraftLightningShoot();
    }
}
