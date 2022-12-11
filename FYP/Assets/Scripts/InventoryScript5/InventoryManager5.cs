using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager5 : MonoBehaviour
{
    public static InventoryManager5 instance;

    //public int maxStackedItems = 4;
    public InventorySlot5[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject inventoryUI;

    int selectedSlot = -1;
    int hotBarNumber = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        ScrollWheelSelectedSlot();

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

   public bool AddItem(Item5 item)
   {
        //Check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot5 slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < itemInSlot.item.maxStackSize && itemInSlot.item.stackabke)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        //Find any empty slot
        for (int i = 0; i< inventorySlots.Length; i++)
        {
            InventorySlot5 slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
   }

    void SpawnNewItem(Item5 item, InventorySlot5 slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item5 GetSelectedItem(bool use)
    {
        InventorySlot5 slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            //return itemInSlot.item;
            Item5 item = itemInSlot.item;
            if (use)
            {
                itemInSlot.count--;
                if(itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }

            return item;
        }
        return null;
    }

    void ScrollWheelSelectedSlot()
    {
        //use mouseScroll to change the SelectedSlot
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            hotBarNumber++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            hotBarNumber--;
        }

        if (hotBarNumber >= 4)
            hotBarNumber = 0;
        else if (hotBarNumber < 0)
            hotBarNumber = 3;

        ChangeSelectedSlot(hotBarNumber);
    }
}
