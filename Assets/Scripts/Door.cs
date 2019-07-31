using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, EnvironmentAcions
{

    [SerializeField] float speed = 1;
    [SerializeField] float distance = 3;

    public void Action()
    {
        this.GetComponent<Rigidbody2D>().velocity = this.transform.up * speed;
        this.GetComponent<Rigidbody2D>().drag = 0;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(distance / speed);
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.up * distance);
    }
}
