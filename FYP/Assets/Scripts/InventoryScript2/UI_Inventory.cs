using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory2 inventory;

    public void SetInventory(Inventory2 inventory)
    {
        this.inventory = inventory;
    }
}
