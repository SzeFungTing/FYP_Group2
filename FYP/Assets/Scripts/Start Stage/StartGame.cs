using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public TableControl tc;
    // Start is called before the first frame update
    void Start()
    {
        tc = FindObjectOfType<TableControl>();
    }
    public void exitgame()
    {
        Application.Quit();
    }

    public void StartStage()
    {
        //if (tc.TryLoadPlayer())
        //{
        //    string map = tc.GetPlayerMap();
        //    Debug.Log(map);
        //    if (map != "")
        //    {
        //        SceneManager.LoadScene(map);
        //    }
        //}
        //else
        //{
        //    SceneManager.LoadScene("HomeScene");
        //}
        SceneManager.LoadScene("HomeScene");
    }

}
