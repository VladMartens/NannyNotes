using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_SausageMagazine : MonoBehaviour
{
    public GameObject[] needSausages, dogs;

    private GameObject sausagesGet;
    private int countSausage = 0;

    private void Start()
    {
        StartCoroutine("StatrScene");
    }

    IEnumerator StatrScene()
    {
        yield return new WaitForSeconds(0.5f);
        if (dogs[0].activeSelf == true)
            yield return 0;
        else
            for (int i = 0; i < dogs.Length; i++)
                if (dogs[i].activeSelf == true)
                    countSausage = ++i;
    }

    public void GetSausage(GameObject sausage)
    {
        sausagesGet = sausage;
        needSausages[countSausage].SetActive(true);
    }

    public void SetSausage()
    {
        C_Take_Drop_Item.mc_this.PutItem();
        countSausage++;
        sausagesGet.SetActive(false);
    }
}
