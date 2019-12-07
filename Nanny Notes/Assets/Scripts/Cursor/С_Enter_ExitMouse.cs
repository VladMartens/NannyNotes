using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class С_Enter_ExitMouse : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource EnterMouse, ExitMouse;

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnterMouse.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ExitMouse.Play();
    }
}
