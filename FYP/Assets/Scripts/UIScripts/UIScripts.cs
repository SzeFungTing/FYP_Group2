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

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        buttonIndicationUI.SetActive(false);
    }

    private void Update()
    {
        if (!pauseUI.activeInHierarchy && !settingUI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Start UI")
            {
                //PauseUI.SetActive(!PauseUI.activeInHierarchy);
                OpenUI(pauseUI);
            }
        }
        else if (pauseUI.activeInHierarchy && !settingUI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseUI(pauseUI);
            }
        }
        else if(!pauseUI.activeInHierarchy && settingUI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseUI(settingUI);
            }
        }

        if (pauseUI.activeInHierarchy || settingUI.activeInHierarchy || SceneManager.GetActiveScene().name == "Start UI")
        {
            Time.timeScale = 0;     //pause the game time
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;     //start the game time
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (shopUI.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (backPackUI.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
        }
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
}
