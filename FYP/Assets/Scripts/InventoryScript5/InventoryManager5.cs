using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager5 : MonoBehaviour
{
    public static InventoryManager5 instance;

    //public int maxStackedItems = 4;
    public InventorySlot5[] inventorySlots;
    public GameObject inventoryItemPrefab;
    [HideInInspector]public GameObject inventoryUI;
    [HideInInspector] public GameObject hotBarUI;
    public GunShooting gunShooting;

    //Synchronize Hot Bar
    //public InventorySlot5[] backpackHotbarSlots;
    //public InventorySlot5[] hotbarSlots;

    bool isOpenBackpack = false;

    int selectedSlot = -1;
    int hotBarNumber = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventoryUI = UIScripts.instance.backPackUI;
        hotBarUI = UIScripts.instance.hotBarUI;

        inventoryUI.SetActive(false);
        hotBarUI.SetActive(true);
        ChangeSelectedSlot(0);
    }

    private void Update()
    {

        ScrollWheelSelectedSlot();
      

        ControlUI();
        
        //if (inventoryUI.activeInHierarchy)
        //{
        //    Cursor.lockState = CursorLockMode.Confined;
        //}
        //else
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        if (!isOpenBackpack)
            inventorySlots[newValue].Select();
        else
            inventorySlots[newValue].Deselect();

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
                SpawnNewItem(item, slot, 1);
                return true;
            }
        }
        return false;
   }

    public void SpawnNewItem(Item5 item, InventorySlot5 slot, int count)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item, count);
    }

    public Item5 GetSelectedItem(bool use)
    {
        //Debug.Log("remove" + " + use: " + use);
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



    //public void SynchronizeHotBar()
    //{
    //    for (int i = 0; i < hotbarSlots.Length; i++)            //open backpack, and Synchronize HotBar
    //    {
    //        InventorySlot5 backpackHotBarSlot = backpackHotbarSlots[i];                                                         //define backpackHotbar Slots
    //        InventoryItem itemInBackpackHotBarSlot = backpackHotBarSlot.GetComponentInChildren<InventoryItem>();        //define item of backpackHotbar Slots
    //        //for (int j = 0; j < backpackHotbarSlots.Length; j++)        //clean backpackHotbar Slots
    //        if (itemInBackpackHotBarSlot != null)                                       //copy hotBarSlot to backpackHotBarSlot
    //        {
    //            Destroy(itemInBackpackHotBarSlot.gameObject);
    //        }

    //        InventorySlot5 hotBarSlot = hotbarSlots[i];                                                                     //define Hotbar Slots
    //        InventoryItem itemInHotBarSlot = hotBarSlot.GetComponentInChildren<InventoryItem>();                            //define item of Hotbar Slots
    //        if (itemInHotBarSlot != null)                                       //copy hotBarSlot to backpackHotBarSlot
    //        {                                                                                                       
    //            SpawnNewItem(itemInHotBarSlot.item, backpackHotBarSlot);
    //            Debug.Log("itemInHotBarSlot.item: " + itemInHotBarSlot.item);
    //            Debug.Log("itemInBackpackHotBarSlot.item: " + itemInBackpackHotBarSlot.item);

    //            //itemInBackpackHotBarSlot.count = itemInHotBarSlot.count;
    //            //Debug.Log("itemInBackpackHotBarSlot.count: " + itemInBackpackHotBarSlot.count);
    //            //Debug.Log("itemInHotBarSlot.count: " + itemInHotBarSlot.count);

    //            //itemInBackpackHotBarSlot.RefreshCount();
    //        }
    //    }
    //}

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

    void ControlUI()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryUI.activeInHierarchy)     //open backpack
            {
                isOpenBackpack = true;
                gunShooting.enabled = false;
                inventoryUI.SetActive(true);
                //SynchronizeHotBar();
                hotBarUI.transform.position = new Vector3(959.9999389648438f, 995.0f, 0.5400000214576721f);     //up
                hotBarUI.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                
            }
            else                //close backpack
            {
                //isOpenBackpack = false;
                //gunShooting.enabled = true;
                //inventoryUI.SetActive(false);
                //hotBarUI.transform.position = new Vector3(959.9999389648438f, 90.0f, 0.5400000214576721f);      //down
                //hotBarUI.transform.localScale = new Vector3(1f, 1f, 1f);
                CloseUI();
            }
        }
    } 

    public void CloseUI()
    {
        isOpenBackpack = false;
        gunShooting.enabled = true;
        inventoryUI.SetActive(false);
        hotBarUI.transform.position = new Vector3(959.9999389648438f, 90.0f, 0.5400000214576721f);      //down
        hotBarUI.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
