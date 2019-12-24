using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmunnitionManager : MonoBehaviour
{
    public enum AmmunitionType
    {
        Standard, Fire, Ice, Lightning
    }

    private Image m_img;

    [Header("Images")]
    public Sprite StandardAmunnition;
    public Sprite FireAmunnition;
    public Sprite IceAmunnition;
    public Sprite LightningAmunnition;

    [Header("Texts")]
    public TextMeshProUGUI Name;
    
    private void Start()
    {
        m_img = GetComponent<Image>();
    }

    public void ShowActualAmmunition(string name, AmmunitionType type)
    {
        m_img.DOFade(0, .5f);
        Name.DOFade(0, .5f);
        Name.text = name;
        
        switch (type)
        {
            //Standard
            case AmmunitionType.Standard:
                m_img.sprite = StandardAmunnition;
                Show();
                break;
            //Fire
            case AmmunitionType.Fire:
                m_img.sprite = FireAmunnition;
                Show();
                break;
            //Ice
            case AmmunitionType.Ice:
                m_img.sprite = IceAmunnition;
                Show();
                break;
            //Lightning
            case AmmunitionType.Lightning:
                m_img.sprite = LightningAmunnition;
                Show();
                break;
        }
    }

    private void Show()
    {
        m_img.DOFade(1, .5f).SetEase(Ease.Linear);
        Name.DOFade(1, .5f).SetEase(Ease.Linear);
    }
}
