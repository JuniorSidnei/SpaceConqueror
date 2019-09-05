using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
   public LootItem lootableItem;
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         Debug.Log("PEGANDO: " + lootableItem.name);
         var wadPickedUp = Inventory.Instance.AddItem(lootableItem);
         
         //Only destroy the item if was pickedup
         if (wadPickedUp)
            Destroy(gameObject);
      }
   }
}
