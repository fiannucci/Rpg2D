using UnityEngine;
using System.Collections.Generic;

public class Player : Entity
{
    public List<InventoryItem> Inventory = new List<InventoryItem>();
    public string[] Skills;
    public int Money;	

    public void AddInventoryItem(InventoryItem item)
    {
        this.Strenght += item.Strength;
        this.Defense += item.Defense;
        Inventory.Add(item);
    }
}
