using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image _itemImage;
    [SerializeField]
    private Text _itemDescription;

    public void ShowItem(InventoryItem item)
    {
        _itemImage.sprite = item.Sprite;
        _itemDescription.text = item.ItemName;
        gameObject.SetActive(true);
    }
	
    public void HideItem()
    {
        gameObject.SetActive(false);
    }
	
}
