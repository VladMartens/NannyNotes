using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Drag_and_Drop : MonoBehaviour
{    
    public string idItem;
    public static Drag_and_Drop mc_init;
    public Vector3 offset;

    Vector3 basePosition;
    RectTransform rt;
    Canvas parentCanvas;
    Vector2 pos;

    void Awake()
    {
        mc_init = this;
        rt = GetComponent<RectTransform>();
        parentCanvas = gameObject.GetComponentInParent<Canvas>();
    }

    public void StartMove()
    {
        basePosition.x = rt.anchoredPosition.x;
        basePosition.y = rt.anchoredPosition.y;
        StartCoroutine("NewPosition");
    }

    IEnumerator NewPosition()
    {
        while(true)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out pos);
            rt.transform.position = parentCanvas.transform.TransformPoint(pos) + offset;
            GetComponent<Image>().color = new Color(255, 255, 255, 1);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
