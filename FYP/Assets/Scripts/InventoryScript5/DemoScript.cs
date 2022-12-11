using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager5 inventoryManager;
    public Item5[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result)
            Debug.Log("item added");
        else
            Debug.Log("item not added");
    }

    public void GetSelectedItem()
    {
        Item5 receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
            Debug.Log("Received item: " + receivedItem);
        else
            Debug.Log("no item received ");
    }

    public void UseSelectedItem()
    {
        Item5 receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
            Debug.Log("use item: " + receivedItem);
        else
            Debug.Log("no item use ");
    }
}
