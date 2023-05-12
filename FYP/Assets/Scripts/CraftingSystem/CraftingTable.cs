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
    [SerializeField] private Transform returnMaterialPointByPlayer;

    private CraftingRecipeSO craftingRecipeSO;

    List<GameObject> consumeItemGameObjectList;
    List<Item5> inputItemList;
    List<Transform> displayList;
    List<Transform> previewDisplayList;

    GameObject craftingUI;
    GameObject previewCraftingUI;

    Animator animator;

    public bool isCrafting = false;
    bool waitCollider;

    public ParticleSystem craftingEffect;
    public ParticleSystem craftingEffect2;
    //ConsumeItem consumeItem;

    //Collider[] colliderArray;

    float emitTime = 0;
    float emitInterval = 0.5f;

    [SerializeField] AudioClip inhaleSound, emitSound;

    private void Awake()
    {
        craftingUI = UIScripts.instance.craftingUI;
        previewCraftingUI = transform.GetChild(/*0*/3).GetChild(0).gameObject;
        displayList = new List<Transform>();
        previewDisplayList = new List<Transform>();

        NextRecipe();
    }

    private void Start()
    {

        animator = transform.GetComponent<Animator>();
        //animator.SetBool("OpenInputMaterialDoor", true);
        //consumeItemGameObjectList = new List<GameObject>();
        //consumeItem = GetComponentInChildren<ConsumeItem>();

    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("OpenOutputItemDoor") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            ConsumeMaterial();


        if (isCrafting)
        {
            //Craft();
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("OpenInputMaterialDoor") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !waitCollider)
            {
                craftingEffect.Play(true);
                GameObject item = Instantiate(craftingRecipeSO.outputItemSO, itemSpawnPoint.position, itemSpawnPoint.rotation);
                AudioSource.PlayClipAtPoint(emitSound, itemSpawnPoint.position);

                item.GetComponent<Rigidbody>().velocity = 8f * -itemSpawnPoint.transform.forward;

                foreach (Transform displayItem in displayList)
                {
                    displayItem.transform.GetChild(1).GetComponent<Text>().text = 0.ToString();
                }


                foreach (Transform previewDisplay in previewDisplayList)
                {
                    previewDisplay.transform.GetChild(1).GetComponent<Text>().text = 0.ToString();
                }



                inputItemList = null;
                consumeItemGameObjectList = null;
                

                animator.SetBool("isOpenOutputDoor", false);
             
                emitTime = Time.time;
                waitCollider = true;



            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("OpenOutputItemDoor") && waitCollider && Time.time > emitTime + emitInterval)
            {
                transform.GetChild(0).GetComponent<Collider>().enabled = true;
                isCrafting = false;
                waitCollider = false;
                craftingEffect.Stop(true);
            }

        }



        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    foreach (GameObject go in consumeItemGameObjectList)
        //        Debug.Log("consumeItemGameObjectList: " + go);
        //    foreach (Item5 item in inputItemList)
        //        Debug.Log("inputItemList: " + item);
        //}
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
        

        if (inputItemList != null && inputItemList.Count == 0)        //Have all the required item to craft
        {
            Debug.Log("Yes");

            animator.SetBool("isOpenOutputDoor", true);
            //transform.GetComponent<Collider>().enabled = false;
            transform.GetChild(0).GetComponent<Collider>().enabled = false;
            isCrafting = true;
            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("OpenOutputItemDoor") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            //{
            //    GameObject item = Instantiate(craftingRecipeSO.outputItemSO, itemSpawnPoint.position, itemSpawnPoint.rotation);
            //    item.GetComponent<Rigidbody>().velocity = 8f * itemSpawnPoint.transform.forward;

            //    foreach (Transform displayItem in displayList)
            //    {
            //        displayItem.transform.GetChild(1).GetComponent<Text>().text = 0.ToString();
            //    }


            //    foreach (Transform previewDisplay in previewDisplayList)
            //    {
            //        previewDisplay.transform.GetChild(1).GetComponent<Text>().text = 0.ToString();
            //    }

            //    isCrafting = false;
            //}



            
        }
        else
        {
            Debug.Log("No");

        }
    }



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

                    AudioSource.PlayClipAtPoint(inhaleSound, transform.position);
                }
            }
        }
    }

    public void DisplayRecipe_Material()            //set and display the needed Material
    {


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

    public void DisplayInputMaterial(Item5 InputItem)               //if the input item is same at the list, the number of the material in the UI will add
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

    public void RemoveInputMaterial()               //if the input item is same at the list, the number of the material in the UI will remove
    {
        //Debug.Log("call RemoveInputMaterial");
        if (consumeItemGameObjectList.Count > 0)
        {
            //Debug.Log("call consumeItemGameObjectList.Count > 0");
            //Debug.Log("consumeItemGameObjectList[consumeItemGameObjectList.Count - 1]: " + consumeItemGameObjectList[consumeItemGameObjectList.Count - 1]);
            Item5 removeItem = consumeItemGameObjectList[consumeItemGameObjectList.Count - 1].GetComponent<WorldItem>().item;

            foreach (Transform displayItem in displayList)
            {
                if (removeItem.image == displayItem.GetComponent<Image>().sprite)
                {
                    int inputItemCount;
                    int.TryParse(displayItem.transform.GetChild(1).GetComponent<Text>().text, out inputItemCount);

                    inputItemCount--;
                    displayItem.transform.GetChild(1).GetComponent<Text>().text = inputItemCount.ToString();
                }
            }

            foreach (Transform previewDisplay in previewDisplayList)
            {
                if (removeItem.image == previewDisplay.GetComponent<Image>().sprite)
                {
                    int inputItemCount;
                    int.TryParse(previewDisplay.transform.GetChild(1).GetComponent<Text>().text, out inputItemCount);

                    inputItemCount--;
                    previewDisplay.transform.GetChild(1).GetComponent<Text>().text = inputItemCount.ToString();
                }
            }

            GameObject item = Instantiate(consumeItemGameObjectList[consumeItemGameObjectList.Count - 1], returnMaterialPointByPlayer.position, returnMaterialPointByPlayer.rotation);
            item.GetComponent<WorldItem>().canConsume = false;
            consumeItemGameObjectList.Remove(consumeItemGameObjectList[consumeItemGameObjectList.Count - 1]);
            inputItemList.Add(removeItem);
        }

    }

    public void UpdateDisplayList()                 //delete the display preview
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
