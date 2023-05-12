using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swap : MonoBehaviour
{
    bool HomeScene = false;
    bool Maze = false;
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log(currentSceneName);
        if (currentSceneName == "HomeScene")
        {
            HomeScene = true;
        }
        else if (currentSceneName == "Puzzle1")
        {
            Maze = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == other.CompareTag("Player"))
            if (HomeScene)
            {
                SceneManager.LoadScene("Puzzle1");
            }
            else if (Maze)
            {
                SceneManager.LoadScene("HomeScene");
            }
    }
}
