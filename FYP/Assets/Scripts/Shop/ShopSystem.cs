using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{

    public GameObject drone;
    public GameObject shopUI;

    Animator Anim_Drone;

    public bool isUIOpened = false;
  
    // Start is called before the first frame update
    void Start()
    {
        drone.SetActive(false);
        shopUI.SetActive(false);

        Anim_Drone = drone.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Anim_Drone.GetCurrentAnimatorStateInfo(0).IsName("Start") && Anim_Drone.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !isUIOpened)            //drone animation end, open shopUI
        {
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
                shopUI.SetActive(true);
                drone.SetActive(true);
                Vector3 playerOffset = transform.position;
                playerOffset.x += 2.5f;
                playerOffset.y += 2.5f;
                playerOffset.z += 2.5f;
                drone.transform.position = playerOffset;
                isUIOpened = true;
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
        shopUI.SetActive(false);
        drone.SetActive(false);
        isUIOpened = false;
    }
}
