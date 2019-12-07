using UnityEngine;

public class С_items_to_unlock : MonoBehaviour
{
    public GameObject[] objectsToOpen, on, off;

    public void Check()
    {
        for (int i = 0; i < objectsToOpen.Length; i++)
            if (objectsToOpen[i].activeSelf == false)
                return;
        foreach (var item in on)
            item.SetActive(true);
        foreach (var item in off)
            item.SetActive(false);
    }

    public void StartCutscene()
    {
        for (int i = 0; i < objectsToOpen.Length; i++)
            if (objectsToOpen[i].activeSelf == false)
                return;
        C_Cutscene_Controller.mc_this.StartCutscene(1);
    }
}
