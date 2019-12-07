using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class C_TakeItem : MonoBehaviour
{
    public string idItem;
    public Vector3 offset;
    public GameObject blockZone,needItem;
    public bool take = false;

    public Image OffObject;

    private Vector3 position;

    Vector3 basePosition;
    RectTransform rt;
    Canvas parentCanvas;
    Vector2 pos;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        parentCanvas = gameObject.GetComponentInParent<Canvas>();
        position = transform.localPosition;
    }

    public void Click()
    {
        OffObject.raycastTarget = false;
        take = true;
        StartCoroutine("NewPosition");
        
        GetComponentInChildren<Image>().raycastTarget = false;
        blockZone.SetActive(true);
        Cursor.visible = false;
    }

    public void MissClick()
    {
        OffObject.raycastTarget = true;
        StopAllCoroutines();
        take = false;
        StartCoroutine("Back");
        Cursor.visible = true;
        GetComponentInChildren<Image>().raycastTarget = true;
        blockZone.SetActive(false);
        needItem.SetActive(false);
    }

    IEnumerator Back()
    {
        float x = (transform.localPosition.x - position.x /*+ 0.13f*/) / 50,
              y = (transform.localPosition.y - position.y /*+ 0.13f*/) / 50;
        for (int i = 0; i < 50; i++)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - x, transform.localPosition.y - y, 0);
            yield return new WaitForSeconds(0.015f);
        }
    }

    IEnumerator NewPosition()
    {
        while (true)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out pos);
            rt.transform.position = parentCanvas.transform.TransformPoint(pos) + offset;
            
            yield return new WaitForSeconds(0.01f);
        }
    }

}