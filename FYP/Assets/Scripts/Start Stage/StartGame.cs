using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 
        
    }
    public void exitgame()
    {
        Application.Quit();
    }

    public void StartStage()
    {
            SceneManager.LoadScene("HomeScene");
        
    }

}
