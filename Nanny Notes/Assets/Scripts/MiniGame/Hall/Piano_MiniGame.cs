using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piano_MiniGame : MonoBehaviour
{
    public GameObject[] notes;
    public GameObject[] keyOrder;
    public GameObject enable_completion;

    public Sprite pressedKey, redKey, normalKey;

    public AudioSource[] soundsKey;

    public static Piano_MiniGame mc_this;

    private int countRightKey = 0;

    private void Awake()
    {
        mc_this = this;
        GetComponent<Animation>().Play();
        StartCoroutine("Sound");
    }

    public void PressKey(GameObject pressedKey)
    {
        StartCoroutine("ClickKey", pressedKey);

        if (pressedKey == keyOrder[countRightKey])
        {
            notes[countRightKey].GetComponent<Image>().color = new Color(0, 200, 0, 1);
            if (++countRightKey > keyOrder.Length - 1)
            {
                pressedKey.GetComponent<Image>().sprite = normalKey;
                enable_completion.SetActive(true);
                gameObject.SetActive(false);
            }
        }
        else
            StartCoroutine("MissClickKey", pressedKey);
    }
    

    IEnumerator ClickKey(GameObject key)
    {
        key.GetComponent<Image>().sprite = pressedKey;
        yield return new WaitForSeconds(0.3f);
        key.GetComponent<Image>().sprite = normalKey;
    }

    IEnumerator MissClickKey(GameObject key)
    {
        key.GetComponent<Image>().sprite = redKey;
        notes[countRightKey].GetComponent<Image>().color = new Color(255, 0, 0);
        yield return new WaitForSeconds(0.5f);
        key.GetComponent<Image>().sprite = normalKey;
        countRightKey = 0;
        for (int i = 0; i < notes.Length; i++)
            notes[i].GetComponent<Image>().color = new Color(0, 0, 0, 1);
        GetComponent<Animation>().Play();
        StartCoroutine("Sound");
    }

    IEnumerator Sound()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < soundsKey.Length; i++)
        {
            soundsKey[i].Play();
            yield return new WaitForSeconds(0.8f);
        }
    }
}
