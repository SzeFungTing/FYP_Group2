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
    public Sprite[] loadingSprites;
    public Image loadingImage;
    public float switchTime = 1.0f;
    private float timer = 0.0f;
    private int currentIndex = 0;
    public TableControl tc;
    void Start()
    {
        currentIndex = Random.Range(0, loadingSprites.Length);
        if (loadingSprites.Length > 0)
            loadingImage.sprite = loadingSprites[currentIndex];

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

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchTime)
        {
            int nextIndex = Random.Range(0, loadingSprites.Length);

            if (currentIndex == nextIndex)
            {
                if (loadingSprites.Length > 0)
                    nextIndex = (currentIndex + 1) % loadingSprites.Length;
            }
            if (loadingSprites.Length > 0)
                loadingImage.sprite = loadingSprites[nextIndex];
            timer = 0.0f;
            currentIndex = nextIndex;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (HomeScene && transform.name.Contains("Door"))
            {
                StartCoroutine(LoadSceneAsynchronously(5));
            }
            else if (Maze)
            {
                StartCoroutine(LoadSceneAsynchronously(0));
            }

            if (transform.gameObject.transform.parent.name.Contains("Portal_DarkForest") )
            {
                StartCoroutine(LoadSceneAsynchronously(1));
            }
            else if (transform.gameObject.transform.parent.name.Contains("Portal_Desert"))
            {
                StartCoroutine(LoadSceneAsynchronously(2));
            }
            else if (transform.gameObject.transform.parent.name.Contains("Portal_ice"))
            {
                StartCoroutine(LoadSceneAsynchronously(3));
            }
            else if (transform.gameObject.transform.parent.name.Contains("Portal_Volcano"))
            {
                StartCoroutine(LoadSceneAsynchronously(4));
            }
            else if (transform.gameObject.transform.parent.name.Contains("Portal_Home"))
            {
                StartCoroutine(LoadSceneAsynchronously(0));
            }
        }
            
    }

    IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        //database
        if (tc)
        {
            tc.SavePlayerAndBackpack(GameObject.FindGameObjectWithTag("Player"));
            if (HomeScene)
            {
                tc.SaveBuildingAndAnimo();
            }
        }
      

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        loadingScreen.SetActive(true);

        //hide hotbar
        if (loadingScreen.activeInHierarchy)
        {
            UIScripts.instance.hotBarUI.SetActive(false);
            if(UIScripts.instance.jetpackUI.activeInHierarchy)
                UIScripts.instance.jetpackUI.SetActive(false);
        }

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
}
