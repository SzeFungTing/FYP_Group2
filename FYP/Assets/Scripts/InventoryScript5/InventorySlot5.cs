using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot5 : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;

    

    private void Awake()
    {
        Deselect();
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

  
}
