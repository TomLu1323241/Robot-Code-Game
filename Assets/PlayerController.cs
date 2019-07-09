using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    List<Cells> cells;

    void Start()
    {
        cells = new List<Cells>(GameObject.FindObjectsOfType<Cells>());
        List<Cells> sorted = new List<Cells>();
        for (int i = 0; i < sorted.Count; i++) //TODO: write sorting algorithm based on trasform
        {
            Cells temp = cells[0];
            for (int j = 0; j < cells.Count; j++)
            {
            }
        }
        foreach(Cells cell in cells)
        {
            Debug.Log(cell.transform.position.x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
