using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_HidenObjectChicks : MonoBehaviour
{
    public Text hidenobjectText;
    public GameObject[] gameObjects;

    private int countObject ;

    private void Start()
    {
        StartText();
    }

    public void StartText()
    {
        for (int i = 0; i < gameObjects.Length; i++)
            if (gameObjects[i].activeSelf == true)
                countObject++;
        hidenobjectText.text = "There are " + countObject + " fence parts left.";
    }

    public void FindElement()
    {
        countObject--;
        hidenobjectText.text = "There are " + countObject + " fence parts left.";
    }
}
