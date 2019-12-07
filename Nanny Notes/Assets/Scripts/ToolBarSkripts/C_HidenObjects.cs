using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class C_HidenObjects : MonoBehaviour
{
    public Hiden_Object[] hidenObjects;
    public List<GameObject> allhidenObjects;
    public GameObject hideObject_ListItem;
    public Transform listObjectTransform;
    public GameObject not_UsedObject;
    public C_Stage_Object cleaningPrize;
    public Transform defaultPosition;
    public int countHidenItems;
    public GameObject[] objectOn, objectOff;

    public static C_HidenObjects mc_this;

    private int countFoundItem = 0;

    [System.Serializable]
    public class Hiden_Object
    {
        public string nameItem;
        public int count;
        public int idColorText = 0; // 0 - default 1 - stage object 2 - hiden stage object 
        public bool complete = false;
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
        if (File.Exists(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/" + SceneManager.GetActiveScene().name + ".data"))
            hidenObjects = C_PlayerPrefs.mc_this.hiden_Objects;

        for (int i=0;i< hidenObjects.Length; i++)
        {
            GameObject qtb = Instantiate(hideObject_ListItem, listObjectTransform);

            qtb.GetComponentInChildren<Text>().text = hidenObjects[i].nameItem;
            switch (hidenObjects[i].idColorText)
            {
                case 0:
                    qtb.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 1);
                    break;
                case 2:
                    qtb.GetComponentInChildren<Text>().color = new Color(175, 0, 136, 1);
                    break;
            }
           
            if (hidenObjects[i].count > 1)
                qtb.GetComponentInChildren<Text>().text +=" ("+ hidenObjects[i].count+")";

            if (hidenObjects[i].complete == true)
            {
                qtb.GetComponentInChildren<Image>().fillAmount = 1;
                countFoundItem++;
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
                    
                    hidenObjects[i].complete = true;
                    countFoundItem++;
                }
                else if(hidenObjects[i].count == 0)
                    listObjectTransform.GetChild(i).GetComponentInChildren<Text>().text = hidenObjects[i].nameItem ;
                else 
                    listObjectTransform.GetChild(i).GetComponentInChildren<Text>().text = hidenObjects[i].nameItem+"("+ hidenObjects[i].count+ ")";
                                  

                if (countFoundItem == hidenObjects.Length)
                {
                    not_UsedObject.GetComponent<Animation>().Play("ClearRoom");
                    StartCoroutine("Timer");
                    for (int y = 0; y < objectOn.Length; y++)
                        objectOn[y].SetActive(true);
                    for (int y = 0; y < objectOff.Length; y++)
                        objectOff[y].SetActive(false);
                }
                break;
            }
        }       
    }

    private void ClearRoom()
    {
        for (int i = 0; i < allhidenObjects.Count; i++)
            allhidenObjects[i].SetActive(false);
        for (int i = 0; i < hidenObjects.Length; i++)
        {
            hidenObjects[i].complete = true;
            listObjectTransform.GetChild(i).GetComponentInChildren<Image>().fillAmount = 1;
        }          


        countFoundItem = hidenObjects.Length;
        not_UsedObject.GetComponent<Animation>().Play("ClearRoom");
        StartCoroutine("Timer");
        for (int y = 0; y < objectOn.Length; y++)
            objectOn[y].SetActive(true);
        for (int y = 0; y < objectOff.Length; y++)
            objectOff[y].SetActive(false);


    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        not_UsedObject.SetActive(false);
        cleaningPrize.GiveItem();
    }
}
