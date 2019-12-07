using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class C_Stick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject StickTransform;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (C_PiramidGame.mc_this.item == null)
            C_CursorManager.instance.UpdateTo(C_CursorManager.Cursors.get);

        else
            C_CursorManager.instance.UpdateTo(C_CursorManager.Cursors.set);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        C_CursorManager.instance.UpdateToDefault();
    }

    public void Checked()
    {
        if (C_PiramidGame.mc_this.item == null)
        {
            if (StickTransform.transform.childCount == 0)
                return;
            C_PiramidGame.mc_this.item = StickTransform.transform.GetChild(0).GetComponent<C_Items>();
            C_PiramidGame.mc_this.itemPosition = C_PiramidGame.mc_this.item.transform.localPosition;
            StickTransform.transform.GetChild(0).GetComponent<C_Items>().Click();
            C_CursorManager.instance.UpdateTo(C_CursorManager.Cursors.set);
        }
        else
        {
            if (C_PiramidGame.mc_this.item.transform.parent == StickTransform.transform)
            {
                C_CursorManager.instance.UpdateTo(C_CursorManager.Cursors.get);
                C_PiramidGame.mc_this.item.MissClick();
                return;
            }          
            
            if(StickTransform.transform.childCount != 0 )
                if(int.Parse(C_PiramidGame.mc_this.item.name) < int.Parse(StickTransform.transform.GetChild(0).name))
                {
                    C_CursorManager.instance.UpdateTo(C_CursorManager.Cursors.get);
                    C_PiramidGame.mc_this.item.MissClick();
                    return;
                }
            C_PiramidGame.mc_this.item.transform.SetParent(StickTransform.transform);
            C_PiramidGame.mc_this.item.transform.SetSiblingIndex(0);
            C_PiramidGame.mc_this.item.transform.localPosition = new Vector3(0, 0, 0);
            C_PiramidGame.mc_this.item.StopAllCoroutines();
            C_PiramidGame.mc_this.item.blockZone.SetActive(false);
            C_PiramidGame.mc_this.item = null;

            C_CursorManager.instance.UpdateTo(C_CursorManager.Cursors.get);
            int number = 1;
            Transform[] itemsInStick = StickTransform.GetComponentsInChildren<Transform>();
            if (itemsInStick.Length < 8)
                return;
            for (int i = 7; i > 1; i--)
            {
                if (itemsInStick[number].name != i.ToString())
                    return;
                number++;
            }
            C_PiramidGame.mc_this.Win();
        }

    }
}
