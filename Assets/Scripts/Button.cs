using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, Interactions
{

    List<EnvironmentAcions> environmentAcion = new List<EnvironmentAcions>();
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
        for(int i = 0; i < this.transform.childCount; i++)
        {
            environmentAcion.Add((EnvironmentAcions)this.transform.GetChild(i).GetComponent(typeof(EnvironmentAcions)));
        }
        if(this.transform.childCount == 0)
        {
            Debug.Log(this.name + " will not preform any actions as it has no child containing an action");
        }
    }
}
