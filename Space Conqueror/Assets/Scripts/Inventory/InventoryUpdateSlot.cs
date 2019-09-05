using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUpdateSlot : MonoBehaviour
{
    
    public Image icon;

    private LootItem item;

    public void AddSlot(LootItem newItem)
    {
        item = newItem;

        //Replacing the icon on inventory
        icon.sprite = item.Icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }
}
