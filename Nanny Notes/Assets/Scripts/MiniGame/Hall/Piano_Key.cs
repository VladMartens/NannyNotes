using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano_Key : MonoBehaviour
{
    public void Press(GameObject key)
    {
        Piano_MiniGame.mc_this.PressKey(key);
    }
}
