using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class C_Profile : MonoBehaviour
{
    public GameObject input, selectedImage;
    public Text nameProfile, nameSelectedProfile;
    public Animation profileAnim;
    public C_ProfileItem[] profileItems;
    public C_ProfileItem selectProfile;

    private string nickName;


    private void Start()
    {
        StartProfile();
    }

    public void StartProfile()
    {
        nickName = C_PlayerPrefs.mc_this.nickName;
        nameProfile.text = "Welcome: " + nickName;
        if (nickName == "" || nickName == null)
            StartCoroutine("WaitEndAnim");
        else
        {
            int i = 0;
            DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/Saved/Profile");
            foreach (var item in dir.GetDirectories())
            {
                profileItems[i].nameProfile.text = item.Name;
                profileItems[i++].delete.SetActive(true);
            }                

            for (i = 0; i < profileItems.Length; i++)
                if (nickName == profileItems[i].nameProfile.text)
                {
                    selectedImage.SetActive(false);
                    profileItems[0].delete.SetActive(false);
                    profileItems[i].toggle.isOn = true;
                    profileItems[i].selectedImage.SetActive(true);
                    profileItems[i].delete.SetActive(true);
                    selectProfile = profileItems[i];
                    break;
                }
        }
    }
    
    public void ChangeText()
    {
        nameSelectedProfile.text = input.GetComponentInChildren<InputField>().text;
    }

    public void SelectProfile(C_ProfileItem selectedProfile)
    {
        nickName = selectedProfile.nameProfile.text;
        selectedImage.SetActive(false);
        selectedImage = selectedProfile.selectedImage;
        selectedProfile.delete.SetActive(true);
        nameSelectedProfile = selectedProfile.nameProfile;

        if (selectProfile.nameProfile.text == "")
            selectProfile.delete.SetActive(false);

        if (nameSelectedProfile.text != "")
            input.SetActive(false);
        else
            input.SetActive(true);

        selectProfile = selectedProfile;
    }

    public void Ok()
    {
        if (selectProfile.nameProfile.text != "")
        {
            input.SetActive(false);
            nickName = selectProfile.nameProfile.text;
            C_PlayerPrefs.mc_this.Save_NickName(nickName);
            nameProfile.text = "Welcome: " + nickName;
            profileAnim.Play("CloseProfile");
            if (!Directory.Exists(Application.streamingAssetsPath + "/Saved/Profile/" + nickName))
                Directory.CreateDirectory(Application.streamingAssetsPath + "/Saved/Profile/" + nickName);

            C_PlayerPrefs.mc_this.LoadLastScene();
        }
    }

    public void Cancel()
    {
        if (C_PlayerPrefs.mc_this.nickName != "") 
            profileAnim.Play("CloseProfile");
        //string temp_NickName = C_PlayerPrefs.mc_this.nickName;
        //if (Directory.Exists(Application.streamingAssetsPath + "/Saved/Profile/" + temp_NickName))
        //{
        //    nickName = C_PlayerPrefs.mc_this.nickName;
        //    for (int i = 0; i < profileItems.Length; i++)
        //        if (profileItems[i].nameProfile.text == nickName)
        //            SelectProfile(profileItems[i]);
        //}           
    }

    public void Delete(Text nameProfile)
    {
        if (Directory.Exists(Application.streamingAssetsPath + "/Saved/Profile/" + nameProfile.text) && nameProfile.text != "")
            Directory.Delete(Application.streamingAssetsPath + "/Saved/Profile/" + nameProfile.text, true);
        nameProfile.text = "";
        if (selectProfile.nameProfile.text == "")
            input.SetActive(true);
    }

    IEnumerator WaitEndAnim()
    {
        while (profileAnim.isPlaying)
            yield return new WaitForSeconds(0.1f);

        profileAnim.Play("OpenProfile");
        input.SetActive(true);
    }
}
