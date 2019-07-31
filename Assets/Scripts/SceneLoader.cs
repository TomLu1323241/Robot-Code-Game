using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

    private static AsyncOperation async;

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void PreLoadNextSceneAsync()
    {
        async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        async.allowSceneActivation = false;
    }

    public static void LoadNextSceneAsync()
    {
        async.allowSceneActivation = true;
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void LoadLevelSelector()
    {
        SceneManager.LoadScene(1);
    }

    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index + 1);
    }
}
