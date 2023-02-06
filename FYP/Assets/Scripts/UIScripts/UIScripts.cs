using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScripts : MonoBehaviour
{
    public static UIScripts instance;

    public GameObject pauseUI;
    public GameObject settingUI;
    public GameObject shopUI;
    public GameObject backPackUI;
    public GameObject hotBarUI;
    public GameObject buttonIndicationUI;
    public GameObject craftingUI;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //buttonIndicationUI.SetActive(false);
    }

    private void Update()
    {

        if (!pauseUI.activeInHierarchy && !settingUI.activeInHierarchy)
        {

            if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Start UI")
            {
                //if (shopUI.activeInHierarchy || backPackUI.activeInHierarchy || craftingUI.activeInHierarchy)
                {
                    //Debug.Log("5");

                    //CloseUI(shopUI);
                    //CloseUI(backPackUI);
                    //CloseUI(craftingUI);

                    //PauseUI.SetActive(!PauseUI.activeInHierarchy);

                }
                //else 
                {
                    //Debug.Log("2");
                    OpenUI(pauseUI); 
                }
            }
           
        }
        else if (pauseUI.activeInHierarchy && !settingUI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Debug.Log("6");

                CloseUI(pauseUI);
            }
        }
        else if (!pauseUI.activeInHierarchy && settingUI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Debug.Log("7");

                CloseUI(settingUI);
            }
        }

        if (pauseUI.activeInHierarchy || settingUI.activeInHierarchy || SceneManager.GetActiveScene().name == "Start UI")
        {
            //Debug.Log("8");

            Time.timeScale = 0;     //pause the game time
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if((shopUI && shopUI.activeInHierarchy) || (backPackUI && backPackUI.activeInHierarchy) || (craftingUI && craftingUI.activeInHierarchy))
        {
            //Debug.Log("10");

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else                                             //if no UI
        {
            //Debug.Log("9");

            Time.timeScale = 1;     //start the game time
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        
        //else if ()
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //}

        //float interactDistance = 3f;
        //if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, interactDistance))
        //{
        //    if (raycastHit.transform.TryGetComponent(out CraftingTable craftingTable) /*|| (raycastHit.transform.GetComponent<CraftingTable>() && raycastHit.transform.GetComponent<CraftingTable>().recipeImage.transform.parent)*/)
        //    {

        //    }
        //}
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenUI(GameObject nextUI)
    {
        nextUI.SetActive(true);
    }

    public void CloseUI(GameObject thisUI)
    {
        thisUI.SetActive(false);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //public void InteractiveLicense()
    //{
    //    Cursor.lockState = CursorLockMode.Confined;
    //}
}
