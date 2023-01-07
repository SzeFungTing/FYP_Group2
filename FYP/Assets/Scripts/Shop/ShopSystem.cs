using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{

    public GameObject drone;
    public GameObject shopUI;

    Animator Anim_Drone;
  
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

        if (Anim_Drone.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            shopUI.SetActive(true);


        if (Input.GetKeyDown(KeyCode.C))
        {
            
            drone.SetActive(!drone.activeSelf);
            if (shopUI.activeInHierarchy)
            {
                shopUI.SetActive(!shopUI.activeSelf);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            shopUI.SetActive(false);
            drone.SetActive(false);
         

        }

    }
}
