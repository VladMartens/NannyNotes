using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class С_Scene_Controller : MonoBehaviour
{
    public static С_Scene_Controller mc_this;
    public Animation animationCloseScene;
    public string nameNextScene;
    public C_PlayerPrefs.SceneState sceneState;

    private void Start()
    {
        mc_this = this;

        if (SceneManager.GetActiveScene().name != "Preloader" && SceneManager.GetActiveScene().name != "MainMenu") 
            C_PlayerPrefs.mc_this.SaveLastScene();
        else
            C_PlayerPrefs.mc_this.LoadLastScene();

        if (File.Exists(Application.streamingAssetsPath + "/Saved/Profile/" + PlayerPrefs.GetString("NickName") + "/" + SceneManager.GetActiveScene().name + ".data"))
        {
            C_PlayerPrefs.SceneStateSavedBool loadScene = C_PlayerPrefs.mc_this.LoadSceneState();
            StartCoroutine("LoadScene", loadScene);
        }
        else
            switch (SceneManager.GetActiveScene().name)
            {
                case "DinDon":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;
                case "Hall":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;
                case "I1":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;
                case "balcony":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;
                case "grannyRoom":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;
                case "nanyRoom":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;
                case "parentroom":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;
                case "boyroom":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;
                case "girlRoom":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;
                case "guestroom":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;
                case "kitchen":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;
                case "chicks":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;
                case "store":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;
                case "barn":
                    C_Cutscene_Controller.mc_this.StartCutscene(0);
                    break;                    
            }         
    }

    public void OnOff_Object(GameObject gameObjectOn)
    {
        if (!gameObjectOn.activeSelf)
            gameObjectOn.SetActive(true);
        else
            gameObjectOn.SetActive(false);
        C_CursorManager.instance.UpdateToDefault();
    }

    public void offObject(GameObject gameObjectOn)
    {
        gameObjectOn.SetActive(false);
        C_CursorManager.instance.UpdateToDefault();
    }
    
    public void StartScene()
    {
        SceneManager.LoadScene(nameNextScene);
    }

    public void StartScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void CloseScene(string nameNextScene)
    {
        C_SoundController.mc_this.StartCoroutine("NextScene");
        if (C_Hint.mc_this != null && C_Hint.mc_this.fillAmount != 0)
            C_PlayerPrefs.mc_this.SetHintStatus(C_Hint.mc_this.fillAmount);
        this.nameNextScene = nameNextScene;
        animationCloseScene.Play("CloseScene");
    }

    IEnumerator LoadScene(C_PlayerPrefs.SceneStateSavedBool sceneStateBool)
    {
        for (int i = 0; i < sceneStateBool.activeHidenObject.Count; i++)
            sceneState.hidenObject[i].SetActive(sceneStateBool.activeHidenObject[i]);

        for (int i = 0; i < sceneStateBool.activeStageObject.Count; i++)
            sceneState.stageObject[i].SetActive(sceneStateBool.activeStageObject[i]);
        yield return 0;
    }
}