using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   private RectTransform m_rect;
   
   public TextMeshProUGUI FireMeteoriteAmount;
   public TextMeshProUGUI LightningMeteoriteAmount;
   public TextMeshProUGUI IceMeteoriteAmount;
   public TextMeshProUGUI GrayMeteoriteAmount;

   private void Start()
   {
      m_rect = GetComponent<RectTransform>();
      gameObject.SetActive(false);
   }

   public void Show()
   {
      AudioManager.PlaySound("MapShowUp");
      gameObject.SetActive(true);
      m_rect.DOAnchorPos(new Vector3(-400, -45, 0),1f).SetEase(Ease.OutBack);
   }

   public void Hide()
   {
      AudioManager.PlaySound("MapShowDown");
      m_rect.DOAnchorPos(new Vector3(256, -110,0),1f).OnComplete(() =>
      {
         HudManager.Instance.HandlePlaying();
         gameObject.SetActive(false);
      });
   }
   
   public TextMeshProUGUI FireMeteorite => FireMeteoriteAmount;
   public TextMeshProUGUI LightningMeteorite => LightningMeteoriteAmount;
   public TextMeshProUGUI IceMeteorite => IceMeteoriteAmount;
   public TextMeshProUGUI GrayMeteorite => GrayMeteoriteAmount;
}
