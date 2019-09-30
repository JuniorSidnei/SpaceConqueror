using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
   //public LootItem lootableItem;
   public enum MeteoriteType
   {
      Ice, Fire, Lightinng
   }

   public MeteoriteType type;
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         switch (type)
         {
            case MeteoriteType.Ice:
               other.GetComponent<ControlPlayer>().AddMeteorite(MeteoriteType.Ice, 1);
               break;
            case MeteoriteType.Lightinng:
               other.GetComponent<ControlPlayer>().AddMeteorite(MeteoriteType.Lightinng, 1);
               break;
            case MeteoriteType.Fire:
               other.GetComponent<ControlPlayer>().AddMeteorite(MeteoriteType.Fire, 1);
               break;
            default:
               throw new ArgumentOutOfRangeException();
         }
         Destroy(gameObject);
      }
   }
}
