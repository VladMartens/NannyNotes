using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class C_PlayerPrefs : MonoBehaviour
{
    public float prefs_musicVolume,
        prefs_effectVolume,
        prefs_ambientVolume;

    public bool fullScreenMode;

    public string lastScene,
                  nickName;

    public C_HidenObjects.Hiden_Object[] hiden_Objects;

    public static C_PlayerPrefs mc_this;


    [Serializable]
    public class SaveInventoryItem
    {
        public string id;
        public string namePicture;
        public string nameItem;
        public int countPieceItem;
        public int pie;
    }

    [Serializable]
    public struct SceneState
    {
        public List<GameObject> hidenObject;
        public List<GameObject> stageObject;
    }

    [Serializable]
    public struct SceneStateSavedBool
    {
        public List<bool> activeHidenObject;
        public List<bool> activeStageObject;
        public C_HidenObjects.Hiden_Object[] hiden_Objects;

    }

    [Serializable]
    public struct GlobalSetting
    {
        public string lastScene;
    }


    [Serializable]
    public struct SaveStatusInMoreScene
    {
        public Dictionary<string, bool> statusInMoreScene;
    }

    [Serializable]
    public struct TaskInfo
    {
        public Dictionary<int, List<bool>> taskinfo;
    }

    private void Awake()
    {
        mc_this = this;
        Load_All();
    }

    private void Start()
    {
        lastScene = SceneManager.GetActiveScene().name;
        
    }

    public void Save_Options()
    {
        PlayerPrefs.SetFloat("_musicVolume", prefs_musicVolume);
        PlayerPrefs.SetFloat("_effectVolume", prefs_effectVolume);
        PlayerPrefs.SetFloat("_ambientVolume", prefs_ambientVolume);
        PlayerPrefs.SetInt("fullScreenMode", fullScreenMode ? 1 : 0);
    }

    public void Load_Options()
    {
        if (PlayerPrefs.HasKey("_musicVolume") ||
            PlayerPrefs.HasKey("_effectVolume") ||
            PlayerPrefs.HasKey("_ambientVolume") ||
            PlayerPrefs.HasKey("fullScreenMode"))
        {
            prefs_musicVolume = PlayerPrefs.GetFloat("_musicVolume");
            prefs_effectVolume = PlayerPrefs.GetFloat("_effectVolume");
            prefs_ambientVolume = PlayerPrefs.GetFloat("_ambientVolume");
            fullScreenMode = PlayerPrefs.GetInt("fullScreenMode") == 0 ? false : true;
        }
        else
        {
            prefs_musicVolume = 1;
            prefs_effectVolume = 1;
            prefs_ambientVolume = 1;
            fullScreenMode = false;
        }
    }

    public void Load_All()
    {
        if (PlayerPrefs.HasKey("NickName"))
            nickName = PlayerPrefs.GetString("NickName");
        Load_Options();
    }

    public void SaveInventoryItems(List<SaveInventoryItem> saveInventoryItems)
    {
        using (var file = File.OpenWrite(Application.streamingAssetsPath + "/Saved/Profile/" + nickName + "/Inventory.data"))
        {
            var writer = new BinaryFormatter();
            writer.Serialize(file, saveInventoryItems);
        }
    }

    public List<SaveInventoryItem> LoadInventoryItems()
    {
        if (File.Exists(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/Inventory.data"))
            using (var file = File.OpenRead(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/Inventory.data"))
            {
                var reader = new BinaryFormatter();
                List<SaveInventoryItem> data = (List<SaveInventoryItem>)reader.Deserialize(file);
                return data;
            }
        else
            return new List<SaveInventoryItem>();
    }

    public void SaveSceneState()
    {
        List<bool> activeHidenObject = new List<bool>(), activeStageObject = new List<bool>();

        for (int i = 0; i < С_Scene_Controller.mc_this.sceneState.hidenObject.Count; i++)
            activeHidenObject.Add(С_Scene_Controller.mc_this.sceneState.hidenObject[i].activeSelf);
        for (int i = 0; i < С_Scene_Controller.mc_this.sceneState.stageObject.Count; i++)
            activeStageObject.Add(С_Scene_Controller.mc_this.sceneState.stageObject[i].activeSelf);

        using (var file = File.OpenWrite(Application.streamingAssetsPath + "/Saved/Profile/" + nickName + "/" + SceneManager.GetActiveScene().name + ".data"))
        {
            var writer = new BinaryFormatter();
            writer.Serialize(file, new SceneStateSavedBool { activeHidenObject = activeHidenObject, activeStageObject = activeStageObject, hiden_Objects = C_HidenObjects.mc_this.hidenObjects });
        }
    }

    public SceneStateSavedBool LoadSceneState()
    {
        if (File.Exists(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/" + SceneManager.GetActiveScene().name + ".data"))
            using (var file = File.OpenRead(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/" + SceneManager.GetActiveScene().name + ".data"))
            {
                var reader = new BinaryFormatter();
                SceneStateSavedBool data = (SceneStateSavedBool)reader.Deserialize(file);
                hiden_Objects = data.hiden_Objects;
                return data;
            }
        else
            return new SceneStateSavedBool();
    }

    public void Save_NickName(string nickName)
    {
        this.nickName = nickName;
        PlayerPrefs.SetString("NickName", nickName);
    }

    public void SaveLastScene()
    {
        using (var file = File.OpenWrite(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/Global Setting.data"))
        {
            var writer = new BinaryFormatter();
            writer.Serialize(file, SceneManager.GetActiveScene().name);
        }
    }

    public void LoadLastScene()
    {
        if (File.Exists(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/Global Setting.data"))
            using (var file = File.OpenRead(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/Global Setting.data"))
            {
                var reader = new BinaryFormatter();
                string data = (string)reader.Deserialize(file);
                lastScene = data;
            }
        else
            lastScene = null;
    }

    public void SaveStatusScene(Dictionary<string, bool> statusInMoreScene)
    {
        using (var file = File.OpenWrite(Application.streamingAssetsPath + "/Saved/Profile/" + nickName + "/StatusInMoreScene.data"))
        {
            var writer = new BinaryFormatter();
            writer.Serialize(file, new SaveStatusInMoreScene { statusInMoreScene = statusInMoreScene });
        }
    }

    public SaveStatusInMoreScene LoadStatusScene()
    {
        if (File.Exists(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/" + "/StatusInMoreScene.data"))
            using (var file = File.OpenRead(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/StatusInMoreScene.data"))
            {
                var reader = new BinaryFormatter();
                SaveStatusInMoreScene data = (SaveStatusInMoreScene)reader.Deserialize(file);
                return data;
            }
        else
            return new SaveStatusInMoreScene();
    }

    public void SaveTask(Dictionary<int, List<bool>> saveInfo)
    {
        using (var file = File.OpenWrite(Application.streamingAssetsPath + "/Saved/Profile/" + nickName + "/Task.data"))
        {
            var writer = new BinaryFormatter();
            writer.Serialize(file, new TaskInfo { taskinfo = saveInfo });
        }
    }

    public TaskInfo LoadTask()
    {
        if (File.Exists(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/" + "/Task.data"))
            using (var file = File.OpenRead(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/Task.data"))
            {
                var reader = new BinaryFormatter();
                TaskInfo data = (TaskInfo)reader.Deserialize(file);
                return data;
            }
        else
            return new TaskInfo();
    }

    public void SaveHint(List<bool> saveHint)
    {
        using (var file = File.OpenWrite(Application.streamingAssetsPath + "/Saved/Profile/" + nickName + "/Hint.data"))
        {
            var writer = new BinaryFormatter();
            writer.Serialize(file, saveHint);
        }
    }

    public List<bool> LoadHint()
    {
        if (File.Exists(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/" + "/Hint.data"))
            using (var file = File.OpenRead(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/Hint.data"))
            {
                var reader = new BinaryFormatter();
                List<bool> data = (List<bool>)reader.Deserialize(file);
                return data;
            }
        else
            return new List<bool>();
    }

    public void SetHintStatus(float time)
    {
        PlayerPrefs.SetFloat("HintTime", time);
    }

    public float GetHintStatus()
    {
        return PlayerPrefs.GetFloat("HintTime");
    }
}
