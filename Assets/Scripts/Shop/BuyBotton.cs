using UnityEngine;
using System.Collections;

public class BuyBotton : MonoBehaviour {

	void OnMouseDown()
    {
        ShopManager.PurchaseSelectedItem();
    }
}
