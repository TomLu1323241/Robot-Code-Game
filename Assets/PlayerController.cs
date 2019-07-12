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
        body = this.GetComponent<Rigidbody2D>();

        OnGroundTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[0];
        LeftEdgeTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[1];
        RightEdgeTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[2];
        LeftWallTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[3];
        RightWallTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[4];
    }

    void Update()
    {
        Animation();
        if (Input.GetKeyDown(KeyCode.Space))// Debugging each line
        {
            for (int i = 0; i < cells.Length; i++)
            {
                Debug.Log(cells[i].action + " : " + cells[i].condition);
            }
        }

        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].isIfStatement)
            {
                bool ifRunning = false;
                switch (cells[i].condition)
                {
                    case Conditions.Empty:
                        ifRunning = false;
                        break;
                    case Conditions.OnGround:
                        ifRunning = OnGround();
                        break;
                    case Conditions.HitWall:
                        ifRunning = HitWall();
                        break;
                    case Conditions.OnEdge:
                        ifRunning = OnEdge();
                        break;
                    case Conditions.InMidAir:
                        break;
                    case Conditions.OnLadder:
                        break;
                    default:
                        break;
                }
                if (ifRunning)
                {
                    for (int j = 1; j < cells[i].linesOfCommands + 1; j++)
                    {
                        switch (cells[i + j].action)
                        {
                            case Actions.Empty:
                                break;
                            case Actions.Walk:
                                Walk();
                                break;
                            case Actions.Jump:
                                Jump();
                                break;
                            case Actions.Climb:
                                break;
                            case Actions.Turn:
                                Turn();
                                break;
                            default:
                                break;
                        }
                    }
                }
                i += cells[i].linesOfCommands + 1;
            }
            switch (cells[i].action)
            {
                case Actions.Empty:
                    break;
                case Actions.Walk:
                    Walk();
                    break;
                case Actions.Jump:
                    Jump();
                    break;
                case Actions.Climb:
                    break;
                case Actions.Turn:
                    Turn();
                    break;
                default:
                    break;
            }
        }

        //Walk();
        //if (OnEdge())
        //{
        //    Jump();
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
        }
        else
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

    void Walk()
    {
        if (facingLeft)
        {
            body.velocity = new Vector2(-speed, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(speed, body.velocity.y);
        }
    }

    void Jump()
    {
        if (this.GetComponent<Collider2D>().IsTouching(ground) && Time.time - lastJump > 0.1)
        {
            body.velocity = body.velocity + Vector2.up * jumpHeight;
            lastJump = Time.time;
        }
    }

    void Turn()
    {
        this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
        facingLeft = !facingLeft;
    }

    void Animation()
    {
        if (Mathf.Abs(body.velocity.x) > 0.01f)
        {
            this.GetComponent<Animator>().SetBool("Walking", true);
        }
        else
        {
            this.GetComponent<Animator>().SetBool("Walking", false);
        }
    }

}
