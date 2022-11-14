using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackScript : MonoBehaviour
{
    public GameObject hotBar1;

    private Inventory2 inventory;

    public GunVacuum GunVacuum;

    private void Start()
    {
        inventory = new Inventory2();

        GunVacuum.SetInventory(inventory);

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    //public void SetInventory(Inventory2 inventory)
    //{
    //    inventory = this.inventory;

    //    //inventory.OnItemListChanged += Inventory_OnItemListChanged;

    //    //RefreshInventoryItems();
    //}

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        
        Debug.Log("updata BackpackItems");
        Debug.Log("BackpackInventory.GetItemList(): " + inventory.GetItemList().Count);
        foreach (Transform child in this.transform.transform)
        {
            if (child == hotBar1.GetComponent<Transform>() ) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        //int y = 0;
        float itemSlotCellSize = 30f;

        foreach (Item2 item in inventory.GetItemList())
        {
            Debug.Log("BackpackInventory.GetItemList(): " + inventory.GetItemList().Count);
            Debug.Log("itemType: " + item.itemType);


            RectTransform itemSlotRectTransform = Instantiate(hotBar1, this.transform).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);



            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, 0);
            Image image = /*SetHorBarItem()*/itemSlotRectTransform.GetChild(0).GetComponent<Image>();
            image.sprite = item.GetSprite();
            itemSlotRectTransform.GetChild(0).GetComponent<ItemWorld>().SetItem(item);
            //Debug.Log(item.itemType);

            Text uiText = itemSlotRectTransform.GetChild(0).GetChild(0).GetComponent<Text>();



            if (item.amount > 1)
            {
                uiText.text = item.amount.ToString();
            }
            else
            {
                uiText.text = "";
            }


            x++;
            //if (x > 4)
            //{
            //    x = 0;
            //    y++;
            //}
        }
    }
}
