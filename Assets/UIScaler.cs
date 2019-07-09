using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaler : MonoBehaviour
{
    [SerializeField] GameObject GameLoopBackground;
    [SerializeField] GameObject GameLoopText;
    [SerializeField] GameObject GameLoopWordBank;

    // Start is called before the first frame update
    void Start()
    {
        float camHeight = Camera.main.orthographicSize * 2;
        float camWidth = camHeight * Camera.main.aspect;
        GameLoopBackground.transform.localPosition = new Vector3(0, -camHeight / 12, 8);
        GameLoopBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(camWidth, camHeight / 2);

        GameLoopText.transform.localPosition = new Vector3(0, -camHeight / 12, 8);
        GameLoopText.GetComponent<RectTransform>().sizeDelta = new Vector2(camWidth, camHeight / 2);

        GameLoopWordBank.transform.localPosition = new Vector3(0, -camHeight / 12 * 5, 8);
        GameLoopWordBank.GetComponent<RectTransform>().sizeDelta = new Vector2(camWidth, camHeight / 6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
