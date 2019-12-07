using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class C_EnterMouese : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audio;

    public void OnPointerEnter(PointerEventData eventData)
    {
        audio.Play();
    }
}
