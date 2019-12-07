using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class C_Hint : MonoBehaviour
{
    //public Dictionary<int, List<Hint>> hints;
    public List<Hint> hints;
    public Image get, set, search, talk, up, down, left, right;
    public float timeShowHint = 3f;
    public float timeReloadHint = 180f;
    public int numberHint = 0;
    public static C_Hint mc_this;
    public NextLevel[] allLevel;

    public float fillAmount;
    public Image activeHint, lockOpened, lockClosed;
    public Animation finishReload, activated;
    public AudioSource openLock;

    
    public List<SkipObjects> skips;

    List<bool> saveHint;

    List<string> myWay;

    [System.Serializable]
    public struct NextLevel
    {
        public string nameNextLevel;
        public GameObject arrow;
    }
    [System.Serializable]
    public struct SkipObjects
    {
        public List<GameObject> onList, offList;
    }

    [System.Serializable]
    public class Hint
    {
        public C_CursorManager.Cursors typeCursorOne, typeCursorTwo, cursorsZoom;
        public Vector3 positionCursorOne, positionCursorTwo, positionCursorZoom;
        public string idNeedItemInInventory;
        public bool acive = true, zoomActive = false;
        public string nameScene;
    }
   
    string[] Hall = new string[] { "Hall" };
    string[] I1 = new string[] { "Hall", "I1" };
    string[] I2 = new string[] { "Hall", "I1", "I2" };
    string[] I3 = new string[] { "Hall", "I1", "I2", "I3" };
    string[] parentroom = new string[] { "Hall", "I1", "I2", "I3", "parentroom" };
    string[] girlRoom = new string[] { "Hall", "I1", "I2", "girlRoom" };
    string[] balcony = new string[] { "Hall", "I1", "I2", "balcony" };
    string[] nanyRoom = new string[] { "Hall", "I1", "nanyRoom" };
    string[] boyroom = new string[] { "Hall", "I1", "boyroom" };
    string[] attic = new string[] { "Hall", "I1", "attic" };
    string[] grannyRoom = new string[] { "Hall", "grannyRoom" };
    string[] closet = new string[] { "Hall", "grannyRoom", "closet" };
    string[] guestroom = new string[] { "Hall", "guestroom" };
    string[] kitchen = new string[] { "Hall", "guestroom", "kitchen" };
    string[] bath = new string[] { "Hall", "guestroom", "kitchen", "bath" };
    string[] lawn = new string[] { "Hall", "guestroom", "kitchen", "lawn" };
    string[] barn = new string[] { "Hall", "guestroom", "kitchen", "lawn", "barn" };
    string[] zlawn = new string[] { "Hall", "guestroom", "kitchen", "lawn", "zlawn" };
    string[] behind_barn = new string[] { "Hall", "guestroom", "kitchen", "lawn", "zlawn", "behind_barn" };
    string[] chicks = new string[] { "Hall", "guestroom", "kitchen", "lawn", "zlawn", "behind_barn", "chicks" };
    string[] behind_barn_zfence = new string[] { "Hall", "guestroom", "kitchen", "lawn", "zlawn", "behind_barn", "behind_barn_zfence" };
    string[] storeplace = new string[] { "Hall", "guestroom", "kitchen", "lawn", "zlawn", "storeplace" };
    string[] store = new string[] { "Hall", "guestroom", "kitchen", "lawn", "zlawn", "storeplace", "store" };
    string[] bdhouse = new string[] { "Hall", "guestroom", "kitchen", "lawn", "zlawn", "bdhouse" };
    string[] arbor = new string[] { "Hall", "guestroom", "kitchen", "lawn", "zlawn", "bdhouse", "arbor" };
    string[] cellar = new string[] { "Hall", "cellar" };
    string[] myPosition;
    string[] myTarget;

    private void Start()
    {
        mc_this = this;
        StartCoroutine("LoadHint");
    }

    public void FindAWay()
    {
        myWay = new List<string>();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Hall":
                myPosition = Hall;
                break;
            case "I1":
                myPosition = I1;
                break;
            case "I2":
                myPosition = I2;
                break;
            case "I3":
                myPosition = I3;
                break;
            case "parentroom":
                myPosition = parentroom;
                break;
            case "girlRoom":
                myPosition = girlRoom;
                break;
            case "balcony":
                myPosition = balcony;
                break;
            case "nanyRoom":
                myPosition = nanyRoom;
                break;
            case "boyroom":
                myPosition = boyroom;
                break;
            case "attic":
                myPosition = attic;
                break;
            case "grannyRoom":
                myPosition = grannyRoom;
                break;
            case "closet":
                myPosition = closet;
                break;
            case "guestroom":
                myPosition = guestroom;
                break;
            case "kitchen":
                myPosition = kitchen;
                break;
            case "bath":
                myPosition = bath;
                break;
            case "lawn":
                myPosition = lawn;
                break;
            case "barn":
                myPosition = barn;
                break;
            case "zlawn":
                myPosition = zlawn;
                break;
            case "behind_barn":
                myPosition = behind_barn;
                break;
            case "chicks":
                myPosition = chicks;
                break;
            case "behind_barn_zfence":
                myPosition = behind_barn_zfence;
                break;
            case "storeplace":
                myPosition = storeplace;
                break;
            case "store":
                myPosition = store;
                break;
            case "bdhouse":
                myPosition = bdhouse;
                break;
            case "arbor":
                myPosition = arbor;
                break;
            case "cellar":
                myPosition = cellar;
                break;
        }
        switch (hints[numberHint].nameScene)
        {
            case "Hall":
                myTarget = Hall;
                break;
            case "I1":
                myTarget = I1;
                break;
            case "I2":
                myTarget = I2;
                break;
            case "I3":
                myTarget = I3;
                break;
            case "parentroom":
                myTarget = parentroom;
                break;
            case "girlRoom":
                myTarget = girlRoom;
                break;
            case "balcony":
                myTarget = balcony;
                break;
            case "nanyRoom":
                myTarget = nanyRoom;
                break;
            case "boyroom":
                myTarget = boyroom;
                break;
            case "attic":
                myTarget = attic;
                break;
            case "grannyRoom":
                myTarget = grannyRoom;
                break;
            case "closet":
                myTarget = closet;
                break;
            case "guestroom":
                myTarget = guestroom;
                break;
            case "kitchen":
                myTarget = kitchen;
                break;
            case "bath":
                myTarget = bath;
                break;
            case "lawn":
                myTarget = lawn;
                break;
            case "barn":
                myTarget = barn;
                break;
            case "zlawn":
                myTarget = zlawn;
                break;
            case "behind_barn":
                myTarget = behind_barn;
                break;
            case "chicks":
                myTarget = chicks;
                break;
            case "behind_barn_zfence":
                myTarget = behind_barn_zfence;
                break;
            case "storeplace":
                myTarget = storeplace;
                break;
            case "store":
                myTarget = store;
                break;
            case "bdhouse":
                myTarget = bdhouse;
                break;
            case "arbor":
                myTarget = arbor;
                break;
            case "cellar":
                myTarget = cellar;
                break;
        }

        int i = 0;
        while (true)
        {
            if (myPosition.Length > i && myTarget.Length > i)
            {
                if (myPosition[i] == myTarget[i])
                    i++;
                else
                {
                    for (int j = myPosition.Length; j >= i - 1; j--)
                    {
                        myWay.Add(myPosition[j]);
                    }
                    for (int j = myTarget.Length; j > i; j--)
                    {
                        myWay.Add(myTarget[j]);
                    }
                    break;
                }
            }
            else
            {
                for (int j = myPosition.Length - 2; j >= i - 1; j--)
                    myWay.Add(myPosition[j]);
                for (int j = i; j < myTarget.Length - 1; j++)
                    myWay.Add(myTarget[j]);

                for (int j = 0; j < allLevel.Length ; j++)
                    if (allLevel[j].nameNextLevel == myWay[0])
                    {
                        allLevel[j].arrow.GetComponent<Animation>().Play("Active");
                        StartCoroutine("TimerHint", allLevel[j].arrow.GetComponent<Animation>());
                        break;
                    }
                break;
            }
        }

    }

    public void ActiveHint()
    {
        //StartCoroutine("DiactiveHint");
        if (!hints[numberHint].acive)
            while (!hints[numberHint].acive)
                numberHint++;

        if (hints[numberHint].nameScene != SceneManager.GetActiveScene().name)
        {
            FindAWay();
            return;
        }

        if (hints[numberHint].cursorsZoom != C_CursorManager.Cursors.defaultCursor && hints[numberHint].zoomActive == false)
        {
            search.rectTransform.localPosition = hints[numberHint].positionCursorZoom;
            search.gameObject.SetActive(true);
            GetComponent<Animation>().Play("ActiveSearch");
            StartCoroutine("TimerShowHint");
            return;
        }
        fillAmount = 0;
        StartCoroutine("ReloadHint");
        activated.Play("Activated");
        switch (hints[numberHint].typeCursorOne)
        {
            case C_CursorManager.Cursors.get:
                if (hints[numberHint].idNeedItemInInventory != "")
                {
                    C_Item_In_Inventory[] inventory = C_ToolBar_Manager.mc_this.inventory_transform.GetComponentsInChildren<C_Item_In_Inventory>();
                    C_PieItem pie = C_ToolBar_Manager.mc_this.inventory_transform.GetComponentInChildren<C_PieItem>();
                    for (int i = 0; i < inventory.Length; i++)
                        if (inventory[i].idItem == hints[numberHint].idNeedItemInInventory)
                        {
                            get.transform.position = new Vector3(inventory[i].transform.position.x + 0.13f, inventory[i].transform.position.y + 0.09f, 0);
                            break;
                        }
                    if (pie != null && pie.idItem == hints[numberHint].idNeedItemInInventory)
                        get.transform.position = new Vector3(pie.transform.position.x + 0.13f, pie.transform.position.y + 0.09f, 0);
                }
                else
                {
                    get.rectTransform.localPosition = hints[numberHint].positionCursorOne;
                }


                get.gameObject.SetActive(true);
                GetComponent<Animation>().Play("ActiveGet");
                break;

            case C_CursorManager.Cursors.set:
                set.rectTransform.localPosition = hints[numberHint].positionCursorOne;
                set.gameObject.SetActive(true);
                GetComponent<Animation>().Play("ActiveSet");
                break;

            case C_CursorManager.Cursors.search:
                search.rectTransform.localPosition = hints[numberHint].positionCursorOne;
                search.gameObject.SetActive(true);
                GetComponent<Animation>().Play("ActiveSearch");
                break;

            case C_CursorManager.Cursors.talk:
                talk.rectTransform.localPosition = hints[numberHint].positionCursorOne;
                talk.gameObject.SetActive(true);
                GetComponent<Animation>().Play("ActiveTalk");
                break;

            case C_CursorManager.Cursors.up:
                up.rectTransform.localPosition = hints[numberHint].positionCursorOne;
                up.gameObject.SetActive(true);
                GetComponent<Animation>().Play("ActiveUp");
                break;

            case C_CursorManager.Cursors.down:
                down.rectTransform.localPosition = hints[numberHint].positionCursorOne;
                down.gameObject.SetActive(true);
                GetComponent<Animation>().Play("ActiveDown");
                break;

            case C_CursorManager.Cursors.left:
                left.rectTransform.localPosition = hints[numberHint].positionCursorOne;
                left.gameObject.SetActive(true);
                GetComponent<Animation>().Play("ActiveLeft");
                break;

            case C_CursorManager.Cursors.right:
                right.rectTransform.localPosition = hints[numberHint].positionCursorOne;
                right.gameObject.SetActive(true);
                GetComponent<Animation>().Play("ActiveRight");
                break;
        }

        switch (hints[numberHint].typeCursorTwo)
        {
            case C_CursorManager.Cursors.get:
                get.rectTransform.localPosition = hints[numberHint].positionCursorTwo;
                get.gameObject.SetActive(true);
                StartCoroutine("StartAnimation", "ActiveGet");
                break;

            case C_CursorManager.Cursors.set:
                set.rectTransform.localPosition = hints[numberHint].positionCursorTwo;
                set.gameObject.SetActive(true);
                StartCoroutine("StartAnimation", "ActiveSet");
                break;

            case C_CursorManager.Cursors.search:
                search.rectTransform.localPosition = hints[numberHint].positionCursorTwo;
                search.gameObject.SetActive(true);
                StartCoroutine("StartAnimation", "ActiveSearch");
                break;

            case C_CursorManager.Cursors.talk:
                talk.rectTransform.localPosition = hints[numberHint].positionCursorTwo;
                talk.gameObject.SetActive(true);
                StartCoroutine("StartAnimation", "ActiveTalk");
                break;

            case C_CursorManager.Cursors.up:
                up.rectTransform.localPosition = hints[numberHint].positionCursorTwo;
                up.gameObject.SetActive(true);
                StartCoroutine("StartAnimation", "ActiveUp");
                break;

            case C_CursorManager.Cursors.down:
                down.rectTransform.localPosition = hints[numberHint].positionCursorTwo;
                down.gameObject.SetActive(true);
                StartCoroutine("StartAnimation", "ActiveDown");
                break;

            case C_CursorManager.Cursors.left:
                left.rectTransform.localPosition = hints[numberHint].positionCursorTwo;
                left.gameObject.SetActive(true);
                StartCoroutine("StartAnimation", "ActiveLeft");
                break;

            case C_CursorManager.Cursors.right:
                right.rectTransform.localPosition = hints[numberHint].positionCursorTwo;
                right.gameObject.SetActive(true);
                StartCoroutine("StartAnimation", "ActiveRight");
                break;
        }
        StartCoroutine("TimerShowHint");
    }

    public void NextStep(int idHint)
    {
        hints[idHint].acive = false;
        StartCoroutine("SaveHint");
    }

    public void NextStepNoSave(int idHint)
    {
        hints[idHint].acive = false;
    }

    public void Save()
    {
        StartCoroutine("SaveHint");
    }

    public void ZoomActive(int idHint)
    {
        hints[idHint].zoomActive = true;
    }

    public void ZoomDiactive(int idHint)
    {
        hints[idHint].zoomActive = false;
    }

    public void ActiveSkip(int number)
    {
        foreach (var item in skips[number].onList)
            item.SetActive(true);
        foreach (var item in skips[number].offList)
            item.SetActive(false);
        C_PlayerPrefs.mc_this.SaveSceneState();
    }



    IEnumerator TimerShowHint()
    {
        yield return new WaitForSeconds(timeShowHint);
        if (hints[numberHint].cursorsZoom != C_CursorManager.Cursors.defaultCursor && hints[numberHint].zoomActive == false)
        {
            GetComponent<Animation>().Play("DiactiveSearch");
            yield return 0;
        }
            

        else
        {
            switch (hints[numberHint].typeCursorOne)
            {
                case C_CursorManager.Cursors.get:
                    GetComponent<Animation>().Play("DiactiveGet");
                    break;

                case C_CursorManager.Cursors.set:
                    GetComponent<Animation>().Play("DiactiveSet");
                    break;

                case C_CursorManager.Cursors.search:
                    GetComponent<Animation>().Play("DiactiveSearch");
                    break;

                case C_CursorManager.Cursors.talk:
                    GetComponent<Animation>().Play("DiactiveTalk");
                    break;

                case C_CursorManager.Cursors.up:
                    GetComponent<Animation>().Play("DiactiveUp");
                    break;

                case C_CursorManager.Cursors.down:
                    GetComponent<Animation>().Play("DiactiveDown");
                    break;

                case C_CursorManager.Cursors.left:
                    GetComponent<Animation>().Play("DiactiveLeft");
                    break;

                case C_CursorManager.Cursors.right:
                    GetComponent<Animation>().Play("DiactiveRight");
                    break;
            }

            switch (hints[numberHint].typeCursorTwo)
            {
                case C_CursorManager.Cursors.get:
                    StartCoroutine("StartAnimation", "DiactiveGet");
                    break;

                case C_CursorManager.Cursors.set:
                    StartCoroutine("StartAnimation", "DiactiveSet");
                    break;

                case C_CursorManager.Cursors.search:
                    StartCoroutine("StartAnimation", "DiactiveSearch");
                    break;

                case C_CursorManager.Cursors.talk:
                    StartCoroutine("StartAnimation", "DiactiveTalk");
                    break;

                case C_CursorManager.Cursors.up:
                    StartCoroutine("StartAnimation", "DiactiveUp");
                    break;

                case C_CursorManager.Cursors.down:
                    StartCoroutine("StartAnimation", "DiactiveDown");
                    break;

                case C_CursorManager.Cursors.left:
                    StartCoroutine("StartAnimation", "DiactiveLeft");
                    break;

                case C_CursorManager.Cursors.right:
                    StartCoroutine("StartAnimation", "DiactiveRight");
                    break;
            }
        }
    }

    IEnumerator TimerHint(Animation animation)
    {
        yield return new WaitForSeconds(timeShowHint);
        animation.Play("Diactive");

    }

    IEnumerator StartAnimation(string nameAnim)
    {
        while (GetComponent<Animation>().isPlaying)
            yield return new WaitForSeconds(0.1f);
        GetComponent<Animation>().Play(nameAnim);
    }

    IEnumerator SaveHint()
    {
        saveHint = new List<bool>();
        for (int i = 0; i < hints.Count; i++)
            saveHint.Add(hints[i].acive);
        C_PlayerPrefs.mc_this.SaveHint(saveHint);
        yield return 0;
    }

    IEnumerator LoadHint()
    {
        saveHint = C_PlayerPrefs.mc_this.LoadHint();
        if (saveHint.Count != 0)
            for (int i = 0; i < hints.Count; i++)
                hints[i].acive = saveHint[i];
        fillAmount = C_PlayerPrefs.mc_this.GetHintStatus();
        if (fillAmount != 0 && fillAmount < 1)
            StartCoroutine("ReloadHint");
        else
            fillAmount = 1;
        yield return 0;
    }

    IEnumerator ReloadHint()
    {        
        activeHint.raycastTarget = false;
        lockOpened.gameObject.SetActive(false);
        lockClosed.gameObject.SetActive(true);
        //yield return new WaitForSeconds(1.5f);
        float step = 0.01f / timeReloadHint;
        while (true)
        {
            fillAmount += step;
            activeHint.fillAmount = fillAmount;
            if (activeHint.fillAmount >= 1)
            {                
                lockOpened.gameObject.SetActive(true);
                openLock.Play();
                lockClosed.gameObject.SetActive(false);
                activeHint.raycastTarget = true;
                finishReload.Play("Finish");
                C_PlayerPrefs.mc_this.SetHintStatus(1);
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        yield return 0;
    }
    
    IEnumerator DiactiveHint()
    {
        activeHint.raycastTarget = false;
            yield return new WaitForSeconds(3.5f);
        activeHint.raycastTarget = true;
        yield return 0;
    }

    private void OnApplicationQuit()
    {
        C_PlayerPrefs.mc_this.SetHintStatus(fillAmount);
    }
}
