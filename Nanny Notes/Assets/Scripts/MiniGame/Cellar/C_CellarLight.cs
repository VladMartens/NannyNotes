using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CellarLight : MonoBehaviour
{
    public Vector3 offset;

    private Vector3 position;
    
    RectTransform rt;
    Canvas parentCanvas;
    Vector2 pos;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        parentCanvas = gameObject.GetComponentInParent<Canvas>();
        position = transform.position;
        StartCoroutine("NewPosition");
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
