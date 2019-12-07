using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class C_PieItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;
    public string idItem;

    Transform transform_image;

    public void Click()
    {
        if (C_ToolBar_Manager.mc_this.DragAndDropItem.activeSelf == true)
            return;
        C_ToolBar_Manager.mc_this.DragAndDropItem.gameObject.SetActive(true);
        C_ToolBar_Manager.mc_this.blockclick.SetActive(true);
        Drag_and_Drop.mc_init.StartMove();
        Drag_and_Drop.mc_init.idItem = idItem;
        GetComponentInChildren<Image>().color = new Color(255f, 255f, 255f, 0.5f);
        Drag_and_Drop.mc_init.GetComponent<Image>().sprite = GetComponentInChildren<Image>().sprite;
        Cursor.visible = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (C_ToolBar_Manager.mc_this.DragAndDropItem.activeSelf == true)
            return;
        transform_image = GetComponentInChildren<Image>().transform;
        StartCoroutine("SizeUp_Down", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine("SizeUp_Down");
        StartCoroutine("SizeUp_Down", false);
    }

    IEnumerator SizeUp_Down(bool sizeUp)
    {
        if (sizeUp)
            while (transform_image.localScale.x <= 1.1f)
            {
                transform_image.localScale = new Vector3(transform_image.localScale.x + 0.01f, transform_image.localScale.y + 0.01f, 0);
                yield return new WaitForSeconds(0.01f);
            }
        else
            while (transform_image.localScale.x >= 1)
            {
                transform_image.localScale = new Vector3(transform_image.localScale.x - 0.01f, transform_image.localScale.y - 0.01f, 0);
                yield return new WaitForSeconds(0.01f);
            }
    }
}
