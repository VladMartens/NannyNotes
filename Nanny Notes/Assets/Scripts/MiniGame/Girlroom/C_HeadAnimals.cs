using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class C_HeadAnimals : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private new Camera camera;
    private Vector3 offset;

    void Awake()
    {
        camera = Camera.allCameras[0];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - camera.ScreenToWorldPoint(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = camera.ScreenToWorldPoint(eventData.position);
        transform.position = new Vector3(newPos.x + offset.x, newPos.y + offset.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
