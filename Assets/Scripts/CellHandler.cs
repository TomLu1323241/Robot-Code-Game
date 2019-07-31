using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CellHandler : MonoBehaviour
{

    [SerializeField] float indentation = 3;
    [SerializeField] GameObject startIf = null;
    [SerializeField] GameObject endIf = null;

    Cell[] cells;

    void Start()
    {
        cells = this.GetComponentsInChildren<Cell>();
        ifSetter();
    }

    private void ifSetter()// Sets the if statement and the end braket for it
    {
        for (int i = 0; i < cells.Length; i++)// Loops thought every cell and checks if it is an if statement
        {
            if (cells[i].isIfStatement)// Checks if it is an if statement
            {
                // Sets the if statement graphics
                GameObject startIfInstance = GameObject.Instantiate(startIf);
                startIfInstance.transform.SetParent(cells[i].transform);
                startIfInstance.transform.localPosition = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect + indentation + startIfInstance.GetComponent<RectTransform>().rect.width / 2, 0);
                // Sets the end braket graphics
                GameObject endIfInstance = GameObject.Instantiate(endIf);
                endIfInstance.transform.SetParent(cells[i + cells[i].linesOfCommands + 1].transform);
                endIfInstance.transform.localPosition = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect + indentation + endIfInstance.GetComponent<RectTransform>().rect.width / 2, 0);
            }
            // Sets the state for each line in the if statement as insideIf to be true
            for (int j = 1; j <= cells[i].linesOfCommands; j++)
            {
                cells[i + j].insideIf = true;
            }
        }
    }

    public bool SnapAndSet(GameObject tickets)
    {
        bool handled = false;// Checks if the ticket passed into the cells is handled
        // Loop through cells to find optimal cells for the ticket
        // If optimal cell is found set the ticket as its child
        Ticket original = null;
        for (int i = 0; i < cells.Length; i++)
        {
            bool inCell = InCell(cells[i], tickets);
            for (int j = 0; j < cells[i].transform.childCount; j++)// Loops though the children of the cell to find a ticket and maybe swap
            {
                if (cells[i].transform.GetChild(j).GetComponent<Ticket>() != null && inCell)
                {
                    original = cells[i].transform.GetChild(j).GetComponent<Ticket>();
                }
            }

            // Check if the ticket is both a condidtion and the user dragged it into a if cell
            if (cells[i].isIfStatement && tickets.GetComponent<Ticket>().ifCondition && inCell)
            {
                tickets.transform.SetParent(cells[i].transform);
                tickets.transform.position = cells[i].GetComponentInChildren<TextMeshProUGUI>().gameObject.transform.position;
                handled = true;
                break;
            }
            else if (!cells[i].isIfStatement && tickets.GetComponent<Ticket>().ifCondition && inCell)
            {
                break;
            }
            else if (cells[i].isIfStatement && !tickets.GetComponent<Ticket>().ifCondition && inCell)
            {
                break;
            }

            // Checks if the ticket is a action inside the if statement
            if (cells[i].insideIf && !tickets.GetComponent<Ticket>().ifCondition && inCell)
            {
                handled = true;
                tickets.transform.SetParent(cells[i].transform);
                tickets.transform.localPosition = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect + indentation * 2 + tickets.GetComponent<RectTransform>().rect.width / 2, 0);
                break;
            }

            // Checks if the user drags anything into a end braket
            bool isEndBracket = false;
            for (int j = 0; j < cells[i].transform.childCount; j++)
            {
                if (cells[i].transform.GetChild(j).GetComponent<TextMeshProUGUI>() != null && cells[i].transform.GetChild(j).GetComponent<TextMeshProUGUI>().text.Equals("}") && inCell)
                {
                    isEndBracket = true;
                    break;
                }
            }
            if (isEndBracket)
            {
                break;
            }

            // Checks if the player drags an action into a normal cell
            if (inCell && !tickets.GetComponent<Ticket>().ifCondition)
            {
                handled = true;
                tickets.transform.SetParent(cells[i].transform);
                tickets.transform.localPosition = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect + indentation + tickets.GetComponent<RectTransform>().rect.width / 2, 0);
            }
        }

        // Removes the original swaps to the new one
        if (original != null && handled)
        {
            // You will question why is there two and how this works and so will I...
            original.Reset();
            original.Reset();
        }

        // Resets the actions and conditions of every cell depending on their child
        resetBasedOnChild();
        // Return if the ticket passed in is handled
        // Handled means it is now a child of a cell and not handled means it was dragged into an improper position
        return handled;
    }

    public void resetBasedOnChild()
    {
        for (int i = 0; i < cells.Length; i++)// Loops though every cell
        {
            for (int j = 0; j < cells[i].transform.childCount; j++)// Loops thought every child
            {
                if (cells[i].transform.GetChild(j).GetComponent<Ticket>() != null)// Finds the ticket in the cell
                {
                    // Set the action and condition of the ticket
                    cells[i].gameObject.GetComponent<Cell>().action = cells[i].transform.GetChild(j).GetComponent<Ticket>().action;
                    cells[i].gameObject.GetComponent<Cell>().condition = cells[i].transform.GetChild(j).GetComponent<Ticket>().condition;
                    break;// Breaks if there is a ticket
                }
                else
                {
                    // if there is no ticket then set all to empty
                    cells[i].gameObject.GetComponent<Cell>().action = Actions.Empty;
                    cells[i].gameObject.GetComponent<Cell>().condition = Conditions.Empty;
                }
            }

            if (cells[i].transform.childCount == 0)// No children means not ticket
            {
                cells[i].gameObject.GetComponent<Cell>().action = Actions.Empty;
                cells[i].gameObject.GetComponent<Cell>().condition = Conditions.Empty;
            }
        }
    }

    private bool InCell(Cell rect, GameObject tickets)// Check if the position of a ticket is within a certain cell
    {
        if (rect.transform.position.x + rect.GetComponent<RectTransform>().rect.width / 2 > tickets.transform.position.x &&
            rect.transform.position.x - rect.GetComponent<RectTransform>().rect.width / 2 < tickets.transform.position.x &&
            rect.transform.position.y + rect.GetComponent<RectTransform>().rect.height / 2 > tickets.transform.position.y &&
            rect.transform.position.y - rect.GetComponent<RectTransform>().rect.height / 2 < tickets.transform.position.y)
        {
            return true;
        }
        return false;
    }
}
