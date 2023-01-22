using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot5 : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;

    public GameObject itemCountPrefab;

    InventoryItem selectedItem;

    bool onHighlight = false;
    float clickTime;


    private void Awake()
    {
        Deselect();
        clickTime = 0f;
        selectedItem = null;
    }

    private void Update()
    {
        SellItem();
    }

    public void Select()
    {
        if(image)
            image.color = selectedColor;
    }

    public void Deselect()
    {
        if (image)
            image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    { 


        if(transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
        else if (transform.childCount == 1)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();      //InventoryItem Script of item1
            Item5 item = inventoryItem.item;                                                        //item of item1
            Transform inventoryItemParentAfterDrag = inventoryItem.parentAfterDrag;                 //parentAfterDrag of item1 
            inventoryItem.parentAfterDrag = transform;                                              //moving item1 to target position
            //Debug.Log("inventoryItem.name: " + inventoryItem.name);

            InventoryItem inventoryItem2 = inventoryItem.parentAfterDrag.GetChild(0).GetComponent<InventoryItem>();
            Item5 item2 = inventoryItem2.GetComponent<InventoryItem>().item;        //item of item2 
            //Debug.Log("inventoryItem2.name: " + inventoryItem2.name);

            if (item.type == item2.type)
            {
                if(inventoryItem2.count + inventoryItem.count > item2.maxStackSize)     //if the amount more than maxStackSize
                {
                   
                    int tempCount = inventoryItem.count - (item2.maxStackSize - inventoryItem2.count);        //Excess quantity
                    inventoryItem2.count += item2.maxStackSize - inventoryItem2.count;     //add to maxStackSize     

                    for (int i =0; i < tempCount; i++)          //spawn a new item in inventory system
                    {
                        InventoryManager5.instance.AddItem(inventoryItem.item);
                    }
                }
                else
                {
                    inventoryItem2.count += inventoryItem.count;
                }
                
                inventoryItem2.RefreshCount();
                Destroy(inventoryItem.gameObject);
                
                
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name + ": is highlighted");
        onHighlight = true;
        //Debug.Log("transform.GetComponentInChildren<InventoryItem>().gameObject.name: " + transform.GetComponentInChildren<InventoryItem>().gameObject.name);
        if (transform.GetComponentInChildren<InventoryItem>())
        {
            selectedItem = transform.GetComponentInChildren<InventoryItem>();
        }
            //Debug.Log("selectedItem.item: " + selectedItem.item);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name + ": is not highlighted");
        onHighlight = false;
    }

    public void SellItem()
    {
        if (onHighlight)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                clickTime = Time.time;
            }

            if (Input.GetKeyUp(KeyCode.Q) && (Time.time - clickTime) < 0.2)   // short Click
            {
                Debug.Log(gameObject.name + ": is click one time");
                if (selectedItem)
                {
                    if (selectedItem.item.sellable)     //if item can sell
                    {
                        selectedItem.count--;
                        MoneyManager.instance.Sell(selectedItem.item.price);
                        if (selectedItem.count <= 0)
                        {
                            Destroy(selectedItem.gameObject);
                            selectedItem = null;
                        }
                        else
                        {
                            selectedItem.RefreshCount();
                        }
                    }
                    else            //item can not sell
                    {
                        Debug.Log("item is not sellable");
                    }
                    
                }

            }

            if (Input.GetKeyUp(KeyCode.Q) && (Time.time - clickTime) > 0.2)      // Long Click
            {
                Debug.Log("long click");

                //GameObject newItemGo = Instantiate(itemCountPrefab, transform.GetChild(0));
                //ItemSliderScript itemSliderScript = GetComponentInChildren<ItemSliderScript>();
                //itemSliderScript.SetItemMaxValue(selectedItem.count);
                //if (Input.GetKeyDown(KeyCode.KeypadEnter))
                //{
                //    int sellNum = itemSliderScript.GetItemMaxValue();
                //    if (selectedItem)
                //    {
                //        selectedItem.count -= sellNum;
                //        if (selectedItem.count <= 0)
                //        {
                //            Destroy(selectedItem.gameObject);
                //        }
                //        else
                //        {
                //            selectedItem.RefreshCount();
                //        }
                //    }

                //    itemSliderScript = null;
                //    Destroy(newItemGo);
                //}

            }
        }
        else if (!onHighlight)
        {
            clickTime = 0f;
            selectedItem = null;
        }
    }
}
