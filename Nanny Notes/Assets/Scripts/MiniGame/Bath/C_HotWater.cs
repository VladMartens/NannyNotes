using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_HotWater : MonoBehaviour
{
    public Image redLine;
    public GameObject blockClick;
    public Animation animation,zoom;
    public RectTransform coldCran, hotCran;

    bool UpCran;

    public void HotWaterUp()
    {
        UpCran = true;
        blockClick.SetActive(true);
        StartCoroutine("UpOrDownWater", 0.04);
        StartCoroutine("UpOrDownCran", hotCran);
    }

    public void HotWaterDown()
    {
        UpCran = false;
        blockClick.SetActive(true);
        StartCoroutine("UpOrDownWater", -0.04);
        StartCoroutine("UpOrDownCran", hotCran);
    }

    public void ColdWaterUp()
    {
        UpCran = true;
        blockClick.SetActive(true);
        StartCoroutine("UpOrDownWater", -0.02);
        StartCoroutine("UpOrDownCran", coldCran);
    }

    public void ColdWaterDown()
    {
        UpCran = false;
        blockClick.SetActive(true);
        StartCoroutine("UpOrDownWater", 0.02);
        StartCoroutine("UpOrDownCran", coldCran);
    }

    IEnumerator UpOrDownWater(float count)
    {
        count /= 30;
        for (int i = 0; i < 30; i++)
        {
            redLine.fillAmount += count;
            yield return new WaitForSeconds(0.05f);
        }
        blockClick.SetActive(false);
        if (redLine.fillAmount >= 0.73 && redLine.fillAmount <= 0.76)
        {
            C_Task.mc_this.Complete(6);
            animation.Play("Par");
            zoom.Play("BathClose");
        }

    }

    IEnumerator UpOrDownCran(Transform cran)
    {
        if (UpCran)
            for (int i = 0; i < 20; i++)
            {
                cran.rotation = new Quaternion(cran.rotation.x, cran.rotation.y, cran.rotation.z + 0.02f, cran.rotation.w);
                yield return new WaitForSeconds(0.05f);
            }
        else
            for (int i = 0; i < 20; i++)
            {
                cran.localRotation = new Quaternion(cran.localRotation.x, cran.localRotation.y, cran.localRotation.z - 0.02f, cran.localRotation.w);
                yield return new WaitForSeconds(0.05f);
            }
        if (redLine.fillAmount >= 0.73 && redLine.fillAmount <= 0.76)
        {
            C_Task.mc_this.Complete(6);
            animation.Play("Par");
            zoom.Play("BathClose");
        }
       
    }
}
