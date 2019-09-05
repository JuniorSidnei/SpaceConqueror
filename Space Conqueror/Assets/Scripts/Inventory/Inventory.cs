using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public delegate void OnInventoryChange();

    public OnInventoryChange OnInventoryChangeCallback;
    
    
    private const int m_space = 20;
    
    public static Inventory Instance;

    public List<LootItem> itens = new List<LootItem>();


    private void Awake()
    {
        Instance = this;
    }

    //Add item to the inventory list
    public bool AddItem(LootItem item)
    {
        if (itens.Count >= m_space)
        {
            Debug.Log("I CAN'T CARE ANYMORE!!1");
            return false;
        }
        
        itens.Add(item);

        Debug.Log("COLLECTING!!1");
        OnInventoryChangeCallback?.Invoke();

        return true;
    }

    //Remove item to the inventory list
    public void RemoveItem(LootItem item)
    {
        itens.Remove(item);

        OnInventoryChangeCallback?.Invoke();
    }
    
}
