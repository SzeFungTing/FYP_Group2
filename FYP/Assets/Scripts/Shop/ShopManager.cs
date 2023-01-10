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

    public Button building, upgrade, equipment, item;
    GameObject ButtonRef;

    public bool isBought = false;
    public float Speed = 5f;



    Animator Anim_Drone;

    // Start is called before the first frame update
    void Start()
    {
        Anim_Drone = ShootingP.gameObject.GetComponent<Animator>();
        Anim_Drone.SetBool("is_Shooted", false);
        Jetpack.SetActive(false);

        CoinsTXT.text = "Coins: $" + /*coins*/MoneyManager.instance.coins.ToString();

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

    public void Update()
    {
        if (isBought)                                           
        {
            if (Anim_Drone.GetCurrentAnimatorStateInfo(0).IsName("TurnAround") && Anim_Drone.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)         //check animation end?, Instantiate ther
            {
                var stuff = Instantiate(ButtonRef.GetComponent<ButtonInfo>().SItem, ShootingP.position, Random.rotation);
                stuff.GetComponentInChildren<Rigidbody>().velocity = ShootingP.forward * Speed;
                Anim_Drone.SetBool("is_Shooted", false);
                isBought = false;
            }
        }


        if (shopItems[4, 2] <= 0)
        {
            Jetpack.SetActive(true);
            isBought = true;
        }
    }


    // Update is called once per frame
    public void Buy()
    {

        /*GameObject*/ ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;




        if (shopItems[4, ButtonRef.GetComponent<ButtonInfo>().ItemID] > 0)
        {


            if (/*coins*/MoneyManager.instance.coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
            {
                /*coins*/MoneyManager.instance.coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];

                Anim_Drone.SetBool("is_Shooted", true);
                isBought = true;


                shopItems[4, ButtonRef.GetComponent<ButtonInfo>().ItemID]--;
                shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;

                CoinsTXT.text = "Coins: $" + MoneyManager.instance.coins.ToString(); //update the coins player have
                ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
                ButtonRef.GetComponent<ButtonInfo>().LimitTxt.text = shopItems[4, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();

            }

        }
    }
    public void Button(GameObject button )
    {
        Select.transform.position = button.transform.position;
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
