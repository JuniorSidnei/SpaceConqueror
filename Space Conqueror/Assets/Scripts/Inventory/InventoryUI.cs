using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemParent;

    private InventoryUpdateSlot[] slots;
    
    private Inventory m_inventory;
    
    void Start()
    {
        m_inventory = Inventory.Instance;
        m_inventory.OnInventoryChangeCallback += UpdateUI;
        slots = itemParent.GetComponentsInChildren<InventoryUpdateSlot>();
    }
    
    void Update()
    {
        
    }

    private void UpdateUI()
    {
        for (var i = 0; i < slots.Length; i++)
        {
            if (i < m_inventory.itens.Count)
            {
                slots[i].AddSlot(m_inventory.itens[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
