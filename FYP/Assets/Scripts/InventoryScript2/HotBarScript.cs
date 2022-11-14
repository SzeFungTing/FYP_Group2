using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarScript : MonoBehaviour
{
    public GameObject selecter;
    Transform selecterPos;

    GameObject hotBar1;
    GameObject hotBar2;
    GameObject hotBar3;
    GameObject hotBar4;

    public GameObject h1;
    GameObject hotBarNum;
    //public GameObject gunGameObject;
    public GunShooting gunShooting;
    //public Vector3 gunPos;

    int currentPos = 0;

    //Item2 item;

    private Inventory2 inventory;
    //private Transform itemSlotContainer;
    //private Transform itemSlotTemplate;

    // Start is called before the first frame update
    void Awake()
    {
        hotBar1 = transform.GetChild(0).gameObject;
        hotBar2 = transform.GetChild(1).gameObject;
        hotBar3 = transform.GetChild(2).gameObject;
        hotBar4 = transform.GetChild(3).gameObject;
        
        selecterPos = selecter.GetComponent<Transform>();

        //gunPos = gunShooting.gameObject.transform;



        //itemSlotContainer = transform.Find("itemSlotContainer");
        //itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward, right
        {
            currentPos += 1;
            SelecterPos();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards, left
        {
            currentPos -= 1;
            SelecterPos();
        }   
    }

    public void SetInventory(Inventory2 inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        Debug.Log("updata InventoryItems");
        foreach (Transform child in this.transform)
        {
            if (child == hotBar1.GetComponent<Transform>() /*|| (child.GetComponent<Image>().sprite == null)*/) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        //int y = 0;
        float itemSlotCellSize = 30f;

        foreach (Item2 item in inventory.GetItemList())
        {


            RectTransform itemSlotRectTransform = Instantiate(hotBar1, this.transform).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);


            //if (Input.GetButtonDown("Fire1"))
            //{
            //    inventory.RemoveItem(item);
            //    Debug.Log("removeItem: " + item.itemType);
            //    ItemWorld.DropItem(gunShooting.GetGunPos(), item, gunShooting.transform);
            //}


            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, 0);
            Image image = /*SetHorBarItem()*/itemSlotRectTransform.GetChild(0).GetComponent<Image>();
            image.sprite = item.GetSprite();
            itemSlotRectTransform.GetChild(0).GetComponent<ItemWorld>().SetItem(item);
            //Debug.Log(item.itemType);

            Text uiText = itemSlotRectTransform.GetChild(0).GetChild(0).GetComponent<Text>();


            //GameObject hb = SetHorBarItem();

            //Image image = hb.GetComponent<RectTransform>().GetComponent<Image>();
            //image.sprite = item.GetSprite();
            //Debug.Log("hotbarItemType: " + item.itemType);

            //Text uiText = hb.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>();

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

    public Item2 GetHotbarItem()
    {
        return transform.GetChild(currentPos+1).GetChild(0).GetComponent<ItemWorld>().item;
    }

    GameObject SetHorBarItem()
    {
        
        if(IsNull(transform.GetChild(currentPos).gameObject))
        {
            hotBarNum = transform.GetChild(currentPos).gameObject;
            Debug.Log("get: " + transform.GetChild(currentPos).gameObject);
        }
        else 
        {
            if(IsNull(hotBar1))
            {
                hotBarNum = hotBar1;
                Debug.Log("new in hotBar1");
            }
            else if (IsNull(hotBar2))
            {
                hotBarNum = hotBar2;
                Debug.Log("new in hotBar2");
            }
            else if (IsNull(hotBar3))
            {
                hotBarNum = hotBar3;
                Debug.Log("new in hotBar3");
            }
            else if (IsNull(hotBar4))
            {
                hotBarNum = hotBar4;
                Debug.Log("new in hotBar4");
            }
        }
        //Debug.Log(hotBarNum);
        
        return hotBarNum;
    }


    bool IsNull(GameObject hotbar)
    {
        if (hotbar.GetComponent<Image>().sprite == null)
        {
            Debug.Log(hotbar + "is null");
            return true;
        }
        else
        {
            Debug.Log(hotbar + "is not null");
            return false;
            
        }
            
    }



    void SelecterPos()
    {
        int maxNum = transform.childCount -1; 
        Debug.Log(maxNum);
        if (currentPos >= maxNum)     
        {
            currentPos = 0;
        }
        else if (currentPos <= -1)
        {
            currentPos = maxNum-1;
        }

        if (currentPos == 0)
            selecterPos.position = /*hotBar1*/transform.GetChild(1).GetComponent<Transform>().position;
        else if (currentPos == 1)
            selecterPos.position = /*hotBar2*/transform.GetChild(2).GetComponent<Transform>().position;
        else if (currentPos == 2)
            selecterPos.position = /*hotBar3*/transform.GetChild(3).GetComponent<Transform>().position;
        else if (currentPos == 3)
            selecterPos.position = /*hotBar4*/transform.GetChild(4).GetComponent<Transform>().position;
        Debug.Log("currentPos: " + currentPos);
        Debug.Log("GetHotbarItem(): " + GetHotbarItem().itemType);
    }







}


















//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class HotBarScript : MonoBehaviour
//{
//    public GameObject selecter;
//    Transform selecterPos;

//    Transform hotBar1;
//    Transform hotBar2;
//    Transform hotBar3;
//    Transform hotBar4;

//    int currentPos = 1;
//    // Start is called before the first frame update
//    void Start()
//    {
//        hotBar1 = this.gameObject.transform.GetChild(0);
//        hotBar2 = this.gameObject.transform.GetChild(1);
//        hotBar3 = this.gameObject.transform.GetChild(2);
//        hotBar4 = this.gameObject.transform.GetChild(3);


//        selecterPos = selecter.GetComponent<Transform>();
//    }

//    // Update is called once per frame
//    void Update()
//    {




//        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward, right
//        {
//            currentPos += 1;
//            SelecterPos();
//        }
//        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards, left
//        {
//            currentPos -= 1;
//            SelecterPos();
//        }
//    }


//    void SelecterPos()
//    {
//        if (currentPos >= 5)
//        {
//            currentPos = 1;
//        }
//        else if (currentPos <= 0)
//        {
//            currentPos = 4;
//        }

//        if (currentPos == 1)
//            selecterPos.position = hotBar1.position;
//        else if (currentPos == 2)
//            selecterPos.position = hotBar2.position;
//        else if (currentPos == 3)
//            selecterPos.position = hotBar3.position;
//        else if (currentPos == 4)
//            selecterPos.position = hotBar4.position;
//    }
//}

