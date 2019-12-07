using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Find_Elements : MonoBehaviour
{
    public GameObject errorFind1, errorFind2;
    public Animation Zoom;
    public GameObject[] offObjects, onObjects;
    public AudioSource SoundOk, SoundWrong, SoundClick;

    private MiniGame_Element lastOpenedItem;
    private int countOpenedElements;
    private bool blockClick = false;

    private void Start()
    {
        Zoom.Play("StartMiniGame");
    }
    public void Click(MiniGame_Element element)
    {
        if (blockClick)
            return;
        SoundClick.Play();
        blockClick = true;
        element.GetComponent<Button>().interactable = false;

        if (lastOpenedItem == null)
        {
            lastOpenedItem = element;
            StartCoroutine("OpenItem", element);
            blockClick = false;
        }
        else
        {
            if (lastOpenedItem.idElement == element.idElement)
            {
                StartCoroutine("OpenItem", element);
                SoundOk.Play();
                countOpenedElements++;
                lastOpenedItem = null;
                if (countOpenedElements == 12)
                {
                    for (int i = 0; i < offObjects.Length; i++)
                        offObjects[i].SetActive(false);
                    for (int i = 0; i < onObjects.Length; i++)
                        onObjects[i].SetActive(true);
                    Zoom.Play("CloseGame");
                    C_PlayerPrefs.mc_this.SaveSceneState();
                }                   
                blockClick = false;
            }
            else
                StartCoroutine("Error", element);
        }
    }

    IEnumerator OpenItem(MiniGame_Element element)
    {
        for (int i = 0; i < 20; i++)
        {
            element.image.color = new Color(255, 255, 255, element.image.color.a + 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Error(MiniGame_Element element)
    {
        SoundWrong.Play();
        errorFind1.transform.SetParent(lastOpenedItem.transform);
        errorFind2.transform.SetParent(element.transform);
        errorFind1.transform.localPosition = new Vector3(0, 0);
        errorFind2.transform.localPosition = new Vector3(0, 0);
        for (int i = 0; i < 20; i++)
        {
            element.image.color = new Color(255, 255, 255, element.image.color.a + 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < 20; i++)
        {
            errorFind1.GetComponent<Image>().color = new Color(255, 255, 255, errorFind1.GetComponent<Image>().color.a + 0.025f);
            errorFind2.GetComponent<Image>().color = new Color(255, 255, 255, errorFind2.GetComponent<Image>().color.a + 0.025f);
            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 20; i++)
        {
            errorFind1.GetComponent<Image>().color = new Color(255, 255, 255, errorFind1.GetComponent<Image>().color.a - 0.025f);
            errorFind2.GetComponent<Image>().color = new Color(255, 255, 255, errorFind2.GetComponent<Image>().color.a - 0.025f);
            element.image.color = new Color(255, 255, 255, element.image.color.a - 0.05f);
            lastOpenedItem.image.color = new Color(255, 255, 255, element.image.color.a - 0.05f);
            yield return new WaitForSeconds(0.001f);
        }        
        element.GetComponent<Button>().interactable = true;
        lastOpenedItem.GetComponent<Button>().interactable = true;
        lastOpenedItem = null;
        blockClick = false;
    }
}
