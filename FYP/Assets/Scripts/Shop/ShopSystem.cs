using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{

    public GameObject drone;
  
    // Start is called before the first frame update
    void Start()
    {
        drone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            drone.SetActive(!drone.activeSelf);

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            drone.SetActive(false);

        }

    }
}
