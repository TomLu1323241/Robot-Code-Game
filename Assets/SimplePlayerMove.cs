using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;

    [SerializeField] Collider2D ground;

    Rigidbody2D body;

    float lastJump;

    void Start()
    {
        lastJump = Time.time;
        body = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.W) && this.GetComponent<Collider2D>().IsTouching(ground) && Time.time - lastJump > 0.1)
        {
            body.velocity = body.velocity + Vector2.up * jumpHeight;
            lastJump = Time.time;
        }
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.A))
        {
            body.velocity = new Vector2(-speed, body.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.velocity = new Vector2(speed, body.velocity.y);
        }
    }
}
