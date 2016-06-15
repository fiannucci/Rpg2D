using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : Singleton<InventoryManager>
{
    
    [SerializeField]
    private UIInventoryItem _uiItemPrefab;

    [SerializeField]
    private Transform _inventoryItemRootParent;

    private bool _inventoryIsActive = false;
    protected InventoryManager() { }  
	

    public void PopulateInventory()
    {
        var inventoryList = GameState.currentPlayer.Inventory;

        foreach(Transform child in _inventoryItemRootParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < inventoryList.Count; ++i)
        {
            UIInventoryItem item = Instantiate(_uiItemPrefab) as UIInventoryItem;
            item.transform.SetParent(_inventoryItemRootParent, false);
            item.SetupItem(inventoryList[i]);
        }
    }

    public void ShowInventory()
    {
        PopulateInventory();
        if (!_inventoryIsActive)
            _inventoryItemRootParent.gameObject.SetActive(true);
        else
            HideInventory();                    
        _inventoryIsActive = !_inventoryIsActive;
    }

    public void HideInventory()
    {
        _inventoryItemRootParent.gameObject.SetActive(false);
    }
}
