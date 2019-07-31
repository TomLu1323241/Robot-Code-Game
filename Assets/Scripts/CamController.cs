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
    bool freeMoving = false;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        this.transform.position = player.transform.position + Vector3.back * 10 + Vector3.down * camRealativeHeight;
    }

    // Update is called once per frame
    void Update()
    {
        changeFreeMove();// Allows double tap to change camera modes
        if (freeMove)
        {
            // Allows the player to pan around the level
            if (Input.GetMouseButtonDown(0) && inPlayArea())
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                freeMoving = true;
            }
            if (Input.GetMouseButton(0) && freeMoving)
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                this.transform.position += direction;
            }
            if (Input.GetMouseButtonUp(0))
            {
                freeMoving = false;
            }
        } else
        {
            // Camera is stuck to player here
            if (Vector3.Magnitude(player.transform.position + Vector3.back * 10 + Vector3.down * camRealativeHeight - this.transform.position) > 1)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position + Vector3.back * 10 + Vector3.down * camRealativeHeight, 0.3f);            }
            else
            {
                this.transform.position = player.transform.position + Vector3.back * 10 + Vector3.down * camRealativeHeight;
            }
            
        }
    }

    void changeFreeMove()
    {
        if (Input.GetMouseButtonDown(0) && inPlayArea())// Checks for double tap
        {
            if (Time.realtimeSinceStartup - lastTap < 0.2)
            {
                freeMove = !freeMove;
            }
            lastTap = Time.realtimeSinceStartup;
        }
    }

    bool inPlayArea()// Checks if the taps happens in the play area
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

    private void OnDrawGizmos()// Draws the play area in green and draws a red box there the player will be positioned
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position + Vector3.up * camRealativeHeight, Vector3.one);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position + Vector3.up * Camera.main.orthographicSize / 3 * 2, new Vector3(Camera.main.orthographicSize * Camera.main.aspect * 2, Camera.main.orthographicSize / 3 * 2));
    }
}
