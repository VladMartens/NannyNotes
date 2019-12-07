using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class C_BrockenVase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject[] ObjectsOn, ObjectsOff;    

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        C_CursorManager.instance.UpdateTo(C_CursorManager.Cursors.set);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        C_CursorManager.instance.UpdateToDefault();
       
    }

    public void ClickTarget()
    {
        C_CursorManager.instance.UpdateToDefault();

        for (int i = 0; i < ObjectsOn.Length; i++)
            ObjectsOn[i].SetActive(true);
        for (int i = 0; i < ObjectsOff.Length; i++)
            ObjectsOff[i].SetActive(false);
    }
}
