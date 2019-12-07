using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_SystemMassage : MonoBehaviour
{
    public static C_SystemMassage mc_this;
    public Text text;

    private void Start()
    {
        mc_this = this;
    }

    public void ActiveMassage(string massage)
    {
        text.text = massage;
        GetComponent<Animation>().Play("ActiveSystemMassage");
    }

}
