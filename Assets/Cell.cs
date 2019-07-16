using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isIfStatement = false;
    public int linesOfCommands = 0;// Lines of open space inside the if statement
    [HideInInspector] public Actions action = Actions.Empty;
    [HideInInspector] public Conditions condition = Conditions.Empty;
    [HideInInspector] public bool insideIf;// if current line is in an if statement
}
