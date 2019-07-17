using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] bool horizontal = true;
    [SerializeField] float size = 5;
    [SerializeField] float speed = 5;

    Vector3 origin;
    Vector3 target;

    bool leftUp = true;
    PhysicsMaterial2D temp;

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
        origin = this.transform.position;

        float width = this.GetComponent<BoxCollider2D>().size.x;
        float height = this.GetComponent<BoxCollider2D>().size.y;

        if (horizontal)
        {
            target = this.transform.position + Vector3.right * size + Vector3.left * width;
        } else
        {
            target = this.transform.position + Vector3.up * size + Vector3.down * height;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontal)
        {
            if (leftUp)
            {
                this.transform.position = this.transform.position + Vector3.right * speed * Time.deltaTime;
            } else
            {
                this.transform.position = this.transform.position + Vector3.left * speed * Time.deltaTime;
            }
        } else
        {
            if (leftUp)
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

    private void ChangeDirection()
    {
        if (horizontal)
        {
            if (leftUp)
            {
                if (this.transform.position.x >= target.x)
                {
                    leftUp = !leftUp;
                }
            } else
            {
                if (this.transform.position.x <= origin.x)
                {
                    leftUp = !leftUp;
                }
            }

        } else
        {
            if (leftUp)
            {
                if (this.transform.position.y >= target.y)
                {
                    leftUp = !leftUp;
                }
            }
            else
            {
                if (this.transform.position.y <= origin.y)
                {
                    leftUp = !leftUp;
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
            Gizmos.DrawLine(this.transform.position + Vector3.left * halfWidth, this.transform.position + Vector3.right * size + Vector3.left * halfWidth);
        } else
        {
            Gizmos.DrawLine(this.transform.position + Vector3.down * halfHeight, this.transform.position + Vector3.up * size + Vector3.down * halfHeight);
        }
    }
}
