using UnityEngine;
using System.Collections.Generic;

public class Player : Entity
{
    public List<InventoryItem> Inventory = new List<InventoryItem>();
    public string[] Skills;
    public int Money;
    
    public int _defensese;	

    public void AddInventoryItem(InventoryItem item)
    {
        this.Strenght += item.Strength;
        this._defensese += item.Defense;
        Inventory.Add(item); // questo la fa. gli altri 2 sopra no
    }
}
