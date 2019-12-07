using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_SaveAndLoadStatusScene : MonoBehaviour
{  
    public GameObject[] loadObject;

    private Dictionary<string, bool> valuePairs;

    void Start()
    {
        LoadScene();
    }
    
    public void LoadScene()
    {
        valuePairs = C_PlayerPrefs.mc_this.LoadStatusScene().statusInMoreScene;
        if (valuePairs == null)
        {
            valuePairs = new Dictionary<string, bool>();
            return;
        }
            
        for (int i = 0; i < loadObject.Length; i++)
            if (valuePairs.ContainsKey(loadObject[i].name))
                loadObject[i].SetActive(valuePairs[loadObject[i].name]);
    }

    public void SaveScene(string nameItem)
    {
        if (valuePairs.ContainsKey(nameItem))
            valuePairs[nameItem] = true;
        else
            valuePairs.Add(nameItem, true);

        C_PlayerPrefs.mc_this.SaveStatusScene(valuePairs);
    }

    public void DeleteStatus(string nameItem)
    {
        if (valuePairs.ContainsKey(nameItem))
            valuePairs.Remove(nameItem);

        C_PlayerPrefs.mc_this.SaveStatusScene(valuePairs);
    }

}
