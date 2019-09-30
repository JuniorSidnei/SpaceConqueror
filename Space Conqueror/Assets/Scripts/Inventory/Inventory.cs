using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Action OnInventoryChangeCallback;

    private const int m_space = 20;
    
    public static Inventory Instance;

    public List<Slot> itens = new List<Slot>();
    

    private void Awake()
    {
        Instance = this;
    }

    //Add item to the inventory list
    public bool AddItem(LootItem item)
    {
        
        var slot = itens.Find(s => s.GetTypeName() == item.name && !s.Full());
        if (slot != null)
        {
            slot.Add(item);
        }
        else
        {
            
        }
        
        //itens.Add(item);

        Debug.Log("COLLECTING!!1");
        OnInventoryChangeCallback?.Invoke();

        return true;
    }

//    //Remove item to the inventory list
//    public void RemoveItem(LootItem item)
//    {
//        itens.Remove(item);
//
//        OnInventoryChangeCallback?.Invoke();
//    }
    
}
