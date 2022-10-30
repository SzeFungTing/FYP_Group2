using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory2 
{
    private List<Item2> itemList;

    public Inventory2()
    {
        itemList = new List<Item2>();

        AddItem(new Item2 { itemType = Item2.ItemType.Traget, amount = 1 });
        Debug.Log(itemList.Count);


        //Debug.Log("Inventory");
    }

    public void AddItem(Item2 item)
    {
        itemList.Add(item);
    }

}
