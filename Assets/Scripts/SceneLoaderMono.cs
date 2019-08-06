using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderMono : MonoBehaviour
{
    public void ResetScene()
    {
        SceneLoader.ReloadScene();
    }

    public void LoadLevelSelector()
    {
        SceneLoader.LoadLevelSelector();
    }

    public void LoadMainMenu()
    {
        SceneLoader.LoadMenu();
    }

    public void LoadNextLevel()
    {
        SceneLoader.LoadNextScene();
    }
}
