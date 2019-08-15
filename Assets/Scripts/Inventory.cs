using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName ="InventoryData", menuName = "ScriptableObjects/CreateInventoryData", order = 1)]
public class Inventory : ScriptableObject
{
    private List<IInventoryItem> items {get; set;} = new List<IInventoryItem>();

    public List<IPickUpable> GetPickUpables()
    {
        return items.Where(itm => itm is IPickUpable).Select(itm => itm as IPickUpable).ToList();
    }

    public void AddItem(IInventoryItem newItem)
    {
        Debug.Log(newItem.ToString() + " picked up");
        items.Add(newItem);
        newItem.PickUp();
    }
}
