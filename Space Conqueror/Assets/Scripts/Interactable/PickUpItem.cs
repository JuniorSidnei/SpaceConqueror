using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
   public enum MeteoriteType
   {
      Ice, Fire, Lightinng, Gray
   }

   public MeteoriteType type;
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (!other.CompareTag("Player")) return;
      
      switch (type)
      {
         case MeteoriteType.Ice:
            other.GetComponent<ControlPlayer>().AddMeteorite(MeteoriteType.Ice, 1);
            HudManager.Instance.HandleLogMessages(LogMessageController.MessageType.BlueOrb);
            break;
         case MeteoriteType.Lightinng:
            other.GetComponent<ControlPlayer>().AddMeteorite(MeteoriteType.Lightinng, 1);
            HudManager.Instance.HandleLogMessages(LogMessageController.MessageType.YellowOrb);
            break;
         case MeteoriteType.Fire:
            other.GetComponent<ControlPlayer>().AddMeteorite(MeteoriteType.Fire, 1);
            HudManager.Instance.HandleLogMessages(LogMessageController.MessageType.RedOrb);
            break;
         case MeteoriteType.Gray:
            other.GetComponent<ControlPlayer>().AddMeteorite(MeteoriteType.Gray, 1);
            HudManager.Instance.HandleLogMessages(LogMessageController.MessageType.GrayOrb);
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }
      Destroy(gameObject);
   }
}
