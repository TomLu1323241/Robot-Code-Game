using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScroller : MonoBehaviour
{
    Vector3 touchStart;

    int height;
    // Start is called before the first frame update
    void Start()
    {
        height = Mathf.CeilToInt((UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 2) / 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position += Vector3.up * direction.y;
        }

        if (this.transform.position.y > 0)
        {
            this.transform.position = Vector3.zero + Vector3.back * 10;
        }

        if (this.transform.position.y < Camera.main.orthographicSize * 2 - height * 4)
        {
            this.transform.position = Vector3.up * Camera.main.orthographicSize * 2 + Vector3.down * height * 4 + Vector3.back * 10;
        }
    }

    private void OnDrawGizmos()
    {
        int height = Mathf.CeilToInt((UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 2) / 3f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(Vector3.down * height * 4 / 2 + Vector3.up * Camera.main.orthographicSize, Vector3.left * Camera.main.orthographicSize * Camera.main.aspect * 2 + Vector3.up * height * 4);
    }
}
