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
    // Start is called before the first frame update
    void Start()
    {
        cellHandler = GameObject.FindObjectOfType<CellHandler>();
        origin = this.transform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(cellHandler.transform.parent);
        cellHandler.ResetBasedOnChild();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = objectPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!cellHandler.SnapAndSet(this.gameObject))
        {
            this.transform.localPosition = origin;
        }
    }
}
