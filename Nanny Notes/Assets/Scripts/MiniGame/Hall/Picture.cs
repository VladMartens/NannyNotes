using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour
{
    public Hiden_Object[] hidenObjects;
    public List<GameObject> allhidenObjects;
    public GameObject hideObject_ListItem;
    public Transform listObjectTransform;
    public GameObject cleaningPrize;
    public int countHidenItems;

    public static Picture mc_this;

    private int countFoundItem = 0;

    [System.Serializable]
    public class Hiden_Object
    {
        public string nameItem;
        public int count, idColorText;
    }

    private void Start()
    {
        mc_this = this;
        CreateListHidenObject();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F4) && countFoundItem < hidenObjects.Length)
            ClearRoom();
    }

    private void CreateListHidenObject()
    {
        for (int i = 0; i < hidenObjects.Length; i++)
        {
            GameObject qtb = Instantiate(hideObject_ListItem, listObjectTransform);

            qtb.GetComponentInChildren<Text>().text = hidenObjects[i].nameItem;
            if (hidenObjects[i].count > 1)
            {
                qtb.GetComponentInChildren<Text>().text += " (" + hidenObjects[i].count + ")";
            }
            switch (hidenObjects[i].idColorText)
            {
                case 0:
                    qtb.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 1);
                    break;
                case 2:
                    qtb.GetComponentInChildren<Text>().color = new Color(175, 0, 136, 1);
                    break;
            }

        }
    }

    public void FoundComplete(string _nameItem)
    {
        for (int i = 0; i < hidenObjects.Length; i++)
        {
            if (hidenObjects[i].nameItem == _nameItem)
            {

                hidenObjects[i].count--;

                if (hidenObjects[i].count == 0)
                {
                    listObjectTransform.GetChild(i).GetComponentInChildren<Animation>().Play("Complete");
                    listObjectTransform.GetChild(i).GetComponentInChildren<Text>().text = hidenObjects[i].nameItem;
                }
                else
                    listObjectTransform.GetChild(i).GetComponentInChildren<Text>().text = hidenObjects[i].nameItem + " (" + hidenObjects[i].count + ")";

                if (countHidenItems == ++countFoundItem)
                    cleaningPrize.SetActive(true);
                break;
            }
        }
    }


    private void ClearRoom()
    {
        for (int i = 0; i < allhidenObjects.Count; i++)
            allhidenObjects[i].SetActive(false);
        for (int i = 0; i < hidenObjects.Length; i++)
            listObjectTransform.GetChild(i).GetComponentInChildren<Image>().fillAmount = 1;

        countFoundItem = hidenObjects.Length;

        cleaningPrize.SetActive(true);
    }

}
