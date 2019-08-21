using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject levelPrefab;

    private readonly string LEVELUNLOCK = "levelUnlock";

    void Start()
    {
        if (PlayerPrefs.GetInt(LEVELUNLOCK, -1) == -1)
        {
            PlayerPrefs.SetInt(LEVELUNLOCK, 1);
            PlayerPrefs.Save();
        }
        int levelsUnlocked = PlayerPrefs.GetInt(LEVELUNLOCK);
        float widthSep = Camera.main.orthographicSize * Camera.main.aspect * 2 / 3;
        float heightSep = 4f;
        int totalLevels = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 2;
        for (int i = 0; i < Mathf.CeilToInt(totalLevels / 3f); i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject levelInstance = GameObject.Instantiate(levelPrefab);
                levelInstance.transform.position = (new Vector2(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize)) + (new Vector2(widthSep / 2 + widthSep * j, -heightSep / 2 - heightSep * i));
                if (i * 3 + j < levelsUnlocked)
                {
                    levelInstance.GetComponentInChildren<TextMeshPro>().text = "" + (i * 3 + j + 1);
                } else
                {
                    levelInstance.GetComponentInChildren<TextMeshPro>().text = "X";
                }
                levelInstance.transform.SetParent(this.gameObject.transform);
                if (i * 3 + j + 1 == totalLevels)
                {
                    break;
                }
            }
        }
    }
}
