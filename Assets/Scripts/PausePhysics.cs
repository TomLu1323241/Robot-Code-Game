using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePhysics : MonoBehaviour
{
    
    public void StopPhysics()
    {
        Time.timeScale = 0;
    }

    public void StartPhysics()
    {
        Time.timeScale = 1;
    }
}
