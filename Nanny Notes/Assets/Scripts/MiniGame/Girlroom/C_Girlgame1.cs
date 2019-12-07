using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Girlgame1 : MonoBehaviour
{
    public GameObject[] offObjects, onObjects;

    private C_Girlgame1Element lastOpenedItem;
    private int countOpenedElements;
    private bool blockClick = false;


    public void Click(C_Girlgame1Element element)
    {
        if (blockClick)
            return;
        blockClick = true;
        element.activeImageBg.SetActive(true);

        if (lastOpenedItem == null)
        {
            lastOpenedItem = element;
            blockClick = false;
        }
        else
        {
            if (lastOpenedItem.idElement == element.idElement)
            {
                if (lastOpenedItem.idElement == 1 || lastOpenedItem.idElement == 2 || lastOpenedItem.idElement == 3 || lastOpenedItem.idElement == 4)
                    countOpenedElements++;
                lastOpenedItem.gameObject.SetActive(false);
                element.gameObject.SetActive(false);
                lastOpenedItem = null;
                C_Take_Drop_Item.mc_this.GetItem();
                if (countOpenedElements == 4)
                {
                    for (int i = 0; i < offObjects.Length; i++)
                        offObjects[i].SetActive(false);
                    for (int i = 0; i < onObjects.Length; i++)
                        onObjects[i].SetActive(true);

                    C_PlayerPrefs.mc_this.SaveSceneState();
                }
                blockClick = false;
            }
            else
            {
                lastOpenedItem.activeImageBg.SetActive(false);
                element.activeImageBg.SetActive(false);
                lastOpenedItem = null;
                blockClick = false;
            }
        }
    }
}
