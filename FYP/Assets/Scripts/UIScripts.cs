using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScripts : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject settingUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Start UI")
        {
            //PauseUI.SetActive(!PauseUI.activeInHierarchy);
            OpenUI(pauseUI);
        }

        if (pauseUI.activeInHierarchy || settingUI.activeInHierarchy || SceneManager.GetActiveScene().name == "Start UI")
        {
            Time.timeScale = 0;     //pause the game time
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Time.timeScale = 1;     //start the game time
            Cursor.lockState = CursorLockMode.Locked;
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
