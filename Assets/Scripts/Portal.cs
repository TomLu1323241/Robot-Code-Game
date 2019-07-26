using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject portal = null;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name.Equals("Player"))
        {
            col.transform.position = portal.transform.GetChild(0).transform.position;
        }
    }

    private void Start()
    {
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
    }
}
