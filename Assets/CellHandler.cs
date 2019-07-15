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

    private void ifSetter()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].isIfStatement)
            {
                GameObject startIfInstance = GameObject.Instantiate(startIf);
                startIfInstance.transform.SetParent(cells[i].transform);
                startIfInstance.transform.localPosition = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect + indentation + startIfInstance.GetComponent<RectTransform>().rect.width / 2, 0);
                GameObject endIfInstance = GameObject.Instantiate(endIf);
                endIfInstance.transform.SetParent(cells[i + cells[i].linesOfCommands + 1].transform);
                endIfInstance.transform.localPosition = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect + indentation + endIfInstance.GetComponent<RectTransform>().rect.width / 2, 0);
            }
            for (int j = 1; j <= cells[i].linesOfCommands; j++)
            {
                cells[i + j].insideIf = true;
            }
        }
    }

    public bool SnapAndSet(GameObject tickets)
    {
        bool handled = false;
        for (int i = 0; i < cells.Length; i++)
        {
            bool alreadyHadCommand = false;
            for (int j = 0; j < cells[i].transform.childCount; j++)
            {
                if (cells[i].transform.GetChild(j).GetComponent<Ticket>() != null && inCell(cells[i], tickets))
                {
                    alreadyHadCommand = true;
                }
            }
            if (alreadyHadCommand)
            {
                break;
            }

            if (cells[i].isIfStatement && tickets.GetComponent<Ticket>().ifCondition && inCell(cells[i], tickets))
            {
                tickets.transform.SetParent(cells[i].transform);
                tickets.transform.position = cells[i].GetComponentInChildren<TextMeshProUGUI>().gameObject.transform.position;
                handled = true;
                break;
            } else if (!cells[i].isIfStatement && tickets.GetComponent<Ticket>().ifCondition && inCell(cells[i], tickets))
            {
                break;
            } else if (cells[i].isIfStatement && !tickets.GetComponent<Ticket>().ifCondition && inCell(cells[i], tickets))
            {
                break;
            }

            if (cells[i].insideIf && !tickets.GetComponent<Ticket>().ifCondition && inCell(cells[i], tickets))
            {
                handled = true;
                tickets.transform.SetParent(cells[i].transform);
                tickets.transform.localPosition = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect + indentation * 2 + tickets.GetComponent<RectTransform>().rect.width / 2, 0);
                break;
            }

            bool isEndBracket = false;
            for (int j = 0; j < cells[i].transform.childCount; j++)
            {
                if (cells[i].transform.GetChild(j).GetComponent<TextMeshProUGUI>() != null && cells[i].transform.GetChild(j).GetComponent<TextMeshProUGUI>().text.Equals("}") && inCell(cells[i], tickets))
                {
                    isEndBracket = true;
                    break;
                }
            }
            if (isEndBracket)
            {
                break;
            }

            if (inCell(cells[i], tickets) && !tickets.GetComponent<Ticket>().ifCondition)
            {
                handled = true;
                tickets.transform.SetParent(cells[i].transform);
                tickets.transform.localPosition = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect + indentation + tickets.GetComponent<RectTransform>().rect.width / 2, 0);
            }
        }
        ResetBasedOnChild();
        return handled;
    }

    public void ResetBasedOnChild()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            for (int j = 0; j < cells[i].transform.childCount; j++)
            {
                if (cells[i].transform.GetChild(j).GetComponent<Ticket>() != null)
                {
                    cells[i].gameObject.GetComponent<Cell>().action = cells[i].transform.GetChild(j).GetComponent<Ticket>().action;
                    cells[i].gameObject.GetComponent<Cell>().condition = cells[i].transform.GetChild(j).GetComponent<Ticket>().condition;
                    break;
                } else
                {
                    cells[i].gameObject.GetComponent<Cell>().action = Actions.Empty;
                    cells[i].gameObject.GetComponent<Cell>().condition = Conditions.Empty;
                }
            }

            if (cells[i].transform.childCount == 0)
            {
                cells[i].gameObject.GetComponent<Cell>().action = Actions.Empty;
                cells[i].gameObject.GetComponent<Cell>().condition = Conditions.Empty;
            }
        }
    }

    private bool inCell(Cell rect, GameObject tickets)
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

    private void OnDrawGizmos()
    {
        RectTransform[] cells = this.GetComponentsInChildren<RectTransform>();
        Gizmos.color = Color.blue;
        for (int i = 0; i < cells.Length; i++)
        {
            Gizmos.DrawWireCube(cells[i].transform.position, new Vector3(cells[i].rect.width - .2f, cells[i].rect.height - .2f));
        }
    }
}
