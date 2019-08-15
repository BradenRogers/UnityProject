using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IInventoryItem, IPickUpable
{
    public void PickUp()
    {
        // Destory Item
        Destroy(this.gameObject);
    }
}
