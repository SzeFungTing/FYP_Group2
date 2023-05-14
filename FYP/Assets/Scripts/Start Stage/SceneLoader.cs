using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    public void LoadScene(int levelIndex)
    {
        StartCoroutine(LoadSceneAsynchronously(levelIndex));
    }

    public void LoadSceneForStartStage()
    {
        TableControl tc = FindObjectOfType<TableControl>();
        int map = tc.GetPlayerMap();
        StartCoroutine(LoadSceneAsynchronously(map));
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
