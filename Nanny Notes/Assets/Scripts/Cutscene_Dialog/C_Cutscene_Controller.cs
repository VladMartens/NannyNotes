using UnityEngine;

public class C_Cutscene_Controller : MonoBehaviour
{
    public C_DialogController.Dialog[] CutscenesInLevel;

    public static C_Cutscene_Controller mc_this;

    private int lastCatscene;

    [System.Serializable]
    public class EventInfo
    {
        public string eventName;
        public int eventCounter;
    }

    private void Awake()
    {
        mc_this = this;
    }

    public void StartCutscene(int numberCutscene)
    {
        lastCatscene = numberCutscene;
        GetComponent<Animation>().Play("Cutscene" + ++numberCutscene);       
    }

    public void OpenDialog()
    {
        C_DialogController.mc_this.StartCoroutine("StartDialog", (CutscenesInLevel[lastCatscene]));
    }

    public void Close()
    {
        GetComponent<Animation>().Play("Cutscene" + ++lastCatscene+"Close");
    }
}
