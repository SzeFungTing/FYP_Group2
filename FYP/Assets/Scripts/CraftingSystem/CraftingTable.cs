using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTable : MonoBehaviour
{
    /*[SerializeField] */public Image recipeImage;
    [SerializeField] private List<CraftingRecipeSO> craftingRecipeSOList;
    [SerializeField] private BoxCollider placeItemsAreaBoxCollider;
    [SerializeField] private Transform itemSpawnPoint;

    private CraftingRecipeSO craftingRecipeSO;

    private void Awake()
    {
        NextRecipe();
    }

    public void NextRecipe()
    {
        if (craftingRecipeSO == null)
        {
            craftingRecipeSO = craftingRecipeSOList[0];
        }
        else
        {
            int index = craftingRecipeSOList.IndexOf(craftingRecipeSO);
            index = (index + 1) % craftingRecipeSOList.Count;
            craftingRecipeSO = craftingRecipeSOList[index];
        }

        recipeImage.sprite = craftingRecipeSO.sprite;
    }

    public void Craft()
    {
        Debug.Log("Craft");
        Collider[] colliderArray = Physics.OverlapBox(
            transform.position + placeItemsAreaBoxCollider.center,
            placeItemsAreaBoxCollider.size,
            placeItemsAreaBoxCollider.transform.rotation);


        List<Item5> inputItemList = new List<Item5>(craftingRecipeSO.inputItemSOList);
        List<GameObject> consumeItemGameObjectList = new List<GameObject>();

        foreach(Collider collider in colliderArray)
        {
            if(collider.TryGetComponent(out WorldItem worldItem))
            {
                if (inputItemList.Contains(worldItem.item))
                {
                    inputItemList.Remove(worldItem.item);
                    consumeItemGameObjectList.Add(collider.gameObject);
                }
            }
        }
            
        if(inputItemList.Count == 0)        //Have all the required item to craft
        {
            Debug.Log("Yes");
            //Transform spawnedItemTransform = 
                Instantiate(craftingRecipeSO.outputItemSO, itemSpawnPoint.position, itemSpawnPoint.rotation);
            
            foreach(GameObject consumeItemGameObject in consumeItemGameObjectList)
            {
                Destroy(consumeItemGameObject);
            }
        }
        else
        {
            Debug.Log("No");

        }
    }
}
