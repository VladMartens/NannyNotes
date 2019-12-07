using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_WaitGiveItem : MonoBehaviour
{
    public C_Stage_Object prize2;
    public Transform getItemTransform;

    public void Start()
    {
        StartCoroutine("WaitGivePrize");
    }

    private IEnumerator WaitGivePrize()
    {
        yield return new WaitForSeconds(3f);
        while (getItemTransform.localScale.x != 0)
            yield return new WaitForSeconds(0.5f);

        prize2.GiveItem();
    }
}
