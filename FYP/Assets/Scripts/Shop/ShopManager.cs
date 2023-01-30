using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    public float coins;
    public Text CoinsTXT;

    public Transform ShootingP;
    public GameObject Select;
    public GameObject Jetpack;

    public GameObject shopUI;
    Transform buildingPlane, upgradePlane, equipmentPlane, itemPlane;
    public Button building, upgrade, equipment, item;
    GameObject ButtonRef;

    public bool isBought = false;
    public float Speed = 5f;
    //public ShopSystem Ss;

    public List<GameObject> sellingItems;
    public GameObject sellingTemplate;

    public List<GameObject> purchaseList;

    Animator Anim_Drone;

    //float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        buildingPlane = shopUI.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0);
        upgradePlane = shopUI.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0);
        equipmentPlane = shopUI.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0);
        itemPlane = shopUI.transform.GetChild(1).GetChild(3).GetChild(0).GetChild(0);

        Anim_Drone = ShootingP.gameObject.GetComponent<Animator>();
        Anim_Drone.SetBool("is_Shooted", false);
        Jetpack.SetActive(false);

        CoinsTXT.text = "Coins: $" + /*coins*/MoneyManager.instance.coins.ToString();

        foreach (GameObject sellingItem in sellingItems)
        {
            //Debug.Log("spawn");
            GameObject temp = Instantiate(sellingTemplate, Classification(sellingItem));

            ButtonInfo buttonInfo = temp.GetComponent<ButtonInfo>();
            buttonInfo.SetTemplate(sellingItem);


            Button btn = buttonInfo.GetComponent<Button>();
            btn.onClick.AddListener(Buy);

        }

        //ID
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        //Price
        shopItems[2, 1] = 30;
        shopItems[2, 2] = 1000; //jetpack
        shopItems[2, 3] = 10;
        shopItems[2, 4] = 20;

        //Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;

        //Limit
        shopItems[4, 1] = 3;
        shopItems[4, 2] = 1;
        shopItems[4, 3] = 20;
        shopItems[4, 4] = 10;


    }

    #region emitItem
    int purchasingIdx = 0;
    float emitTime = 0;
    float emitInterval = 1f;
    #endregion

    public void Update()
    {
        if (isBought)
        {
            if(purchasingIdx < purchaseList.Count)
            {
                //Debug.Log("isBought");
                if (Anim_Drone.GetCurrentAnimatorStateInfo(0).IsName("TurnAround") && Anim_Drone.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Time.time > emitTime+ emitInterval)         //check animation end?, Instantiate ther
                {

                    //foreach (GameObject item in purchaseList)
                    ////for(int i =0;i< purchaseList.Count;i++)
                    //{
                    //    {

                Debug.Log("spawn, normalized time:"+ Anim_Drone.GetCurrentAnimatorStateInfo(0).normalizedTime);
                GameObject stuff = Instantiate(purchaseList[purchasingIdx++], ShootingP.position, Random.rotation);

                stuff.GetComponentInChildren<Rigidbody>().velocity = ShootingP.forward * Speed;
                emitTime = Time.time;

                    //    }
                    //}
                }
            }
            else
            {
                purchaseList = new List<GameObject>();
                Anim_Drone.SetBool("is_Shooted", false);
                Debug.Log("End Aimation");
                isBought = false;

                purchasingIdx = 0;
                emitTime = 0;
            }

        }



        //if (!Ss.isUIOpened)
        //{


        //        if (Anim_Drone.GetCurrentAnimatorStateInfo(0).IsName("TurnAround") && Anim_Drone.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)         //check animation end?, Instantiate ther
        //        {
        //            for(int i = 1; i < 5; i++)
        //            {

        //                if (shopItems[3, i] != 0) 
        //                {
        //                    var stuff = Instantiate(ButtonRef.GetComponent<ButtonInfo>().SItem, ShootingP.position, Random.rotation);
        //                    stuff.GetComponentInChildren<Rigidbody>().velocity = ShootingP.forward * Speed;
        //                    Anim_Drone.SetBool("is_Shooted", false);
        //                    shopItems[3, i] = 0;
        //                }
        //            }
        //        }
        //}

        //if (isBought)
        //{
        //    if (Anim_Drone.GetCurrentAnimatorStateInfo(0).IsName("TurnAround") && Anim_Drone.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)         //check animation end?, Instantiate ther
        //    {
        //        if(ButtonRef.GetComponent<ButtonInfo>().ItemID != 2)
        //        {
        //            var stuff = Instantiate(ButtonRef.GetComponent<ButtonInfo>().SItem, ShootingP.position, Random.rotation);
        //            stuff.GetComponentInChildren<Rigidbody>().velocity = ShootingP.forward * Speed;

        //        }

        //        Anim_Drone.SetBool("is_Shooted", false);
        //        isBought = false;
        //    }
        //}



        if (shopItems[4, 2] <= 0)
        {
            Jetpack.SetActive(true);
            isBought = true;
        }
    }


    // Update is called once per frame
    public void Buy()
    {
        Debug.Log("buy");

        /*GameObject*/
        ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        //int tempLimit;
        //int.TryParse(ButtonRef.GetComponent<ButtonInfo>().LimitTxt.text, out tempLimit);
        //int tempPrice;
        //int.TryParse(ButtonRef.GetComponent<ButtonInfo>().PriceTxt.text, out tempPrice);
        //int tempQuantity;
        //int.TryParse(ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text, out tempQuantity);


        //if (shopItems[4, ButtonRef.GetComponent<ButtonInfo>().ItemID] > 0)
        if (ButtonRef.GetComponent<ButtonInfo>().limit > 0)
        {


            if (/*coins*/MoneyManager.instance.coins >= /*shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID]*/ButtonRef.GetComponent<ButtonInfo>().price)
            {
                //Debug.Log("tempPrice: " + ButtonRef.GetComponent<ButtonInfo>().price);
                //Anim_Drone.SetBool("is_Shooted", true);
                //isBought = true;

                //if ((Anim_Drone.GetCurrentAnimatorStateInfo(0).IsName("TurnAround") && Anim_Drone.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) || Anim_Drone.GetCurrentAnimatorStateInfo(0).IsName("Start"))
                {
                    /*coins*/
                    MoneyManager.instance.coins -= /*shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID]*/ButtonRef.GetComponent<ButtonInfo>().price;

                    /*shopItems[4, ButtonRef.GetComponent<ButtonInfo>().ItemID]*/
                    ButtonRef.GetComponent<ButtonInfo>().limit--;
                    /*shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]*/
                    ButtonRef.GetComponent<ButtonInfo>().quantity++;

                    CoinsTXT.text = "Coins: $" + MoneyManager.instance.coins.ToString(); //update the coins player have
                    //ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = /*shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]*/tempQuantity.ToString();
                    //ButtonRef.GetComponent<ButtonInfo>().LimitTxt.text = /*shopItems[4, ButtonRef.GetComponent<ButtonInfo>().ItemID]*/tempLimit.ToString();
                    ButtonRef.GetComponent<ButtonInfo>().RefreshTemplate();

                    purchaseList.Add(ButtonRef.GetComponent<ButtonInfo>().SItem);
                }


            }

        }
    }
    public void Button(GameObject button)
    {
        Select.transform.position = button.transform.position;
    }

    public Transform Classification(GameObject item)
    {
        ItemType itemInfo = item.GetComponent<WorldItem>().item.type;

        switch (itemInfo)
        {
            default:
            case ItemType.Material:
                return buildingPlane;

        }
    }

    public void PlaceAnOrder()
    {
        if (purchaseList.Count > 0)
        {
            Anim_Drone.SetBool("is_Shooted", true);
            Debug.Log("purchaseList.Count > 0");

            isBought = true;
        }

    }

    IEnumerator SpawnObject()
    {
        Debug.Log("call");

        //if (Anim_Drone.GetCurrentAnimatorStateInfo(0).IsName("TurnAround") && Anim_Drone.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)         //check animation end?, Instantiate ther
        //{
        //    Debug.Log("Start");

        foreach (GameObject item in purchaseList)
        //for(int i =0;i< purchaseList.Count;i++)
        {
            //if (ButtonRef.GetComponent<ButtonInfo>().ItemID != 2)
            {
                Debug.Log("spawn");

                GameObject stuff = Instantiate(item, ShootingP.position, Random.rotation);
                //GameObject stuff = Instantiate(purchaseList[i], ShootingP.position, Random.rotation);
                stuff.GetComponentInChildren<Rigidbody>().velocity = ShootingP.forward * Speed;
                //purchaseList.Remove(item);
                yield return new WaitForSeconds(3f);

            }
        }
       
        //Debug.Log("end");

        //purchaseList = new List<GameObject>();
        //Anim_Drone.SetBool("is_Shooted", false);
        //isBought = false;

        //}
    }

 

    //public void TurnAround(GameObject _ButtonRef)
    //{
    //    if (Anim_Drone.GetCurrentAnimatorStateInfo(0).IsName("TurnAround"))
    //    {

    //        var stuff = Instantiate(_ButtonRef.GetComponent<ButtonInfo>().SItem, ShootingP.position, Random.rotation);
    //        stuff.GetComponentInChildren<Rigidbody>().velocity = ShootingP.forward * Speed;
    //        Anim_Drone.SetBool("is_Shooted", false);

    //    }  
    //}
}
