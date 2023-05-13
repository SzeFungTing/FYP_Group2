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
    void Start()
    {
        currentIndex = Random.Range(0, loadingSprites.Length);
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

        if(timer >= switchTime)
        {
            int nextIndex = Random.Range(0, loadingSprites.Length);

            if (currentIndex == nextIndex) {
                nextIndex = (currentIndex + 1) % loadingSprites.Length;
            }
            loadingImage.sprite = loadingSprites[nextIndex];
            timer = 0.0f;
            currentIndex = nextIndex;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == other.CompareTag("Player"))
            if (HomeScene)
            {
                StartCoroutine(LoadSceneAsynchronously(5));
            }
            else if (Maze)
            {
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
