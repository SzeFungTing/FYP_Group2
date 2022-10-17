using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;



    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(false);
  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.Escape) && Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(false);

        }
    }
}