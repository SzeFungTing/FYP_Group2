using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public Item5 item;
    /*[HideInInspector]*/public bool canConsume;

    private void Start()
    {
        canConsume = false;
    }

    public int GetItemId()
    {
        return item.id;
    }

    public int GetCurrentPrice()
    {
        return (int)item.price;
    }

    public int GetDefaultPrice()
    {
        return (int)item.originalPrice;
    }
}
