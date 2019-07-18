using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{

    [SerializeField] bool goingLeft;
    [SerializeField] float speed;

    void Start()
    {
        if (goingLeft)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.left * speed;
        } else
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.right * speed;
        }
        
    }
}
