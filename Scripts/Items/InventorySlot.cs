using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class InventorySlot
{
   public ItemData item;
   [SerializeField] private int quantity;


    public InventorySlot()
    {
        item = null;
        quantity = 0;
    }
    public InventorySlot(ItemData _item, int _quantity)
    {
        item = _item;
        quantity = _quantity;
    }

    public ItemData GetItem() { return item; }
    public int GetQuantity() { return quantity; }
    public void AddQuantity(int _quantity) { quantity += _quantity; }
    
}
