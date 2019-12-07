using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class C_CursorDialog : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        C_CursorManager.instance.UpdateTo(C_CursorManager.Cursors.talk);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        C_CursorManager.instance.UpdateToDefault();       
    }

    public void DialogWindowOff()
    {
        gameObject.SetActive(false);
    }
}
