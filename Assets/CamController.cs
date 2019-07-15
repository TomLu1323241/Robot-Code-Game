using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] float camRealativeHeight = 5;

    PlayerController player;

    bool freeMove = false;
    float lastTap = -1000;
    Vector3 touchStart;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        changeFreeMove();
        if (freeMove)
        {
            if (Input.GetMouseButtonDown(0) && inPlayArea())
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(0) && inPlayArea())
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                this.transform.position += direction;
            }
        } else
        {
            this.transform.position = player.transform.position + Vector3.back * 10 + Vector3.down * camRealativeHeight;
        }
    }

    void changeFreeMove()
    {
        if (Input.GetMouseButtonDown(0) && inPlayArea())
        {
            if (Time.time - lastTap < 0.2)
            {
                freeMove = !freeMove;
            }
            lastTap = Time.time;
        }
    }

    bool inPlayArea()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x > this.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect &&
            mousePos.x < this.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect && 
            mousePos.y > this.transform.position.y + Camera.main.orthographicSize / 3 &&
            mousePos.y < this.transform.position.y + Camera.main.orthographicSize)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position + Vector3.up * camRealativeHeight, Vector3.one);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position + Vector3.up * Camera.main.orthographicSize / 3 * 2, new Vector3(Camera.main.orthographicSize * Camera.main.aspect * 2, Camera.main.orthographicSize / 3 * 2));
    }
}
