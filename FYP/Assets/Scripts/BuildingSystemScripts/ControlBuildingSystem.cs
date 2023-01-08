using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBuildingSystem : MonoBehaviour
{
    public GameObject buildingGrid;
    BuildingSystem buildingSystem;

    PlaceableObject objectToPlace;

    public GameObject player;
    GunShooting gunShooting;
    GunVacuum gunVacuum;

    private void Start()
    {
        buildingGrid.SetActive(false);

        buildingSystem = buildingGrid.GetComponent<BuildingSystem>();
        gunShooting = player.GetComponentInChildren<GunShooting>();
        gunVacuum = player.GetComponentInChildren<GunVacuum>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //buildingGrid.SetActive(!buildingGrid.activeSelf);
            if (!buildingGrid.activeInHierarchy)                        //open building system
            {
                buildingGrid.SetActive(true);
                gunShooting.enabled = false;
                gunVacuum.enabled = false;
            }
            else                                                        //close building system
            {

                objectToPlace = buildingSystem.GetPlaceableObject();
                if (objectToPlace && objectToPlace.GetComponent<ObjectDrag>())
                {
                    buildingSystem.SetCanBuild(false);
                    Destroy(objectToPlace.gameObject);
                }


                buildingGrid.SetActive(false);

                gunShooting.enabled = true;
                gunVacuum.enabled = true;
            }
            
        }
    }
}
