using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Arrow : MonoBehaviour
{
    public bool x,y;

    private void Start()
    {
        StartCoroutine("Anim");
    }

    IEnumerator Anim()
    {
        bool back = false;
        if (y)
        {
            while(y)
            {
                for(int i = 0; i < 30; i++)
                {
                    if (back)
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.5f, transform.localPosition.z);
                    else
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.5f, transform.localPosition.z);
                    yield return new WaitForSeconds(0.005f);
                }
                back = !back;
            }
        }
        else if (x)
        {
            while (x)
            {
                for (int i = 0; i < 30; i++)
                {
                    if (back)
                        transform.localPosition = new Vector3(transform.localPosition.x + 0.5f, transform.localPosition.y , transform.localPosition.z);
                    else
                        transform.localPosition = new Vector3(transform.localPosition.x - 0.5f, transform.localPosition.y , transform.localPosition.z);
                    yield return new WaitForSeconds(0.005f);
                }
                back = !back;
            }
        }
    }
}
