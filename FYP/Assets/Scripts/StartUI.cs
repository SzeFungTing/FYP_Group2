using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void HomeScene()
    {
        SceneManager.LoadScene("HomeScene");
    }


    public void exitgame()
    {
        Application.Quit();
    }

}
