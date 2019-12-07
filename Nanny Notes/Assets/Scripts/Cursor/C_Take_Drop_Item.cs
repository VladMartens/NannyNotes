using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Take_Drop_Item : MonoBehaviour
{
    public AudioSource[] takeItemInventory, dropItemInventory, putItem,errorItem;
    public AudioSource sacces,needItem,getItem,hidenObject;

    public static C_Take_Drop_Item mc_this;

    private void Start()
    {
        mc_this = this;
    }

    public void TakeItem()
    {
        takeItemInventory[Random.Range(0, takeItemInventory.Length)].Play();
    }
    public void DropItem()
    {
        dropItemInventory[Random.Range(0, dropItemInventory.Length)].Play();
    }
    public void PutItem()
    {
        putItem[Random.Range(0, putItem.Length)].Play();
    }
    public void ErrorItem()
    {
        errorItem[Random.Range(0, errorItem.Length)].Play();
    }
    public void Sacces()
    {
        sacces.Play();
    }
    public void NeedItem()
    {
        needItem.Play();
    }
    public void GetItem()
    {
        getItem.Play();
    }
    public void HidenObject()
    {
        hidenObject.Play();
    }
    
}
