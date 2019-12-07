using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_KitchenGame : MonoBehaviour
{
    public C_Produckt[] cookinSequence;
    public GameObject[] redLines;
    public C_BlockClick blockClick;
    public GameObject getPie;

    private int numberProduct = 0;

    private void Start()
    {
        for (int i = 0; i < redLines.Length; i++)
            if (redLines[i].activeSelf == true)
                numberProduct = i+1;
    }

    public void Check(C_Produckt produckt)
    {
        if (produckt == cookinSequence[numberProduct])
        {
            //produckt.GetComponent<Animation>().Play("123");
            blockClick.gameObject.SetActive(false);
            produckt.needItem.SetActive(false);
            produckt.redLine.SetActive(true);
            produckt.gameItem.SetActive(false);
            produckt.image.SetActive(true);
            produckt.itemKitchen.SetActive(false);
            Debug.Log("Complete");
            if (++numberProduct == cookinSequence.Length)
                getPie.SetActive(true);
            produckt.gameObject.SetActive(false);
        }
        else
        {
            produckt.gameItem.SetActive(false);
            blockClick.Click();
        }
        C_CursorManager.instance.UpdateToDefault();
        C_PlayerPrefs.mc_this.SaveSceneState();
    }
}
