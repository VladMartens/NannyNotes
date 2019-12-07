using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class С_Сooking : MonoBehaviour
{
    public GameObject[] on, off;
    public Text timerText;

    void Start()
    {
        StartCoroutine("Coocking");
    }

    IEnumerator Coocking()
    {
        for(int i = 5;i>0;i--)
        {
            timerText.text = "00:0"+i.ToString();
            yield return new WaitForSeconds(1);
        }

        foreach (GameObject item in on)
            item.SetActive(true);
        Off();
    }

    void Off()
    {
        for (int i = 0; i < off.Length; i++)
            off[i].SetActive(false);
    }
}
