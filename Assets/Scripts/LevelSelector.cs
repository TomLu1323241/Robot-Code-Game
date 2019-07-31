using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject levelPrefab;

    void Start()
    {
        float widthSep = Camera.main.orthographicSize * Camera.main.aspect * 2 / 3;
        float heightSep = 4f;
        int levels = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 2;
        for (int i = 0; i < Mathf.CeilToInt(levels / 3f); i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject levelInstance = GameObject.Instantiate(levelPrefab);
                levelInstance.transform.position = (new Vector2(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize)) + (new Vector2(widthSep / 2 + widthSep * j, -heightSep / 2 - heightSep * i));
                levelInstance.GetComponentInChildren<TextMeshPro>().text = "" + (i * 3 + j + 1);
                levelInstance.transform.SetParent(this.gameObject.transform);
                if (i * 3 + j + 1 == levels)
                {
                    break;
                }
            }
        }
    }
}
