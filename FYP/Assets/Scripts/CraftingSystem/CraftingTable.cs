using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTable : MonoBehaviour
{
    /*[SerializeField] */
    public Image recipeImage;
    [SerializeField] private List<CraftingRecipeSO> craftingRecipeSOList;
    [SerializeField] private BoxCollider placeItemsAreaBoxCollider;
    [SerializeField] private Transform itemSpawnPoint;
    [SerializeField] private Transform returnMaterialPoint;

    private CraftingRecipeSO craftingRecipeSO;

    List<GameObject> consumeItemGameObjectList;
    List<Item5> inputItemList;
    List<Transform> displayList;
    List<Transform> previewDisplayList;

    GameObject craftingUI;
    GameObject previewCraftingUI;


    //ConsumeItem consumeItem;

    //Collider[] colliderArray;

    private void Awake()
    {
        craftingUI = UIScripts.instance.CraftingUI;
        previewCraftingUI = transform.GetChild(0).GetChild(0).gameObject;
        displayList = new List<Transform>();
        previewDisplayList = new List<Transform>();

        NextRecipe();
    }

    private void Start()
    {
        //consumeItemGameObjectList = new List<GameObject>();
        //consumeItem = GetComponentInChildren<ConsumeItem>();

    }

    private void Update()
    {
        ConsumeMaterial();

        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (GameObject go in consumeItemGameObjectList)
                Debug.Log("consumeItemGameObjectList: " + go);
            foreach (Item5 item in inputItemList)
                Debug.Log("inputItemList: " + item);
        }
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

        UpdateDisplayList();
        previewCraftingUI.transform.GetChild(0).GetComponent<Image>().sprite = craftingRecipeSO.sprite;
        craftingUI.transform.GetChild(0).GetComponent<Image>().sprite = craftingRecipeSO.sprite;
        DisplayRecipe_Material();



        inputItemList = new List<Item5>(craftingRecipeSO.inputItemSOList);




        if ((consumeItemGameObjectList != null) && consumeItemGameObjectList.Count > 0)
        {
            foreach (GameObject consumeItemGameObject in consumeItemGameObjectList)
            {
                Debug.Log("restart");

                GameObject returnObject = Instantiate(consumeItemGameObject, returnMaterialPoint.position, returnMaterialPoint.rotation);
                //returnObject.GetComponent<Rigidbody>().velocity = 5f * returnMaterialPoint.transform.forward;



                //foreach (GameObject go in consumeItemGameObjectList)
                //    Debug.Log("(restart)consumeItemGameObjectList: " + go);
                //foreach (Item5 item in inputItemList)
                //    Debug.Log("(restart)inputItemList: " + item);
            }
            consumeItemGameObjectList = null;

            //inputItemList = null;
            inputItemList = new List<Item5>(craftingRecipeSO.inputItemSOList);
        }
    }

    public void Craft()
    {
        Debug.Log("Craft");
        //Collider[] colliderArray = Physics.OverlapBox(
        //    transform.position + placeItemsAreaBoxCollider.center,
        //    placeItemsAreaBoxCollider.size,
        //    placeItemsAreaBoxCollider.transform.rotation);


        //inputItemList = new List<Item5>(craftingRecipeSO.inputItemSOList);
        //consumeItemGameObjectList = new List<GameObject>();

        //foreach (Collider collider in colliderArray)
        //{
        //    if (collider.TryGetComponent(out WorldItem worldItem))
        //    {
        //        if (inputItemList.Contains(worldItem.item))
        //        {
        //            Debug.Log(collider.gameObject);

        //            inputItemList.Remove(worldItem.item);
        //            consumeItemGameObjectList.Add(collider.gameObject);
        //            //Destroy(collider.gameObject);
        //            //DestroyObject(collider.gameObject);
        //        }
        //    }
        //}

        //foreach (GameObject go in consumeItemGameObjectList)
        //    Debug.Log("2consumeItemGameObjectList: " + go);
        //foreach (Item5 item in inputItemList)
        //    Debug.Log("2inputItemList: " + item);

        if (inputItemList != null && inputItemList.Count == 0)        //Have all the required item to craft
        {
            Debug.Log("Yes");
            //Transform spawnedItemTransform = 
            Instantiate(craftingRecipeSO.outputItemSO, itemSpawnPoint.position, itemSpawnPoint.rotation);

            //foreach(GameObject consumeItemGameObject in consumeItemGameObjectList)
            //{
            //    Destroy(consumeItemGameObject);
            //}
            //Debug.Log("displayList");
            foreach (Transform displayItem in displayList)
            {
                displayItem.transform.GetChild(1).GetComponent<Text>().text = 0.ToString();
            }



            //Debug.Log("displayList");
            foreach (Transform previewDisplay in previewDisplayList)
            {
                previewDisplay.transform.GetChild(1).GetComponent<Text>().text = 0.ToString();
            }



            inputItemList = null;
            consumeItemGameObjectList = null;
        }
        else
        {
            Debug.Log("No");

        }
    }

    //public void DestroyObject(GameObject gameObject)
    //{
    //    Debug.Log("Destroy: " + gameObject);
    //    Destroy(gameObject);
    //}

    public void ConsumeMaterial(/*WorldItem worldItem*/)
    {
        if (inputItemList == null)
        {
            //Debug.Log("new inputItemList");
            inputItemList = new List<Item5>(craftingRecipeSO.inputItemSOList);

        }

        if (/*consumeItemGameObjectList.Count == 0 ||*/ consumeItemGameObjectList == null)
        {
            //Debug.Log("new consumeItemGameObjectList");

            consumeItemGameObjectList = new List<GameObject>();

        }

        Collider[] colliderArray = Physics.OverlapBox(
            transform.position + placeItemsAreaBoxCollider.center,
            placeItemsAreaBoxCollider.size,
            placeItemsAreaBoxCollider.transform.rotation);

        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out WorldItem worldItem))
            {
                if (inputItemList.Contains(worldItem.item))
                {
                    inputItemList.Remove(worldItem.item);
                    consumeItemGameObjectList.Add(collider.gameObject.GetComponent<WorldItem>().item.objectPrefab);
                    DisplayInputMaterial(worldItem.item);
                    //consumeItem.canDestroy = true;
                    Destroy(collider.gameObject);
                    //Debug.Log(collider.gameObject);

                }
            }
        }

        //if (inputItemList.Contains(worldItem.item))
        //{
        //    inputItemList.Remove(worldItem.item);
        //    consumeItemGameObjectList.Add(worldItem.item.objectPrefab);
        //    consumeItem.canDestroy = true;
        //}
    }

    public void DisplayRecipe_Material()
    {
        //foreach (Item5 inputItem in craftingRecipeSO.inputItemSOList)
        //{
        //    Transform consumeItemSlot = null;
        //    if (consumeItemSlot == null)
        //    {

        //    }


        //    consumeItemSlot.GetComponent<Image>().sprite = inputItem.image;
        //    consumeItemSlot.GetComponentInChildren<Text>().text = 0 + " / " + inputItem;

        //    Item5 previousItem = inputItem;
        //    if (previousItem.objectPrefab == inputItem.objectPrefab)
        //    {

        //    }

        //    consumeItemSlot = Instantiate(craftingUI.transform.GetChild(1).GetChild(0), craftingUI.transform.GetChild(1));
        //    consumeItemSlot.gameObject.SetActive(true);

        //}



        //int inputItemCount = 0;
        //Item5 previousItem;
        //Transform consumeItemSlot = null;

        //foreach (Item5 inputItem in craftingRecipeSO.inputItemSOList)
        //{
        //    previousItem = inputItem;
        //    if (previousItem.objectPrefab == inputItem.objectPrefab)
        //    {
        //        inputItemCount++;
        //        consumeItemSlot.GetComponentInChildren<Text>().text = 0 + " / " + inputItemCount;
        //        return;
        //    }
        //}

        //foreach (Item5 inputItem in craftingRecipeSO.inputItemSOList)
        //{
        //    previousItem = inputItem;
        //    //Transform consumeItemSlot = null;
        //    if (consumeItemSlot == null)
        //    {
        //        consumeItemSlot = Instantiate(craftingUI.transform.GetChild(1).GetChild(0), craftingUI.transform.GetChild(1));
        //        consumeItemSlot.gameObject.SetActive(true);

        //        consumeItemSlot.GetComponent<Image>().sprite = inputItem.image;
        //        inputItemCount++;
        //        consumeItemSlot.GetComponentInChildren<Text>().text = 0 + " / " + inputItemCount;

        //    }
        //}



        bool itemAlreadyInInventory = false;
        int inputItemCount = 0;
        Transform consumeItemSlot = null;

        //Item5 currentInputItem = null;
        Item5 previousInputItem = null;
        //foreach (Item5 inputItem in craftingRecipeSO.inputItemSOList)
        for (int i = 0; i < craftingRecipeSO.inputItemSOList.Count; i++)
        {
            //Debug.Log("i: " + i);
            Item5 currentInputItem = craftingRecipeSO.inputItemSOList[i];
            //for (int j = -1; i < craftingRecipeSO.inputItemSOList.Count; j++)
            {
                //Debug.Log("itemAlreadyInInventory");

                //if (j >= 0)
                if(i - 1 >=0)
                {
                    //Debug.Log("j: " + j);

                    previousInputItem = craftingRecipeSO.inputItemSOList[i-1];
                }

                if (previousInputItem != null && (previousInputItem.objectPrefab == currentInputItem.objectPrefab))
                {
                    //Debug.Log("add itemAlreadyInInventory");

                    inputItemCount++;
                    consumeItemSlot.GetComponentInChildren<Text>().text =  "  / " + inputItemCount;
                    itemAlreadyInInventory = true;
                }
            }

            if (!itemAlreadyInInventory)
            {
                //Debug.Log("not itemAlreadyInInventory");

                consumeItemSlot = Instantiate(craftingUI.transform.GetChild(1).GetChild(0), craftingUI.transform.GetChild(1));
                consumeItemSlot.gameObject.SetActive(true);

                inputItemCount = 0;
                inputItemCount++;
                consumeItemSlot.GetComponentInChildren<Text>().text =  "  / " + inputItemCount;
                consumeItemSlot.GetComponent<Image>().sprite = currentInputItem.image;
                displayList.Add(consumeItemSlot);
                //Debug.Log(item.itemType);
            }
        }


        foreach(Transform displayItem in displayList)
        {
            Transform consumeItemSlotInPreview = Instantiate(displayItem, previewCraftingUI.transform.GetChild(1));
            consumeItemSlotInPreview.gameObject.SetActive(true);
            previewDisplayList.Add(consumeItemSlotInPreview);

        }
    }

    public void DisplayInputMaterial(Item5 InputItem)
    {
        //Debug.Log("DisplayInputMaterial");

        foreach(Transform displayItem in displayList)
        {
            if (InputItem.image == displayItem.GetComponent<Image>().sprite)
            {
                int inputItemCount;
                int.TryParse(displayItem.transform.GetChild(1).GetComponent<Text>().text, out inputItemCount);

                inputItemCount++;
                displayItem.transform.GetChild(1).GetComponent<Text>().text = inputItemCount.ToString();
            }
        }

        foreach (Transform previewDisplay in previewDisplayList)
        {
            if (InputItem.image == previewDisplay.GetComponent<Image>().sprite)
            {
                int inputItemCount;
                int.TryParse(previewDisplay.transform.GetChild(1).GetComponent<Text>().text, out inputItemCount);

                inputItemCount++;
                previewDisplay.transform.GetChild(1).GetComponent<Text>().text = inputItemCount.ToString();
            }
        }
    }

    public void UpdateDisplayList()
    {
        if (displayList != null)
        {
            //Debug.Log("displayList");
            foreach (Transform displayItem in displayList)
            {
                Destroy(displayItem.gameObject);
            }
            displayList = new List<Transform>();
        }

        if (previewDisplayList != null)
        {
            //Debug.Log("displayList");
            foreach (Transform previewDisplay in previewDisplayList)
            {
                Destroy(previewDisplay.gameObject);
            }
            previewDisplayList = new List<Transform>();
        }
    }
}
