using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Task : MonoBehaviour
{
    public TaskElement[] taskElements;
    public Image next, back, imagePage, nameTask;
    public GameObject toDoList, animatedBook;

    private int numberTask, numberPage=0;
    public static C_Task mc_this;

    [System.Serializable]
    public class TaskElement
    {
        public Page[] pages;
        public Image taskComplete;
        public Image newTask;
    }

    [System.Serializable]
    public struct Page
    {
        public Sprite spritePage;
        public bool active;
    }

    private void Start()
    {
        mc_this = this;
        StartCoroutine("LoadTask");
    }

    public void Exit()
    {
        GetComponent<Animation>().Play("Close_Task");
        toDoList.SetActive(true);
        next.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
        imagePage.gameObject.SetActive(false);
        nameTask.gameObject.SetActive(true);
        numberPage = 0;

        StartCoroutine("SaveTask");
    }

    public void SelectTask(int numberTask)
    {
        taskElements[numberTask].newTask.gameObject.SetActive(false);
        this.numberTask = numberTask;
        nameTask.gameObject.SetActive(false);
        toDoList.SetActive(false);
        imagePage.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
        imagePage.sprite = taskElements[numberTask].pages[0].spritePage;
        numberPage = 0;
        if (taskElements[numberTask].pages.Length > 1 && taskElements[numberTask].pages[1].active)
            next.gameObject.SetActive(true);

        StartCoroutine("SaveTask");

        for (int i = 0; i < taskElements.Length; i++)
            if (taskElements[i].newTask.gameObject.activeSelf == true && taskElements[i].newTask.transform.parent.gameObject.activeSelf == true)
                return;
        animatedBook.SetActive(false);
    }

    public void Next()
    {       
        imagePage.sprite = taskElements[numberTask].pages[++numberPage].spritePage;
        if(taskElements[numberTask].pages.Length == numberPage + 1)
                next.gameObject.SetActive(false);
        else if (!taskElements[numberTask].pages[numberPage + 1].active)
            next.gameObject.SetActive(false);
    }
    public void Back()
    {
        if (numberPage == 0)
        {
            nameTask.gameObject.SetActive(true);
            next.gameObject.SetActive(false);
            back.gameObject.SetActive(false);
            imagePage.gameObject.SetActive(false);
            toDoList.SetActive(true);
        }
        else
        {
            imagePage.sprite = taskElements[numberTask].pages[--numberPage].spritePage;
            next.gameObject.SetActive(true);
        }
    }

    public void ActiveTask(int numberTask)
    {
        taskElements[numberTask].taskComplete.transform.parent.gameObject.SetActive(true);
        animatedBook.SetActive(true);

        StartCoroutine("SaveTask");
    }

    public void ActivetedImageTask(int numberTask)
    {
        for (int i = 0; i < taskElements[numberTask].pages.Length; i++)
            if (!taskElements[numberTask].pages[i].active)
            {
                taskElements[numberTask].pages[i].active = true;
                taskElements[numberTask].newTask.gameObject.SetActive(true);
                return;
            }
        animatedBook.SetActive(true);

        StartCoroutine("SaveTask");
    }

    public void Complete(int numberTask)
    {
        taskElements[numberTask].taskComplete.gameObject.SetActive(true);

        StartCoroutine("SaveTask");
    }

    IEnumerator SaveTask()
    {
        Dictionary<int, List<bool>> tas_kinfo = new Dictionary<int, List<bool>>();
        
        for (int i = 0; i < taskElements.Length; i++)
        {
            tas_kinfo.Add(i, new List<bool>());
            tas_kinfo[i].Add(taskElements[i].newTask.transform.parent.gameObject.activeSelf);
            tas_kinfo[i].Add(taskElements[i].newTask.gameObject.activeSelf);
            tas_kinfo[i].Add(taskElements[i].taskComplete.gameObject.activeSelf);

            for (int j = 0; j < taskElements[i].pages.Length; j++)
                tas_kinfo[i].Add(taskElements[i].pages[j].active);
        }
        C_PlayerPrefs.mc_this.SaveTask(tas_kinfo);
        yield return 0;
    }

    IEnumerator LoadTask()
    {
        Dictionary<int, List<bool>> tas_kinfo = C_PlayerPrefs.mc_this.LoadTask().taskinfo;
        if (tas_kinfo != null)
            for (int i = 0; i < taskElements.Length; i++)
            {
                int j = 0;

                taskElements[i].newTask.transform.parent.gameObject.SetActive(tas_kinfo[i][j++]);
                taskElements[i].newTask.gameObject.SetActive(tas_kinfo[i][j++]);

                if (taskElements[i].newTask.gameObject.activeSelf == true && taskElements[i].newTask.transform.parent.gameObject.activeSelf == true)
                    animatedBook.SetActive(true);

                taskElements[i].taskComplete.gameObject.SetActive(tas_kinfo[i][j++]);

                for (int x = 0; x < taskElements[i].pages.Length; x++)
                    taskElements[i].pages[x].active = tas_kinfo[i][j++];
            }
        yield return 0;
    }
}
