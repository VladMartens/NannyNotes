using System.Collections;
using UnityEngine;

public class C_SoundController : MonoBehaviour
{
    public float musicVolume = 1,
       effectVolume = 1,
       ambientVolume = 1;

    public AudioSource[] music_Array,
         effect_Array,
         click_effect_Array,
         ambient_Array;

    public static C_SoundController mc_this;


    private void Start()
    {
        mc_this = this;
        ambientVolume = C_PlayerPrefs.mc_this.prefs_ambientVolume;
        effectVolume = C_PlayerPrefs.mc_this.prefs_effectVolume;
        musicVolume = C_PlayerPrefs.mc_this.prefs_musicVolume;

        StartCoroutine("AwaikScene");
    }

    public IEnumerator AwaikScene()
    {
        float step = musicVolume / 40f;
        for (int j = 0; j < music_Array.Length; j++)
            music_Array[j].volume = 0;
        for (int i = 0; i < 40; i++)
        {
            for (int j = 0; j < music_Array.Length; j++)
                music_Array[j].volume += step;

            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine("ChangeMusicVolume");
        StartCoroutine("ChangeEffectVolume");
        StartCoroutine("ChangeAmbientVolume");
        yield return 0;
    }

    public IEnumerator NextScene()
    {
        float step = musicVolume / 40f;
        for (int i =0;i< 40; i++)
        {
            for (int j = 0; j < music_Array.Length; j++)            
                music_Array[j].volume -= step;
            
            yield return new WaitForSeconds(0.01f);
        }

        yield return 0;
    }

    public IEnumerator ChangeMusicVolume()
    {
        for (int i = 0; i < music_Array.Length; i++)
            music_Array[i].volume = musicVolume;

        C_PlayerPrefs.mc_this.prefs_musicVolume = musicVolume;
        yield return 0;
    }

    public IEnumerator ChangeEffectVolume()
    {
        for (int i = 0; i < effect_Array.Length; i++)
            effect_Array[i].volume = effectVolume;

        for (int i = 0; i < click_effect_Array.Length; i++)
            click_effect_Array[i].volume = effectVolume;

        C_PlayerPrefs.mc_this.prefs_effectVolume = effectVolume;
       yield return 0;
    }

    public IEnumerator ChangeAmbientVolume()
    {
        for (int i = 0; i < ambient_Array.Length; i++)
            ambient_Array[i].volume = ambientVolume;

        C_PlayerPrefs.mc_this.prefs_ambientVolume = ambientVolume;
        yield return 0;
    }
}
