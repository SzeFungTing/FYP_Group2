using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public InventoryManager inventoryManager;


    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(false);
  
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryManager.ListItems();
            inventory.SetActive(!inventory.activeSelf);

        }
            if (Input.GetKeyDown(KeyCode.Escape))
            {

            inventory.SetActive(false);

            }
        
    }
}