using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBuildingSystem : MonoBehaviour
{
    public GameObject buildingGrid;
    BuildingSystem buildingSystem;

    PlaceableObject objectToPlace;

    private void Start()
    {
        buildingSystem = buildingGrid.GetComponent<BuildingSystem>();
        //buildingGrid.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //buildingGrid.SetActive(!buildingGrid.activeSelf);
            if (!buildingGrid.activeInHierarchy)
            {
                buildingGrid.SetActive(true);
            }
            else
            {
                //if (objectToPlace)
                {
                    objectToPlace = buildingSystem.GetPlaceableObject();
                    if (objectToPlace && objectToPlace.GetComponent<ObjectDrag>())
                    {
                        buildingSystem.SetCanBuild(false);
                        Destroy(objectToPlace.gameObject);
                    }
                }

                buildingGrid.SetActive(false);
            }
            
        }
    }
}
