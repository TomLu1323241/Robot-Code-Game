using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRequiresActivation : MonoBehaviour, EnvironmentAcions
{
    [SerializeField] bool horizontal = true;
    [SerializeField] bool startBotLeft = true;
    [SerializeField] float size = 5;
    [SerializeField] float speed = 5;

    Vector3 leftBotTarget;
    Vector3 rightTopTarget;

    bool movingTopRight = true;
    bool running = false;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.name.Equals("Player"))
        {
            col.transform.GetComponent<Collider2D>().sharedMaterial.friction = 1;
            col.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.name.Equals("Player"))
        {
            col.transform.GetComponent<Collider2D>().sharedMaterial.friction = 0;
            col.transform.SetParent(null);
        }
    }

    private void Start()
    {
        if (startBotLeft)
        {
            movingTopRight = true;
        }
        else
        {
            movingTopRight = false;
        }

        leftBotTarget = this.transform.position;

        float width = this.GetComponent<BoxCollider2D>().size.x;
        float height = this.GetComponent<BoxCollider2D>().size.y;

        if (horizontal)
        {
            if (startBotLeft)
            {
                rightTopTarget = this.transform.position + Vector3.right * size + Vector3.left * width;
            }
            else
            {
                rightTopTarget = this.transform.position;
                leftBotTarget = this.transform.position - Vector3.right * size;
            }
        }
        else
        {
            if (startBotLeft)
            {
                rightTopTarget = this.transform.position + Vector3.up * size + Vector3.down * height;
            }
            else
            {
                rightTopTarget = this.transform.position;
                leftBotTarget = this.transform.position - Vector3.up * size - Vector3.up * height / 2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            if (horizontal)
            {
                if (movingTopRight)
                {
                    this.transform.position = this.transform.position + Vector3.right * speed * Time.deltaTime;
                }
                else
                {
                    this.transform.position = this.transform.position + Vector3.left * speed * Time.deltaTime;
                }
            }
            else
            {
                if (movingTopRight)
                {
                    this.transform.position = this.transform.position + Vector3.up * speed * Time.deltaTime;
                }
                else
                {
                    this.transform.position = this.transform.position + Vector3.down * speed * Time.deltaTime;
                }
            }
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        if (horizontal)
        {
            if (movingTopRight)
            {
                if (this.transform.position.x >= rightTopTarget.x)
                {
                    movingTopRight = !movingTopRight;
                }
            }
            else
            {
                if (this.transform.position.x <= leftBotTarget.x)
                {
                    movingTopRight = !movingTopRight;
                }
            }
        }
        else
        {
            if (movingTopRight)
            {
                if (this.transform.position.y >= rightTopTarget.y)
                {
                    movingTopRight = !movingTopRight;
                }
            }
            else
            {
                if (this.transform.position.y <= leftBotTarget.y)
                {
                    movingTopRight = !movingTopRight;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        float halfWidth = this.GetComponent<BoxCollider2D>().size.x / 2;
        float halfHeight = this.GetComponent<BoxCollider2D>().size.y / 2;
        if (horizontal)
        {
            if (startBotLeft)
            {
                Gizmos.DrawLine(this.transform.position + Vector3.left * halfWidth, this.transform.position + Vector3.right * size + Vector3.left * halfWidth);
            }
            else
            {
                Gizmos.DrawLine(this.transform.position - Vector3.left * halfWidth, this.transform.position - Vector3.right * size + Vector3.left * halfWidth);
            }
        }
        else
        {
            if (startBotLeft)
            {
                Gizmos.DrawLine(this.transform.position - Vector3.down * halfHeight, this.transform.position + Vector3.up * size + Vector3.down * halfHeight);
            }
            else
            {
                Gizmos.DrawLine(this.transform.position + Vector3.down * halfHeight, this.transform.position - Vector3.up * size + Vector3.down * halfHeight);
            }
        }
    }

    public void Action()
    {
        running = true;
    }
}
