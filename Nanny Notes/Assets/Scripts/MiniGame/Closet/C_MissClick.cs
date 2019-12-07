using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_MissClick : MonoBehaviour
{
    public C_TakeItem cheess, trap;

    public void Click()
    {
        if (cheess.take && cheess.gameObject.activeSelf == true)
            cheess.MissClick();
        else
            trap.MissClick();
    }
}
