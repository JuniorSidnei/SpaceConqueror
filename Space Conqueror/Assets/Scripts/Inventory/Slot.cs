using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot
{
    private List<LootItem> items;

    private string itemName;
    
    public Slot()
    {
        items = new List<LootItem>(5);
    }

    public bool Add(LootItem item)
    {
        if (Full())
        {
            return false;
        }
        
        items.Add(item);
        return true;
    }
    
    public bool Remove(LootItem item)
    {
        if (Empty())
        {
            return false;
        }
        
        items.Remove(item);
        return true;
    }

    public bool Empty()
    {
        return items.Count > 0;
    }

    public bool Full()
    {
        return items.Count == items.Capacity;
    }

    public void SetSlotType(string name)
    {
        if (!Empty())
        {
           Debug.LogError("SAI LADRÃO, ta cheio");
        }
        
        itemName = name;
    }

    public string GetTypeName()
    {
        return itemName;
    }
}
