using UnityEngine;
using System.Collections;

public class PlayerInventoryDislpay : MonoBehaviour
{
    bool displayInventory = false;
    Rect inventoryWindowRect;
    private Vector2 inventoryWindowSize = new Vector2(150, 150);
    Vector2 inventoryItemIconSize = new Vector2(130, 32);

    float offsetX = 6;
    float offsetY = 6;
    
    void Awake()
    {
        inventoryWindowRect = new Rect(Screen.width - inventoryWindowSize.x, Screen.height - inventoryWindowSize.y - 40, inventoryWindowSize.x,inventoryWindowSize.y);
    }	

    void OnGUI()
    {
        if(GUI.Button(new Rect(Screen.width-40, Screen.height - 40, 40, 40), "INV"))
        {
            displayInventory = !displayInventory;
        }
        if (displayInventory)
        {
            inventoryWindowRect = GUI.Window(0, inventoryWindowRect, DisplayInventorWindow, "Inventory");
            inventoryWindowSize = new Vector2(inventoryWindowRect.width, inventoryWindowRect.height);
        }
    }

    void DisplayInventorWindow(int windowID)
    {
        var currentX = 0 + offsetX;
        var currentY = 18 + offsetY;

        foreach(var item in GameState.currentPlayer.Inventory)
        {
            Rect textcoords = item.Sprite.textureRect;

            textcoords.x /= item.Sprite.texture.width;
            textcoords.y /= item.Sprite.texture.height;
            textcoords.height /= item.Sprite.texture.height;
            textcoords.width /= item.Sprite.texture.width;

            GUI.DrawTextureWithTexCoords(new Rect(currentX, currentY, item.Sprite.texture.width, item.Sprite.texture.height), item.Sprite.texture, textcoords);

            currentX += inventoryItemIconSize.x;
            if(currentX + inventoryItemIconSize.x + offsetX > inventoryWindowSize.x)
            {
                currentX = offsetX;
                currentY += inventoryItemIconSize.y;
                if (currentY + inventoryItemIconSize.y + offsetY > inventoryWindowSize.y)
                    return;
            }
        }
    }
}
