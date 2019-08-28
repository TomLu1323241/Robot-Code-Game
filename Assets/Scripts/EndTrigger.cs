using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{

    [SerializeField] GameObject starParent = null;
    [Header("Timer")]
    [SerializeField] float time3Star = 5;
    [SerializeField] float time2Star = 10;

    float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    public void StartTime()
    {
        startTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D col)// Loades next level
    {
        if (col.name.Equals("Player"))
        {
            Time.timeScale = 0;
            this.transform.GetChild(0).gameObject.SetActive(true);
            if (Time.time - startTime > time3Star && Time.time - startTime < time2Star)
            {
                starParent.transform.GetChild(2).gameObject.SetActive(false);
            } else if (Time.time - startTime > time2Star)
            {
                starParent.transform.GetChild(2).gameObject.SetActive(false);
                starParent.transform.GetChild(1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("levelUnlock") < SceneLoader.GetLevel())
            {
                PlayerPrefs.SetInt("levelUnlock", SceneLoader.GetLevel());
                PlayerPrefs.Save();
            }
        }
    }
}
