using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class Ticket : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    CellHandler cellHandler = null;
    public Actions action;

    // Start is called before the first frame update
    void Start()
    {
        cellHandler = GameObject.FindObjectOfType<CellHandler>();
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
        cellHandler.SnapAndSet(this.gameObject);
    }
}
