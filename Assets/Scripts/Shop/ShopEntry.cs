using UnityEngine;
using System.Collections;

public class ShopEntry : MonoBehaviour
{
    private bool canEnterShop;
    public GameObject shopEntrancePanel;

    void DialogVisible(bool visibility)
    {
        canEnterShop = visibility;
        MessaggingManager.Instance.BroadcastUIEvent(visibility);
    }
	
    void OnTriggerEnter2D(Collider2D col)
    {
        DialogVisible(true);
        shopEntrancePanel.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        DialogVisible(false);
        shopEntrancePanel.SetActive(false);
    }

    void Update()
    {
        if (canEnterShop && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (NavigationManager.CanNavigate(this.tag))
                NavigationManager.NavigateTo(this.tag);
        } 
    }


}
