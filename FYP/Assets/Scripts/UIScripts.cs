using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScripts : MonoBehaviour
{
    public GameObject PauseUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUI.SetActive(!PauseUI.activeInHierarchy);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
