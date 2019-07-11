using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isIfStatement = false;
    public int linesOfCommands = 0;
    [HideInInspector] public Actions action = Actions.Empty;
}
