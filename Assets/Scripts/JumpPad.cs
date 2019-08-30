using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    [SerializeField] float jumpHeight = 13;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            col.gameObject.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity + Vector2.up * jumpHeight;
        }
    }
}
