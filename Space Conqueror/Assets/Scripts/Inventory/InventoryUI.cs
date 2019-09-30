using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemParent;

    private Slot[] slots;

    private Inventory m_inventory;
    
    void Start()
    {
        m_inventory = Inventory.Instance;
       // m_inventory.OnInventoryChangeCallback += UpdateUI;
        slots = itemParent.GetComponentsInChildren<Slot>();
    }
    
//    private void UpdateUI()
//    {
//        //Varrendo o inventario vendo qual slot está vago
//        for (var i = 0; i < slots.Length; i++)
//        {
//            //Verifica se tem slot
//            if (i < m_inventory.itens.Count)
//            {
//                slots[i].AddSlot(m_inventory.itens[i], 1);
//            }
//            else
//            {
//                slots[i].ClearSlot();
//            }
//        }
//    }
}
