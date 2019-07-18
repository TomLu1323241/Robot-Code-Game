using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed = 3;
    [SerializeField] float jumpHeight = 5.5f;
    [SerializeField] float arrowRadius = 2f;
    [SerializeField] GameObject arrow = null;

    Cell[] cells;
    Rigidbody2D body = null;
    bool facingLeft = false;

    // Triggers to use for conditions
    Collider2D OnGroundTrigger;
    Collider2D LeftEdgeTrigger;
    Collider2D RightEdgeTrigger;
    Collider2D LeftWallTrigger;
    Collider2D RightWallTrigger;
    Collider2D LeftNearTrigger;
    Collider2D RightNearTrigger;

    // Layers
    readonly string GROUND = "Ground";
    readonly string JUMPABLE_OBSTACLES = "Jumpable Obstacles";
    readonly string CRATE = "Crate";

    // Find where the end goal is
    EndTrigger endTrigger;

    // Timers
    float lastJump = 0;
    float lastNearObstacles = 0;

    void Start()
    {
        // Find all the cells and convert them into an array
        cells = GameObject.FindObjectsOfType<Cell>();
        // Simple bubble sort to order then the way that it is ordered visually (top cell to bottom cell)
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

        // Initialize components
        body = this.GetComponent<Rigidbody2D>();

        OnGroundTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[0];
        LeftEdgeTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[1];
        RightEdgeTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[2];
        LeftWallTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[3];
        RightWallTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[4];
        LeftNearTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[5];
        RightNearTrigger = this.transform.GetComponentsInChildren<BoxCollider2D>()[6];

        endTrigger = GameObject.FindObjectOfType<EndTrigger>();
        arrow = GameObject.Instantiate(arrow);

        facingLeft = this.GetComponent<SpriteRenderer>().flipX;
    }

    void Update()
    {
        ArrowHandler();// Handles the arrow that points the user to the objective
        
        Animation();// Handles all the animation of the robot

        CodeHandler();// Translate the drag and drop code into working code

        if (Input.GetKeyDown(KeyCode.Space))// Debugging shows what each cell contains
        {
            for (int i = 0; i < cells.Length; i++)
            {
                Debug.Log(cells[i].action + " : " + cells[i].condition);
            }
        }
    }

    void CodeHandler()
    {
        for (int i = 0; i < cells.Length; i++)// Loops though every cell in order
        {
            if (cells[i].isIfStatement)// Check if current cell is if statement
            {
                bool ifRunning = false;
                switch (cells[i].condition)// Checks if the if statment is true
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
                    case Conditions.HitCrate:
                        ifRunning = HitCrate();
                        break;
                    case Conditions.OnEdge:
                        ifRunning = OnEdge();
                        break;
                    case Conditions.InMidAir:
                        ifRunning = InMidAir();
                        break;
                    case Conditions.NearBarrel:
                        ifRunning = NearBarrel();
                        break;
                    case Conditions.OnLadder:
                        break;
                    default:
                        break;
                }
                if (ifRunning)
                {
                    for (int j = 1; j < cells[i].linesOfCommands + 1; j++)// Runs the code in the if statment in a nested loop
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
                i += cells[i].linesOfCommands + 1;// Add the lines that were already ran in the loop above
            }
            switch (cells[i].action)// Normal action in the game loop
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

    private bool HitCrate()
    {
        if (facingLeft)// Depending on which way the robot is facing check if it hit a wall
        {
            if (LeftWallTrigger.IsTouchingLayers(LayerMask.GetMask(CRATE)))
            {
                return true;
            }
        }
        else
        {
            if (RightWallTrigger.IsTouchingLayers(LayerMask.GetMask(CRATE)))
            {
                return true;
            }
        }
        return false;
    }

    void ArrowHandler()
    {
        // Sets the posistion of the arrow so it is always a certain radius away form the player
        arrow.transform.position = Vector3.Normalize(endTrigger.transform.position - this.transform.position) * arrowRadius + this.transform.position;

        // Rotate the arrow so it points to the objective
        Vector2 direction = endTrigger.transform.position - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        arrow.transform.rotation = rotation;

        // Makes the arrow fade as you get closer to the objective
        if (Mathf.Abs(Vector3.Magnitude(endTrigger.transform.position - this.transform.position)) < 7)
        {
            if ((((Mathf.Abs(Vector3.Magnitude(endTrigger.transform.position - this.transform.position)) - 4) / 3) * 255) < 0)
            {
                arrow.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);// At a certain distance make the arrow disappear
                return;
            }
            arrow.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, (byte)(((Mathf.Abs(Vector3.Magnitude(endTrigger.transform.position - this.transform.position)) - 4) / 3) * 255));
        }
    }

    bool OnGround()
    {
        // Check if the ground collider is touching the ground
        if (OnGroundTrigger.IsTouchingLayers(LayerMask.GetMask(GROUND)))
        {
            return true;
        }
        return false;
    }

    bool OnEdge()
    {
        // Depending on which way the robot is facing check if it is on edge
        if (facingLeft)
        {
            if (!LeftEdgeTrigger.IsTouchingLayers(LayerMask.GetMask(GROUND)) && OnGroundTrigger.IsTouchingLayers(LayerMask.GetMask(GROUND)) && RightEdgeTrigger.IsTouchingLayers(LayerMask.GetMask(GROUND)))
            {
                return true;
            }
        }
        else
        {
            if (!RightEdgeTrigger.IsTouchingLayers(LayerMask.GetMask(GROUND)) && OnGroundTrigger.IsTouchingLayers(LayerMask.GetMask(GROUND)) && LeftEdgeTrigger.IsTouchingLayers(LayerMask.GetMask(GROUND)))
            {
                return true;
            }
        }
        return false;
    }

    bool InMidAir()
    {
        // Check if the 3 bottom colliders are not touching anything
        if (!LeftEdgeTrigger.IsTouchingLayers(LayerMask.GetMask(GROUND)) && !OnGroundTrigger.IsTouchingLayers(LayerMask.GetMask(GROUND)) && !RightEdgeTrigger.IsTouchingLayers(LayerMask.GetMask(GROUND)))
        {
            return true;
        }
        return false;
    }

    bool HitWall()
    {
        if (facingLeft)// Depending on which way the robot is facing check if it hit a wall
        {
            if (LeftWallTrigger.IsTouchingLayers(LayerMask.GetMask(GROUND)))
            {
                return true;
            }
        }
        else
        {
            if (RightWallTrigger.IsTouchingLayers(LayerMask.GetMask(GROUND)))
            {
                return true;
            }
        }
        return false;
    }

    bool NearBarrel()
    {
        if (facingLeft)
        {
            if (LeftNearTrigger.IsTouchingLayers(LayerMask.GetMask(JUMPABLE_OBSTACLES)) && Time.time - lastNearObstacles > 0.1)
            {
                lastNearObstacles = Time.time;
                return true;
            }
        } else
        {
            if (RightNearTrigger.IsTouchingLayers(LayerMask.GetMask(JUMPABLE_OBSTACLES)) && Time.time - lastNearObstacles > 0.1)
            {
                lastNearObstacles = Time.time;
                return true;
            }
        }
        return false;
    }

    void Walk()
    {
        if (facingLeft)// Depending on which way the robot is facing, walk in that direction
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
        if (OnGroundTrigger.IsTouchingLayers(LayerMask.GetMask(GROUND)) || OnGroundTrigger.IsTouchingLayers(LayerMask.GetMask(CRATE)) && Time.time - lastJump > 0.1)// If on ground jump the time stuff is to prevent spam jump
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            lastJump = Time.time;
        }
    }

    void Turn()
    {
        this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;// Flips the sprite
        facingLeft = !facingLeft;// Flips which way its facing
    }

    void Animation()
    {
        if (Mathf.Abs(body.velocity.x) > 0.01f)// Handles walking depending on velocity
        {
            this.GetComponent<Animator>().SetBool("Walking", true);
        }
        else
        {
            this.GetComponent<Animator>().SetBool("Walking", false);
        }
    }

}
