using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed = 3;
    [SerializeField] float jumpHeight = 5.5f;
    [SerializeField] Collider2D ground = null;

    Cell[] cells;
    Rigidbody2D body = null;
    float lastJump = 0;
    bool facingLeft = false;

    Collider2D OnGroundTrigger;
    Collider2D LeftEdgeTrigger;
    Collider2D RightEdgeTrigger;
    Collider2D LeftWallTrigger;
    Collider2D RightWallTrigger;

    void Start()
    {
        cells = GameObject.FindObjectsOfType<Cell>();
        for (int j = 0; j < cells.Length - 1; j++)
        {
            for (int i = 0; i < cells.Length - 1; i++)
            {
                if (cells[i].transform.position.y < cells[i + 1].transform.position.y)
                {
                    Cell cell = cells[i + 1];
                    cells[i + 1] = cells[i];
                    cells[i] = cell;
                }
            }
        }
        SetUpIfStatements();
        body = this.GetComponent<Rigidbody2D>();

        OnGroundTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[0];
        LeftEdgeTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[1];
        RightEdgeTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[2];
        LeftWallTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[3];
        RightWallTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[4];
    }

    private void SetUpIfStatements()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))// Debugging each line
        {
            for (int i = 0; i < cells.Length; i++)
            {
                Debug.Log(cells[i].action);
            }
        }
        foreach(Cell cell in cells)
        {
            switch(cell.action)
            {
                case Actions.Empty:
                    break;
                case Actions.Walk:
                    if (facingLeft)
                    {
                        body.velocity = new Vector2(-speed, body.velocity.y);
                    } else
                    {
                        body.velocity = new Vector2(speed, body.velocity.y);
                    }
                    break;
                case Actions.Jump:
                    if (this.GetComponent<Collider2D>().IsTouching(ground) && Time.time - lastJump > 0.1)
                    {
                        body.velocity = body.velocity + Vector2.up * jumpHeight;
                        lastJump = Time.time;
                    }
                    break;
                case Actions.Climb:
                    break;
                case Actions.Turn:
                    this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
                    facingLeft = !facingLeft;
                    break;
                default:
                    break;
            }
        }
        Animation();
        body.velocity = new Vector2(speed, body.velocity.y);
        if (OnEdge() && Time.time - lastJump > 0.1)
        {
            body.velocity = body.velocity + Vector2.up * jumpHeight;
            lastJump = Time.time;
        }



        //if (facingLeft)
        //{
        //    body.velocity = new Vector2(-speed, body.velocity.y);
        //}
        //else
        //{
        //    body.velocity = new Vector2(speed, body.velocity.y);
        //}
        //if (HitWall())
        //{
        //    this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
        //    facingLeft = !facingLeft;
        //}
    }
    
    bool OnGround()
    {
        if (OnGroundTrigger.IsTouching(ground))
        {
            return true;
        }
        return false;
    }

    bool OnEdge()
    {
        if (facingLeft)
        {
            if (!LeftEdgeTrigger.IsTouching(ground) && OnGroundTrigger.IsTouching(ground) && RightEdgeTrigger.IsTouching(ground))
            {
                return true;
            }
        } else
        {
            if (!RightEdgeTrigger.IsTouching(ground) && OnGroundTrigger.IsTouching(ground) && LeftEdgeTrigger.IsTouching(ground))
            {
                return true;
            }
        }
        return false;
    }

    bool HitWall()
    {
        if (facingLeft)
        {
            if (LeftWallTrigger.IsTouching(ground))
            {
                return true;
            }
        }
        else
        {
            if (RightWallTrigger.IsTouching(ground))
            {
                return true;
            }
        }
        return false;
    }

    void Animation()
    {
        if (Mathf.Abs(body.velocity.x) > 0.01f)
        {
            this.GetComponent<Animator>().SetBool("Walking", true);
        } else
        {
            this.GetComponent<Animator>().SetBool("Walking", false);
        }
    }

}
