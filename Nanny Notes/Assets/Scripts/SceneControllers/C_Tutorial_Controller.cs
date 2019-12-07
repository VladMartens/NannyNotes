using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Tutorial_Controller : MonoBehaviour
{
    public static C_Tutorial_Controller mc_this;
    public bool activeanimation = false;

    public bool tutorial = false;

    private void Awake()
    {
        tutorial = false;
        mc_this = this;
    }

    public void StartAnimation(string nameAnimation)
    {
        if (tutorial)
        {
            if (nameAnimation == "Step2Open")
                activeanimation = true;
            if (nameAnimation == "Step4Open" && activeanimation == false)
                return;

            StartCoroutine("IE_StartAnimation", nameAnimation);
        }
       
    }

    IEnumerator IE_StartAnimation(string nameAnimation)
    {
        while (GetComponent<Animation>().isPlaying)
            yield return new WaitForSeconds(0.1f);

        GetComponent<Animation>().Play(nameAnimation);
    }

    public void HideObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void SkipTutorial()
    {
        tutorial = false;
    }
}
