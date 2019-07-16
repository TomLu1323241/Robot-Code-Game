using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class Ticket : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    CellHandler cellHandler = null;

    public bool ifCondition = false;
    public Actions action;
    public Conditions condition;

    Vector3 origin;
    
    void Start()
    {
        cellHandler = GameObject.FindObjectOfType<CellHandler>();
        origin = this.transform.localPosition;// Save initial posistion to snap back to after player misplaces it
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(cellHandler.transform.parent);// Removes this ticket from the cell essentially removing this function from running
        cellHandler.ResetBasedOnChild();// Resets the all the cells and their commands
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Simple follow mouse formula
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = objectPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!cellHandler.SnapAndSet(this.gameObject))// Checks if this ticket has snapped to a posistion in one of the cells
        {
            this.transform.localPosition = origin;// if not it resets it back to the origin
        }
    }
}
