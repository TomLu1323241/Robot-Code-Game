using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, EnvironmentAcions
{

    [SerializeField] float speed = 1;

    public void Action()
    {
        Debug.Log(this.transform.forward);
        this.GetComponent<Rigidbody2D>().velocity = this.transform.up * speed;
        this.GetComponent<Rigidbody2D>().drag = 0;
    }
}
