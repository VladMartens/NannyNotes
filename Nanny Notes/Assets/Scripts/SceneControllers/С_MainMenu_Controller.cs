using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class С_MainMenu_Controller : MonoBehaviour
{
    [Header("Options")]
    public Slider SoundAmbient, SoundFx, SoundMusic;
    public Toggle fullScreenMode;

    private void Start()
    {
        SoundAmbient.value = C_PlayerPrefs.mc_this.prefs_ambientVolume;
        SoundFx.value = C_PlayerPrefs.mc_this.prefs_effectVolume;
        SoundMusic.value = C_PlayerPrefs.mc_this.prefs_musicVolume;
    }

    public void StartAnimation(string nameAnimation)
    {
        GetComponent<Animation>().Play(nameAnimation);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        C_PlayerPrefs.mc_this.LoadLastScene();
        if (C_PlayerPrefs.mc_this.lastScene!=null && C_PlayerPrefs.mc_this.lastScene != "")
            С_Scene_Controller.mc_this.StartScene(C_PlayerPrefs.mc_this.lastScene);
        else        
            С_Scene_Controller.mc_this.StartScene("Intro_Book");               
    }

    public void ResetOptions()
    {
        C_PlayerPrefs.mc_this.Load_Options();
        Screen.fullScreen = C_PlayerPrefs.mc_this.fullScreenMode;
        fullScreenMode.isOn = C_PlayerPrefs.mc_this.fullScreenMode;
        SoundAmbient.value = C_PlayerPrefs.mc_this.prefs_ambientVolume;
        SoundFx.value = C_PlayerPrefs.mc_this.prefs_effectVolume;
        SoundMusic.value = C_PlayerPrefs.mc_this.prefs_musicVolume;

        UpdateSoundAmbient();
        UpdateSoundFx();
        UpdateSoundMusic();
    }

    public void ApplyOptions()
    {
        C_PlayerPrefs.mc_this.fullScreenMode = fullScreenMode.isOn;
        C_PlayerPrefs.mc_this.Save_Options();
    }


    public void UpValue(string nameSound)
    {
        switch (nameSound)
        {
            case "Ambient":
                if (SoundAmbient.value + 0.2f > 1)
                    SoundAmbient.value = 1;
                else SoundAmbient.value += 0.2f;
                UpdateSoundAmbient();
                break;

            case "Effect":
                if (SoundFx.value + 0.2f > 1)
                    SoundFx.value = 1;
                else SoundFx.value += 0.2f;
                UpdateSoundFx();
                break;

            case "Music":
                if (SoundMusic.value + 0.2f > 1)
                    SoundMusic.value = 1;
                else SoundMusic.value += 0.2f;
                UpdateSoundMusic();
                break;
        }
    }

    public void DownValue(string nameSound)
    {
        switch (nameSound)
        {
            case "Ambient":
                if (SoundAmbient.value - 0.2f < 0)
                    SoundAmbient.value = 0;
                else SoundAmbient.value -= 0.2f;
                UpdateSoundAmbient();
                break;

            case "Effect":
                if (SoundFx.value - 0.2f < 0)
                    SoundFx.value = 0;
                else SoundFx.value -= 0.2f;
                UpdateSoundFx();
                break;

            case "Music":
                if (SoundMusic.value - 0.2f < 0)
                    SoundMusic.value = 0;
                else SoundMusic.value -= 0.2f;
                UpdateSoundMusic();
                break;
        }
    }

    public void UpdateSoundAmbient()
    {
        if (C_SoundController.mc_this != null)
        {
            C_SoundController.mc_this.ambientVolume = SoundAmbient.value;
            C_SoundController.mc_this.StartCoroutine("ChangeAmbientVolume");
        }
    }

    public void UpdateSoundFx()
    {
        if (C_SoundController.mc_this != null)
        {
            C_SoundController.mc_this.effectVolume = SoundFx.value;
            C_SoundController.mc_this.StartCoroutine("ChangeEffectVolume");
        }
    }

    public void UpdateSoundMusic()
    {
        if (C_SoundController.mc_this != null)
        {
            C_SoundController.mc_this.musicVolume = SoundMusic.value;
            C_SoundController.mc_this.StartCoroutine("ChangeMusicVolume");
        }
    }

    public void UpdateFUllScreen()
    {
        Screen.fullScreen = fullScreenMode.isOn;
    }

    public void CleanSetting()
    {
        PlayerPrefs.DeleteAll();
        C_PlayerPrefs.mc_this.Load_All();
        SoundAmbient.value = 1;
        SoundFx.value = 1;
        SoundMusic.value = 1;
        UpdateSoundAmbient();
        UpdateSoundFx();
        UpdateSoundMusic();
    }
}
