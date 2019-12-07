using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BlockClick : MonoBehaviour
{
    public C_TakeItem[] ollItems;

    private C_TakeItem selectedItem;

    public void FindSelectItem()
    {
        foreach (var item in ollItems)
            if (item.gameObject.activeSelf == true)
            {
                selectedItem = item;
                return;
            }
    }

    public void Click()
    {
        selectedItem.gameObject.SetActive(false);
        selectedItem.needItem.SetActive(false);
        gameObject.SetActive(false);
    }
}
