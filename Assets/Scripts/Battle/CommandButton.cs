using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class CommandButton : MonoBehaviour
{
    private CommandBar commandBar;
    public InventoryItem Item;
    bool selected;
	
    public void Init(CommandBar commandBar)
    {
        this.commandBar = commandBar;
        gameObject.layer = commandBar.Layer;

        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        collider.size = new Vector2(1f, 1f);

        SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
        render.sprite = commandBar.DefaultButtonImage;
        render.sortingLayerName = "GUI";
        render.sortingOrder = 5;
    }

    public void AddInventoryItem(InventoryItem item)
    {
        this.Item = item;
        GameObject childGo = new GameObject("InventoryItemDislpayImage");
        SpriteRenderer renderer = childGo.AddComponent<SpriteRenderer>();
        renderer.sprite = item.Sprite;
        renderer.sortingLayerName = "GUI";
        renderer.sortingOrder = 10;
        renderer.transform.parent = this.transform;
        renderer.transform.localScale *= 4;
    }

    void UpdateSelection()
    {
        var renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = selected ? commandBar.SelectedButtonImage : commandBar.DefaultButtonImage;
    }

    public void ClearSelection()
    {
        selected = false;
        UpdateSelection();
    }

    void OnMouseDown()
    {
        if (commandBar.CanSelectButton)
        {
            selected = !selected;
            UpdateSelection();
            commandBar.SelectButton(selected ? this : null);
        }
    }
}

