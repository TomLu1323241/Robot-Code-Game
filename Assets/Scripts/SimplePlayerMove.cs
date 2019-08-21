using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float jumpHeight = 5.5f;

    [SerializeField] Collider2D ground = null;

    Rigidbody2D body = null;

    float lastJump;

    //Actions action = Actions.Walk;

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
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerPrefs.SetInt("levelUnlock", 1);
            PlayerPrefs.Save();
        }
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
