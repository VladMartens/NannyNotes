using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Lock : MonoBehaviour
{
    public List<GameObject> numbers;
    public Animation animation;
    public GameObject on,off;
    private bool truePass = true;
    List<int> tueNamberPass = new List<int>() { 4, 2, 3, 8 };
    int num=0;

    public void Click(int number)
    {
        if (tueNamberPass[num] == number)
        {
            if (num == 3&& truePass)
            {
                animation.Play("lockedClose");
                on.SetActive(true);
                off.SetActive(false);
                C_PlayerPrefs.mc_this.SaveSceneState();
                C_Hint.mc_this.NextStep(466);
            }               
            num++;
        }           
        else
        {
            if (num == 3)
            {
                for (int i = 0; i < numbers.Count; i++)
                    numbers[i].SetActive(true);
                num = 0;
                truePass = true;
                return;
            }
            truePass = false;
            num++;
        }

    }
}
