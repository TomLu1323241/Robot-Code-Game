using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellHandler : MonoBehaviour
{

    RectTransform[] cells;

    void Start()
    {
        cells = this.GetComponentsInChildren<RectTransform>();
    }

    public void SnapAndSet(GameObject tickets)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if(inCell(cells[i], tickets))
            {
                tickets.transform.SetParent(cells[i].transform);
                tickets.transform.position = cells[i].transform.position;
            }
        }
        ResetBasedOnChild();
    }

    public void ResetBasedOnChild()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].childCount == 0)
            {
                cells[i].gameObject.GetComponent<Cell>().action = Actions.Empty;
            } else
            {
                cells[i].gameObject.GetComponent<Cell>().action = cells[i].transform.GetChild(0).GetComponent<Ticket>().action;
            }
        }
    }

    private bool inCell(RectTransform rect, GameObject tickets)
    {
        if (rect.transform.position.x + rect.rect.width / 2 > tickets.transform.position.x &&
            rect.transform.position.x - rect.rect.width / 2 < tickets.transform.position.x &&
            rect.transform.position.y + rect.rect.height / 2 > tickets.transform.position.y &&
            rect.transform.position.y - rect.rect.height / 2 < tickets.transform.position.y)
        {
            return true;
        }
            
        return false;
    }

    private void OnDrawGizmos()
    {
        RectTransform[] cells = this.GetComponentsInChildren<RectTransform>();
        Gizmos.color = Color.red;
        for (int i = 0; i < cells.Length; i++)
        {
            Gizmos.DrawWireCube(cells[i].transform.position, new Vector3(cells[i].rect.width - .2f, cells[i].rect.height - .2f));
        }
    }
}
