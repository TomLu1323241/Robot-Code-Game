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
    bool facingLeft = true;

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
    }

    private void SetUpIfStatements()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].isIfStatement)
            {
                for (int j = 0; j < cells[i].linesOfCommands; j++)
                {
                    i++;
                }
            }
        }
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
                        body.velocity = new Vector2(speed, body.velocity.y);
                    } else
                    {
                        body.velocity = new Vector2(-speed, body.velocity.y);
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
