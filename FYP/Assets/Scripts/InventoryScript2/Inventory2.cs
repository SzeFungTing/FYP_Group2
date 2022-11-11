using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory2 
{
    public event EventHandler OnItemListChanged;

    private List<Item2> itemList;

    public Inventory2()
    {
        itemList = new List<Item2>();

        //AddItem(new Item2 { itemType = Item2.ItemType.ChickenGhost, amount = 1 });
        //AddItem(new Item2 { itemType = Item2.ItemType.Traget, amount = 1 });
        //AddItem(new Item2 { itemType = Item2.ItemType.Cube, amount = 1 });
        //Debug.Log(itemList.Count);


        //Debug.Log("Inventory");
    }

    public void AddItem(Item2 item)
    {

        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item2 inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
                //Debug.Log(item.itemType);
            }
        }
        else
        {
            itemList.Add(item);
            //Debug.Log(item.itemType);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item2 item)
    {
        Debug.Log("RemoveItem: " + item.itemType + " " + item.amount);
        if (item.IsStackable())
        {
            Item2 itemInInventory = null;
            //Debug.Log("itemInInventory is null?: " + itemInInventory.itemType);
            //Debug.Log("itemInInventory.amount: " + itemInInventory.amount);
            foreach (Item2 inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    Debug.Log("inventoryItem is null?: " + inventoryItem.itemType + "inventoryItem.amount: " + inventoryItem.amount);
                    Debug.Log("item.amount--");
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            Debug.Log("itemInInventory is null?: " + itemInInventory.itemType + "itemInInventory.amount: " + itemInInventory.amount);
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                Debug.Log("item.amount<0");
                itemList.Remove(itemInInventory);
            }
           
        }
        else
        {
            Debug.Log("item can not stackable");
            itemList.Remove(item);
            
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item2> GetItemList()
    {
        return itemList;
    }
}
