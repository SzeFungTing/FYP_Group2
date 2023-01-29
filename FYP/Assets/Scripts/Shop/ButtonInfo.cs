using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Image itemImage;
    public Text PriceTxt;
    public Text QuantityTxt;
    public Text LimitTxt;
    public Text NameTxt;
    public GameObject SItem;

    [HideInInspector] public float price;
    [HideInInspector] public int quantity;
    [HideInInspector] public int limit;

    public GameObject ShopManager;


    private void Start()
    {

        //price = itemInfo.originalPrice;

        //limit = itemInfo.limit;


    }

    // Update is called once per frame
    void Update()
    {
        //PriceTxt.text = "Price: $" + ShopManager.GetComponent<ShopManager>().shopItems[2, ItemID].ToString();
        //QuantityTxt.text = ShopManager.GetComponent<ShopManager>().shopItems[3, ItemID].ToString();
        //LimitTxt.text = ShopManager.GetComponent<ShopManager>().shopItems[4, ItemID].ToString();
        
    }

    public void SetTemplate(GameObject item)
    {
        Item5 itemInfo = item.GetComponent<WorldItem>().item;
        itemImage.sprite = itemInfo.image;

        price = itemInfo.originalPrice;
        PriceTxt.text = "Price: $" + price;

        quantity = 0;
        //LimitTxt.text = quantity.ToString();

        limit = itemInfo.limit;
        //LimitTxt.text = limit.ToString();

        NameTxt.text = "-" + itemInfo.itemDisplayName + "-";
        SItem = item;

        RefreshTemplate();
    }

    public void RefreshTemplate()
    {
        LimitTxt.text = limit.ToString();
        QuantityTxt.text = quantity.ToString();
    }
}
