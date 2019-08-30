using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name.Equals("Player"))
        {
            SceneLoader.ReloadScene();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.name.Equals("Player"))
        {
            SceneLoader.ReloadScene();
        }
    }

}
