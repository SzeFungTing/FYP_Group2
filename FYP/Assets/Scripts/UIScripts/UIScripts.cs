using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class UIScripts : MonoBehaviour
{
    public static UIScripts instance;

    public GameObject pauseUI;
    public GameObject settingUI;
    public GameObject shopUI;
    public GameObject backPackUI;
    public GameObject hotBarUI;
    public GameObject craftingUI;

    public GameObject craftingHint;

    public GunVacuum gunVacuum;
    public GunShooting gunShooting;

    public GameObject jetpackUI;
    public GameObject loadingUI;

    public bool isTimeStop = false;     //to pause playerMovement, shooting, inhale function
    public bool isOpenUI = false;

    public Dropdown dropDown;

    public TableControl tc;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        craftingHint.SetActive(false);
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
                    //OpenUI(pauseUI); 
                }

                if(!((shopUI && shopUI.activeInHierarchy) || (backPackUI && backPackUI.activeInHierarchy) || (craftingUI && craftingUI.activeInHierarchy)))
                {
                    OpenUI(pauseUI);

                }
                else if((shopUI && shopUI.activeInHierarchy) || (backPackUI && backPackUI.activeInHierarchy) || (craftingUI && craftingUI.activeInHierarchy))
                {
                    if (shopUI.activeInHierarchy)
                    {
                        CloseUI(shopUI);
                    }
                    else if(craftingUI.activeInHierarchy)
                    {
                        CloseUI(craftingUI);
                    }
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
       

        if (pauseUI.activeInHierarchy || settingUI.activeInHierarchy || SceneManager.GetActiveScene().name == "Start UI")       //pause the game time, game, lock mouse
        {
            //Debug.Log("8");
            isTimeStop = true;
            isOpenUI = true;

            Time.timeScale = 0;     //pause the game time
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if((shopUI && shopUI.activeInHierarchy) || (backPackUI && backPackUI.activeInHierarchy) || (craftingUI && craftingUI.activeInHierarchy))       //pause game, lock mouse , not pause game time
        {
            //Debug.Log("10");
            //isTimeStop = true;
            isTimeStop = false;
            isOpenUI = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else                                             //if no UI
        {
            //Debug.Log("9");
            isTimeStop = false;
            isOpenUI = false;

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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        tc.SavePlayerAndBackpack(player);

        if (SceneManager.GetActiveScene().name == "HomeScene")
        {
            tc.SaveBuildingAndAnimo();
        }

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

    public void ChangeResolution()
    {
        switch (dropDown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true);
                break;

            case 1:
                Screen.SetResolution(960, 540, true);
                break;

            case 2:
                Screen.SetResolution(640, 360, true);
                break;

            default:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }

    //public void InteractiveLicense()
    //{
    //    Cursor.lockState = CursorLockMode.Confined;
    //}
}
