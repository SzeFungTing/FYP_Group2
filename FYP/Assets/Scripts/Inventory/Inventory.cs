using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;

    public InventoryManager InventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(false);
        InventoryManager.ListItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            inventory.SetActive(true);
          
        }
    }
}
