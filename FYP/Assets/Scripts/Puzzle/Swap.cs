using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Swap : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    bool HomeScene = false;
    bool Maze = false;
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
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
        Debug.Log("enter secen");
        Debug.Log(transform.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("is player enter secen");
            Debug.Log(other.name);
            if (HomeScene)
            {
                StartCoroutine(LoadSceneAsynchronously(5));
            }
            else if (Maze)
            {
                Debug.Log("enter Maze");
                StartCoroutine(LoadSceneAsynchronously(0));
            }

            if (other.gameObject.name == "Portal_DarkForest")
            {
                StartCoroutine(LoadSceneAsynchronously(1));
            }
            else if (other.gameObject.name == "Portal_Desert")
            {
                StartCoroutine(LoadSceneAsynchronously(2));
            }
            else if (other.gameObject.name == "Portal_ice")
            {
                StartCoroutine(LoadSceneAsynchronously(3));
            }
            else if (other.gameObject.name == "Portal_Volcano")
            {
                StartCoroutine(LoadSceneAsynchronously(4));
            }
            else if (other.gameObject.name == "Portal_Home")
            {
                StartCoroutine(LoadSceneAsynchronously(0));
            }
        }
           
    }

    IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
}
