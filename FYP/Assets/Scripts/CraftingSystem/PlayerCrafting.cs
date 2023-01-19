using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrafting : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask interactLayerMask;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
        {
            float interactDistance = 3f;
            if(Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, interactDistance))
            {
                if(raycastHit.transform.TryGetComponent(out CraftingTable craftingTable) || (raycastHit.transform.GetComponent<CraftingTable>() && raycastHit.transform.GetComponent<CraftingTable>().recipeImage.transform.parent))
                {

                    //Interacting with Crafting table
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //next recipe
                        craftingTable.NextRecipe();
                    }
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        //start craft
                        craftingTable.Craft();
                    }
                } 
            }
        }

        float interactDistance2 = 3f;
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit2, interactDistance2))
        {
            if (raycastHit2.transform.TryGetComponent(out CraftingTable craftingTable) || (raycastHit2.transform.GetComponent<CraftingTable>() && raycastHit2.transform.GetComponent<CraftingTable>().recipeImage.transform.parent))
            {
                UIScripts.instance.buttonIndicationUI.SetActive(true);
            }
        }
        else
        {
            UIScripts.instance.buttonIndicationUI.SetActive(false);

        }
    }
}
