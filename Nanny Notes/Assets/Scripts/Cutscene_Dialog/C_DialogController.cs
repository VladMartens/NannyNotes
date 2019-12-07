using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_DialogController : MonoBehaviour
{
    public Animation animation;
    public GameObject DialogObeject;
    public Text textDialog;
    public GameObject nannyIcon, boyIcon, girlIcon, grannyIcon, granny2Icon;
    public float speedTyping;
   

    public static C_DialogController mc_this;

    private int numberDialogsInfo = 0,
                numberText = 0;
    private bool typing = false;
    private bool closed = true;

    private Dialog dialog;

    [System.Serializable]
    public class Dialog
    {
        public DialogInfo[] dialogsInfo;
        public string action_end;
        public C_Stage_Object giveItem;
    }

    [System.Serializable]
    public class DialogInfo
    {
        public Actors actorId;
        public string[] text;
    }

    public enum Actors
    {
        nanny = 0,
        boy = 1,
        girl = 2,
        granny = 3,
        granny2 = 4,
    }


    private void Awake()
    {
        mc_this = this;
    }        

    public IEnumerator StartDialog(Dialog dialogCutscene)
    {
        DialogObeject.SetActive(true);
        DialogObeject.GetComponent<Animation>().Play("DialogOpen");
        closed = false;
        dialog = dialogCutscene;
        numberDialogsInfo = 0;
        numberText = 0;
        yield return new WaitForSeconds(1f);
        if (!closed)
            StartCoroutine("Typing", dialog.dialogsInfo[numberDialogsInfo].text[numberText]);
    }

    public IEnumerator Typing(string text)
    {
        typing = true;
        nannyIcon.SetActive(false);
        boyIcon.SetActive(false);
        girlIcon.SetActive(false);
        grannyIcon.SetActive(false);
        granny2Icon.SetActive(false);
        textDialog.text = "";
        switch (dialog.dialogsInfo[numberDialogsInfo].actorId) {
            case Actors.nanny:
                {
                    nannyIcon.SetActive(true);
                    break;
                }
            case Actors.granny:
                {
                    grannyIcon.SetActive(true);
                    break;
                }
            case Actors.boy:
                {
                    boyIcon.SetActive(true);
                    break;
                }
            case Actors.girl:
                {
                    girlIcon.SetActive(true);
                    break;
                }
            case Actors.granny2:
                {
                    granny2Icon.SetActive(true);
                    break;
                }
        }       
        foreach (char letter in text.ToCharArray())
        {
            textDialog.text += letter;
            yield return new WaitForSeconds(speedTyping);
        }
        StartCoroutine("Timer",5);
        typing = false;
    }

    public void StopTyping_NextText()
    {
        if (typing)
        {
            StopCoroutine("Typing");
            textDialog.text = dialog.dialogsInfo[numberDialogsInfo].text[numberText];
            StartCoroutine("Timer",10);
            typing = false;
        }
        else
        {
            StopCoroutine("Timer");
            if (numberText < dialog.dialogsInfo[numberDialogsInfo].text.Length - 1)
                StartCoroutine("Typing", dialog.dialogsInfo[numberDialogsInfo].text[++numberText]);
            else
            {
                if (numberDialogsInfo < dialog.dialogsInfo.Length - 1)
                {
                    numberText = 0;
                    StartCoroutine("Typing", dialog.dialogsInfo[++numberDialogsInfo].text[numberText]);
                }
                else
                {
                    closed = true;
                    CloseDialog();
                }                   
            }
        }
    }

    IEnumerator Timer(float second)
    {
        yield return new WaitForSeconds(second);
        StopTyping_NextText();
    }

    public void CloseDialog()
    {
        StopCoroutine("Timer");
        if (typing)
            StopCoroutine("Typing");
        nannyIcon.SetActive(false);
        boyIcon.SetActive(false);
        girlIcon.SetActive(false);
        grannyIcon.SetActive(false);
        granny2Icon.SetActive(false);
        numberDialogsInfo = 0;
        numberText = 0;
        textDialog.text = "";
        DialogObeject.GetComponent<Animation>().Play("DialogClose");
        C_Cutscene_Controller.mc_this.Close();
        if (dialog.action_end != null)
        {
            switch (dialog.action_end)
            {
                case "start_tutorial":
                    C_Tutorial_Controller.mc_this.tutorial = true;
                    C_Tutorial_Controller.mc_this.StartAnimation("Step1Open");
                    break;
                case "start_animationStore":
                    animation.Play("Grannyleaves");
                    break;
                case "give_item":
                    C_ToolBar_Manager.mc_this.ShowItem(dialog.giveItem);
                    break;
                case "click_target":
                    dialog.giveItem.ClickTarget(false);
                    break;
                case "active_task3":
                    C_Task.mc_this.ActiveTask(3);
                    break;
                case "active_task4":
                    C_Task.mc_this.ActiveTask(4);
                    break;
                case "the_end":
                    С_Scene_Controller.mc_this.CloseScene("extro");
                    break;                    
            }
        }
        C_CursorManager.instance.UpdateToDefault();
        C_PlayerPrefs.mc_this.SaveSceneState();
    }
}
