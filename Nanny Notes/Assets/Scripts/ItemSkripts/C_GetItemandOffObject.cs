using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_GetItemandOffObject : MonoBehaviour
{
    public GameObject[] on, off;
    public int countObject;

    private int nowObject;

    public void Check()
    {
        nowObject++;
        if (nowObject != countObject)
                return;
        foreach (var item in on)
            item.SetActive(true);
        foreach (var item in off)
            item.SetActive(false);
    }
    
}
