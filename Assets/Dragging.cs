using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class Dragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start");
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = objectPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Going");
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = objectPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
    }

    private void OnMouseDown()
    {
        Debug.Log("Ran");
        OnDrag(null);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
