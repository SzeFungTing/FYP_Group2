using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{

    public GameObject drone;
    GameObject shopUI;

    Animator Anim_Drone;

    //[SerializeField] ShopManager shopManager;

    public bool isUIOpened = false;
  
    // Start is called before the first frame update
    void Start()
    {
        shopUI = UIScripts.instance.shopUI;

        if (drone && shopUI)
        {
            drone.SetActive(false);
            shopUI.SetActive(false);
        }
        

        Anim_Drone = drone.transform.GetComponentInChildren<Animator>();
        //Anim_Drone = drone.transform.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Anim_Drone 
            &&((Anim_Drone.GetCurrentAnimatorStateInfo(0).IsName("Start") && Anim_Drone.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) 
            || Anim_Drone.GetCurrentAnimatorStateInfo(0).IsName("StartBladeRotation")) 
            && !isUIOpened)            //drone animation end, open shopUI
        {
            UIScripts.instance.hotBarUI.SetActive(false);
            shopUI.SetActive(true);
            isUIOpened = true;
        }


        if (Input.GetKeyDown(KeyCode.C))                        //open/ close shop UI
        {
            //drone.SetActive(!drone.activeSelf);                   //open/ close drone                
            //shopUI.SetActive(!shopUI.activeSelf);

            if (shopUI.activeInHierarchy)
            {
                CloseShopUI();

            }
            else
            {
                if (!drone.activeInHierarchy)
                {
                    //shopUI.SetActive(true);
                    drone.SetActive(true);
                    //Vector3 playerPos = transform.position;
                    //Debug.Log("transform.position: "  + transform.position);
                    //playerPos.y += 1f;

                    //Vector3 playerDirection = transform.forward;
                    //Debug.Log("playerDirection: " + playerDirection);

                    //Vector3 spawnOffset = playerPos + playerDirection *5f;
                    //Debug.Log("spawnOffset: " + spawnOffset);

                    //spawnOffset.x -= 0.7f;
                    //spawnOffset.z += 3.39f;
                    drone.transform.position = /*spawnOffset*/transform.GetChild(4).position;
                    //Debug.Log("drone.transform.position: " + drone.transform.position);

                    //isUIOpened = true;
                }

            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))                     //Escape to close shopUI  
        {
            //shopUI.SetActive(false);
            //drone.SetActive(false);
            //isUIOpened = false;
            CloseShopUI();
        }
    }

    public void CloseShopUI()
    {
        Debug.Log("CloseShopUI");
        UIScripts.instance.hotBarUI.SetActive(true);

        shopUI.SetActive(false);
        if(!ShopManager.instance.isBought)
            drone.SetActive(false);

        isUIOpened = false;
    }
}
