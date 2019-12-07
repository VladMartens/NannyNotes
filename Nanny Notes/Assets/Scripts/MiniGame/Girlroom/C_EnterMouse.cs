using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class C_EnterMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject onn;

    public void OnPointerEnter(PointerEventData eventData)
    {
        onn.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onn.SetActive(false);
    }
}
