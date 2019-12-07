using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop_Zoom : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject left, right;
    private float borderLeft, borderRight;
    private new Camera camera;
    private Vector3 offset;

    void Awake()
    {
        camera = Camera.allCameras[0];       
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        borderLeft = left.transform.position.x;
        borderRight = right.transform.position.x;
        offset = transform.position - camera.ScreenToWorldPoint(eventData.position);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = camera.ScreenToWorldPoint(eventData.position);
        if(newPos.x + offset.x > borderLeft && newPos.x + offset.x < borderRight)
         transform.position = new Vector3(newPos.x + offset.x, transform.position.y, transform.position.z);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
