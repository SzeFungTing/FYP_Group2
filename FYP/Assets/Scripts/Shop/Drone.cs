using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public Transform Player;

    //GameObject shopUI;

    // Start is called before the first frame update
    void Start()
    {
        //shopUI = UIScripts.instance.shopUI;

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);

        //if (!shopUI.activeInHierarchy && ShopManager.instance.isBought)
        //{

        //}
    }
}
