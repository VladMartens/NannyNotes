using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_PiramidGame : MonoBehaviour
{
    public GameObject[] on, off;
    public C_Stage_Object prize;
    public Animation animationZoom;
    public C_Items item;
    public Vector3 itemPosition;

    public static C_PiramidGame mc_this;

    private void Start()
    {
        mc_this = this;
    }

    public void Win()
    {
        animationZoom.Play("PiramidClose");
        prize.GiveItem();
        for (int i = 0; i < on.Length; i++)
            on[i].SetActive(true);
        for (int i = 0; i < off.Length; i++)
            off[i].SetActive(false);
        C_PlayerPrefs.mc_this.SaveSceneState();
    }

    public void MissClick()
    {
        item.MissClick();
        item = null;
    }

}
