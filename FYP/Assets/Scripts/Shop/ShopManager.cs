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

    public GameObject Select;
    public GameObject Jetpack;

    public Button building, upgrade, equipment, item;

    public bool isBought = false;


    // Start is called before the first frame update
    void Start()
    {
        Jetpack.SetActive(false);

        CoinsTXT.text = "Coins: $" + coins.ToString();

        //ID
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        //Price
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 1000; //jetpack
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 40;

        //Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;

        //Limit
        shopItems[4, 1] = 3;
        shopItems[4, 2] = 1;
        shopItems[4, 3] = 0;
        shopItems[4, 4] = 0;


    }

    public void Update()
    {
        if (shopItems[4, 2] <= 0)
        {
            Jetpack.SetActive(true);
            isBought = true;
        }
    }


    // Update is called once per frame
    public void Buy()
    {

        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

       

        if (shopItems[4, ButtonRef.GetComponent<ButtonInfo>().ItemID] > 0)
        {
         
            if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
            {
                coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];

                shopItems[4, ButtonRef.GetComponent<ButtonInfo>().ItemID]--;
                shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;

               Instantiate(ButtonRef.GetComponent<ButtonInfo>().SItem, transform.position,new Quaternion(-0.7071068f, 0,0, 0.7071068f));

                CoinsTXT.text = "Coins: $" + coins.ToString(); //update the coins player have
                ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
                ButtonRef.GetComponent<ButtonInfo>().LimitTxt.text = shopItems[4, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();

            }

        }
    }
    public void Button(GameObject button)
    {
        Select.transform.position= button.transform.position;
    }

}
