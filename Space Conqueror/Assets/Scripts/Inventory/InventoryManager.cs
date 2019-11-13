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

   private void Start()
   {
      m_rect = GetComponent<RectTransform>();
   }

   public void Show()
   {
      AudioManager.PlaySound("MapShowUp");
      m_rect.DOAnchorPos(new Vector3(350, -560, 0),1f).OnComplete(() =>
      {
         GameManager.Instance.InvController++;
      });
   }

   public void Hide()
   {
      AudioManager.PlaySound("MapShowDown");
      m_rect.DOAnchorPos(new Vector3(1260, -560,0),1f).OnComplete(() =>
      {
         GameManager.Instance.InvController--;
         HudManager.Instance.HandlePlaying();  
      });
   }
   
   public TextMeshProUGUI FireMeteorite
   {
      get => FireMeteoriteAmount;
      set => FireMeteoriteAmount = value;
   }

   public TextMeshProUGUI LightningMeteorite
   {
      get => LightningMeteoriteAmount;
      set => LightningMeteoriteAmount = value;
   }

   public TextMeshProUGUI IceMeteorite
   {
      get => IceMeteoriteAmount;
      set => IceMeteoriteAmount = value;
   }
}
