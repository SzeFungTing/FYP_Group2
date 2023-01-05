using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBuildingSystem : MonoBehaviour
{
    public GameObject buildingGrid;

    private void Start()
    {
        buildingGrid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildingGrid.SetActive(!buildingGrid.activeSelf);
        }
    }
}
