using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreasurePlate : MonoBehaviour, Interactions
{
    [SerializeField] Sprite[] animation = null;
    [SerializeField] float animationSpeed = 0.3f;

    List<EnvironmentAcions> environmentAcion = new List<EnvironmentAcions>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name.Contains("Player") || col.transform.name.Contains("Crate"))
        {
            Interact();
            StartCoroutine(Animation(true));
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.name.Contains("Player") || col.transform.name.Contains("Crate"))
        {
            StartCoroutine(Animation(false));
        }
    }

    IEnumerator Animation(bool pressingDown)
    {
        if (pressingDown)
        {
            for (int i = 0; i < animation.Length; i++)
            {
                this.GetComponent<SpriteRenderer>().sprite = animation[i];
                yield return new WaitForSeconds(animationSpeed);
            }
        } else
        {
            for (int i = animation.Length - 1; i >= 0; i--)
            {
                this.GetComponent<SpriteRenderer>().sprite = animation[i];
                yield return new WaitForSeconds(animationSpeed);
            }
        }
    }

    public void Interact()
    {
        foreach (EnvironmentAcions action in environmentAcion)
        {
            action.Action();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            environmentAcion.Add((EnvironmentAcions)this.transform.GetChild(i).GetComponent(typeof(EnvironmentAcions)));
        }
        if (this.transform.childCount == 0)
        {
            Debug.Log(this.name + " will not preform any actions as it has no child containing an action");
        }
    }
}
