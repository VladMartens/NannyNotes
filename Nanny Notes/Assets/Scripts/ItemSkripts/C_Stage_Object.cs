using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class C_Stage_Object : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public C_CursorManager.Cursors cursor;
    public int idHintStep;

    [Header("ForInventory")]
    public string idItem;
    public Sprite spriteForInventory;
    public string textForInventory;
    public bool pieceItem = false, pieItem = false, coinItem = false;

    [Header("TargetOption")]
    public string targetId;
    public GameObject[] ObjectsOn, ObjectsOff;
    public int numberStratCutscene = 0;

    public List<string> systemMassage;

    public bool succes;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (C_ToolBar_Manager.mc_this.blockclick.activeSelf == true)
            if (cursor == C_CursorManager.Cursors.set)
            {
                Cursor.visible = true;
                C_CursorManager.instance.UpdateTo(cursor);
            }
            else
            { return; }
        else
            C_CursorManager.instance.UpdateTo(cursor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        C_CursorManager.instance.UpdateToDefault();
        if (C_ToolBar_Manager.mc_this.blockclick.activeSelf == true)
            Cursor.visible = false;
    }

    public void GiveItem()
    {
        C_Take_Drop_Item.mc_this.GetItem();
        C_CursorManager.instance.UpdateToDefault();
        C_ToolBar_Manager.mc_this.ShowItem(this);
        C_Hint.mc_this.NextStep(idHintStep);
    }

    public void ClickTarget(bool needdragAndDrop_Item)
    {
        C_ToolBar_Manager.mc_this.blockclick.SetActive(false);
        if (needdragAndDrop_Item && C_ToolBar_Manager.mc_this.dragAndDrop_Item.gameObject.activeSelf == true && targetId == Drag_and_Drop.mc_init.idItem)
        {
            C_Hint.mc_this.NextStep(idHintStep);

            for (int i = 0; i < ObjectsOn.Length; i++)
                ObjectsOn[i].SetActive(true);
            for (int i = 0; i < ObjectsOff.Length; i++)
                ObjectsOff[i].SetActive(false);
            if (numberStratCutscene != 0)
                C_Cutscene_Controller.mc_this.StartCutscene(numberStratCutscene);
                        
            if(succes)
                C_Take_Drop_Item.mc_this.Sacces();
            else
                C_Take_Drop_Item.mc_this.PutItem();

            if (C_ToolBar_Manager.mc_this.inventoryList.GetComponentInChildren<C_PieItem>() != null && C_ToolBar_Manager.mc_this.inventoryList.GetComponentInChildren<C_PieItem>().idItem== targetId)
            {
                if (C_ToolBar_Manager.mc_this.inventoryList.GetComponentInChildren<C_PieItem>().text.text == "x3")
                {
                    C_ToolBar_Manager.mc_this.inventoryList.GetComponentInChildren<C_PieItem>().text.text = "x2";
                    C_ToolBar_Manager.mc_this.inventoryList.GetComponentInChildren<C_PieItem>().GetComponentInChildren<Image>().color = new Color(255, 255, 255, 1);
                   
                    for (int i = 0; i < C_ToolBar_Manager.mc_this.saveInventoryItems.Count; i++)
                        if (C_ToolBar_Manager.mc_this.saveInventoryItems[i].id == "15_00")
                            C_ToolBar_Manager.mc_this.saveInventoryItems[i].pie = 2;

                }
                else if (C_ToolBar_Manager.mc_this.inventoryList.GetComponentInChildren<C_PieItem>().text.text == "x2")
                {
                    C_ToolBar_Manager.mc_this.inventoryList.GetComponentInChildren<C_PieItem>().text.text = "x1";
                    C_ToolBar_Manager.mc_this.inventoryList.GetComponentInChildren<C_PieItem>().GetComponentInChildren<Image>().color = new Color(255, 255, 255, 1);
                    for (int i = 0; i < C_ToolBar_Manager.mc_this.saveInventoryItems.Count; i++)
                        if (C_ToolBar_Manager.mc_this.saveInventoryItems[i].id == "15_00")
                            C_ToolBar_Manager.mc_this.saveInventoryItems[i].pie = 1;
                }
                else if (C_ToolBar_Manager.mc_this.inventoryList.GetComponentInChildren<C_PieItem>().text.text == "x1")
                {
                    for (int i = 0; i < C_ToolBar_Manager.mc_this.saveInventoryItems.Count; i++)
                        if (C_ToolBar_Manager.mc_this.saveInventoryItems[i].id == "15_00")
                            C_ToolBar_Manager.mc_this.saveInventoryItems.Remove(C_ToolBar_Manager.mc_this.saveInventoryItems[i]);
                    DestroyImmediate(C_ToolBar_Manager.mc_this.inventoryList.GetComponentInChildren<C_PieItem>().gameObject);
                }
            }
            else
            {
                C_Item_In_Inventory[] inventory = C_ToolBar_Manager.mc_this.inventoryList.GetComponentsInChildren<C_Item_In_Inventory>();
                for (int i = 0; i < inventory.Length; i++)
                    if (inventory[i].idItem == targetId)
                    {
                        C_ToolBar_Manager.mc_this.saveInventoryItems.Remove(C_ToolBar_Manager.mc_this.saveInventoryItems[i]);
                        DestroyImmediate(inventory[i].gameObject);
                        break;
                    }
            }

            C_CursorManager.instance.UpdateToDefault();
            C_PlayerPrefs.mc_this.SaveInventoryItems(C_ToolBar_Manager.mc_this.saveInventoryItems);
            C_ToolBar_Manager.mc_this.dragAndDrop_Item.gameObject.SetActive(false);
        }
        else if (needdragAndDrop_Item && C_ToolBar_Manager.mc_this.dragAndDrop_Item.gameObject.activeSelf == false)
        {
            C_Take_Drop_Item.mc_this.NeedItem();
            if (systemMassage != null && systemMassage.Count != 0)
            {
                C_SystemMassage.mc_this.text.text = systemMassage[Random.Range(0, systemMassage.Count)];
                C_SystemMassage.mc_this.GetComponent<Animation>().Play("ActiveSystemMassage");
            }
        }
        else if (!needdragAndDrop_Item)
        {
            C_CursorManager.instance.UpdateToDefault();
            C_Hint.mc_this.NextStep(idHintStep);
            for (int i = 0; i < ObjectsOn.Length; i++)
                ObjectsOn[i].SetActive(true);
            for (int i = 0; i < ObjectsOff.Length; i++)
                ObjectsOff[i].SetActive(false);
            if (numberStratCutscene != 0)
                C_Cutscene_Controller.mc_this.StartCutscene(numberStratCutscene);
        }
        else
        {
            C_Take_Drop_Item.mc_this.ErrorItem();
            C_ToolBar_Manager.mc_this.MissClick();            
        }
            

       
        C_PlayerPrefs.mc_this.SaveSceneState();
    }
}