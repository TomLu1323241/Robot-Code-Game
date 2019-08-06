using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)// Loades next level
    {
        if (col.name.Equals("Player"))
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
