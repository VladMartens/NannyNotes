using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Items : MonoBehaviour
{
    public Vector3 offset;
    public GameObject blockZone;

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
        C_CursorManager.instance.UpdateToDefault();
        position = transform.localPosition;
        StartCoroutine("NewPosition");
        GetComponentInChildren<Image>().raycastTarget = false;
        blockZone.SetActive(true);
    }

    public void MissClick()
    {
        StopAllCoroutines();
        transform.localPosition = position;
        GetComponentInChildren<Image>().raycastTarget = true;
        blockZone.SetActive(false);
        C_PiramidGame.mc_this.item = null;
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
