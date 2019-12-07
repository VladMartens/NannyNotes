using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ToolBar_Manager : MonoBehaviour
{
    [Header("Swith Inventory")]
    public Toggle inventory, items;
    public GameObject hidenGameObject;

    [Header("Create Items")]
    public Transform inventory_transform;
    public GameObject prefab_Item, prefab_pieceItem, prefab_pieItem, DragAndDropItem, prefab_coinItem;
    

    [Header("ShowItem")]
    public Image imageShowItem;
    public Text nameObject;
    public bool pieceItem = false;
    public bool pieItem = false;
    public bool coinItem = false;
    [System.NonSerialized]
    public string idItem;

    [Header("Drag and Drop")]
    public Drag_and_Drop dragAndDrop_Item;
    public GameObject inventoryList;
    public GameObject blockclick;

    [System.NonSerialized]
    public static C_ToolBar_Manager mc_this;


    public List<C_PlayerPrefs.SaveInventoryItem> saveInventoryItems = new List<C_PlayerPrefs.SaveInventoryItem>();

    private C_Stage_Object stage_Object;

    private void Start()
    {
        mc_this = this;
        LoadInventory();
    }

    void LoadInventory()
    {
        saveInventoryItems = C_PlayerPrefs.mc_this.LoadInventoryItems();
        GameObject newObject;

        for (int i = 0; i < saveInventoryItems.Count; i++)
        {
            if (saveInventoryItems[i].countPieceItem != 0)
            {
                newObject = Instantiate(prefab_pieceItem, inventory_transform);
                newObject.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("InventoryImage/" + saveInventoryItems[i].namePicture);
                newObject.GetComponentInChildren<Image>().color = new Color(255, 255, 255, 1);
                newObject.GetComponent<C_PieceItem>().idItem = saveInventoryItems[i].id;
                newObject.GetComponent<C_PieceItem>().text.text = "(" + saveInventoryItems[i].countPieceItem + "/3)";
                newObject.GetComponentInChildren<Text>().text = saveInventoryItems[i].nameItem;
            }
            else if(saveInventoryItems[i].pie != 0)
            {
                newObject = Instantiate(prefab_pieItem, inventory_transform);
                newObject.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("InventoryImage/" + saveInventoryItems[i].namePicture);
                newObject.GetComponentInChildren<Image>().color = new Color(255, 255, 255, 1);
                newObject.GetComponent<C_PieItem>().idItem = saveInventoryItems[i].id;
                newObject.GetComponent<C_PieItem>().text.text = "x"+ saveInventoryItems[i].pie;
                newObject.GetComponentInChildren<Text>().text = saveInventoryItems[i].nameItem;
            }      
            else
            {
                newObject = Instantiate(prefab_Item, inventory_transform);
                newObject.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("InventoryImage/" + saveInventoryItems[i].namePicture);
                newObject.GetComponentInChildren<Image>().color = new Color(255, 255, 255, 1);
                newObject.GetComponent<C_Item_In_Inventory>().idItem = saveInventoryItems[i].id;
                newObject.GetComponentInChildren<Text>().text = saveInventoryItems[i].nameItem;
            }           
        }
    }

    public void Show_Items_Inventory()
    {
        if (inventory.GetComponent<Toggle>().isOn == true)
            hidenGameObject.SetActive(false);
        else if (items.GetComponent<Toggle>().isOn == true)
            hidenGameObject.SetActive(true);
    }

    public void ShowItem(C_Stage_Object stage_Object)
    {
        this.stage_Object = stage_Object;
        imageShowItem.sprite = stage_Object.spriteForInventory;
        nameObject.text = stage_Object.textForInventory;
        idItem = stage_Object.idItem;
        pieceItem = stage_Object.pieceItem;
        coinItem = stage_Object.coinItem;
        pieItem = stage_Object.pieItem;
        stage_Object.gameObject.SetActive(false);
        GetComponent<Animation>().Play("Open");
        //if (stage_Object.pieceItem)
        //{
        //    C_PieceItem[] pieseitems = inventory_transform.GetComponentsInChildren<C_PieceItem>();
        //    foreach (var item in pieseitems)
        //        if (item.idItem == idItem && item.text.text == "(2/3)")
        //        {
        //            Destroy(item.gameObject);
        //            break;
        //        }
        //}        
    }

    public void CloseAnimation()
    {
        GetComponent<Animation>().Play("Close");
    }

    public void Create_Item_in_Inventory()
    {
        if (pieItem)
        {
            GameObject newObject = Instantiate(prefab_pieItem, inventory_transform);

            newObject.GetComponentInChildren<Image>().sprite = imageShowItem.sprite;
            newObject.GetComponentInChildren<C_PieItem>().idItem = idItem;
            newObject.GetComponentInChildren<Text>().text = nameObject.text;
            newObject.GetComponentInChildren<C_PieItem>().text.text = "x3";
            StartCoroutine("DropItem", newObject.GetComponentInChildren<Image>().transform);
            C_PlayerPrefs.SaveInventoryItem newItems = new C_PlayerPrefs.SaveInventoryItem { id = idItem, namePicture = imageShowItem.sprite.name, countPieceItem = 0, nameItem = nameObject.text, pie = 3 };
            saveInventoryItems.Add(newItems);
            C_PlayerPrefs.mc_this.SaveInventoryItems(saveInventoryItems);
            C_PlayerPrefs.mc_this.SaveSceneState();
            return;
        }
        if (!pieceItem)
        {
            GameObject newObject = Instantiate(prefab_Item, inventory_transform);

            newObject.GetComponentInChildren<Image>().sprite = imageShowItem.sprite;
            newObject.GetComponentInChildren<C_Item_In_Inventory>().idItem = idItem;
            newObject.GetComponentInChildren<Text>().text = nameObject.text;
            StartCoroutine("DropItem", newObject.GetComponentInChildren<Image>().transform);
            C_PlayerPrefs.SaveInventoryItem newItems = new C_PlayerPrefs.SaveInventoryItem { id = idItem, namePicture = imageShowItem.sprite.name, countPieceItem = 0, nameItem = nameObject.text, pie = 0};
            saveInventoryItems.Add(newItems);
            C_PlayerPrefs.mc_this.SaveInventoryItems(saveInventoryItems);
            C_PlayerPrefs.mc_this.SaveSceneState();
            return;
        }
        if (coinItem)
        {
            C_PieceItem[] pieseitems = inventory_transform.GetComponentsInChildren<C_PieceItem>();
            foreach (var item in pieseitems)
            {
                if (item.idItem == idItem)
                {
                    if (item.text.text == "(1/4)")
                    {
                        for (int i = 0; i < saveInventoryItems.Count; i++)
                            if (saveInventoryItems[i].id == idItem)
                                saveInventoryItems[i].countPieceItem = 2;
                        item.text.text = "2/4";

                        StartCoroutine("DropItem", item.GetComponentInChildren<Image>().rectTransform);
                        C_PlayerPrefs.mc_this.SaveInventoryItems(saveInventoryItems);
                        C_PlayerPrefs.mc_this.SaveSceneState();
                        return;
                    }
                    else if (item.text.text == "(2/4)")
                    {
                        for (int i = 0; i < saveInventoryItems.Count; i++)
                            if (saveInventoryItems[i].id == idItem)
                                saveInventoryItems[i].countPieceItem = 3;
                        item.text.text = "3/4";

                        StartCoroutine("DropItem", item.GetComponentInChildren<Image>().rectTransform);
                        C_PlayerPrefs.mc_this.SaveInventoryItems(saveInventoryItems);
                        C_PlayerPrefs.mc_this.SaveSceneState();
                        return;
                    }
                    else if (item.text.text == "(3/4)")
                    {
                        saveInventoryItems.Remove(new C_PlayerPrefs.SaveInventoryItem
                        {
                            id = item.idItem,
                            namePicture = item.GetComponentInChildren<Image>().sprite.name,
                            countPieceItem = 3,
                            nameItem = item.GetComponentInChildren<Text>().text
                        });
                        Destroy(item.gameObject);
                        GameObject newObjects = Instantiate(prefab_Item, inventory_transform);

                        newObjects.GetComponentInChildren<Image>().sprite = imageShowItem.sprite;
                        newObjects.GetComponentInChildren<C_Item_In_Inventory>().idItem = idItem;
                        newObjects.GetComponentInChildren<Text>().text = nameObject.text;
                        StartCoroutine("DropItem", newObjects.GetComponentInChildren<Image>().rectTransform);
                        C_PlayerPrefs.SaveInventoryItem newItems = new C_PlayerPrefs.SaveInventoryItem { id = idItem, namePicture = imageShowItem.sprite.name, countPieceItem = 0, nameItem = nameObject.text, pie = 0 };
                        saveInventoryItems.Add(newItems);
                        C_PlayerPrefs.mc_this.SaveInventoryItems(saveInventoryItems);
                        C_PlayerPrefs.mc_this.SaveSceneState();
                        return;
                    }
                }
                else
                {
                    GameObject newObject = Instantiate(prefab_coinItem, inventory_transform);
                    newObject.GetComponentInChildren<Text>().text = nameObject.text;
                    StartCoroutine("DropItem", newObject.GetComponentInChildren<Image>().rectTransform);
                    newObject.GetComponentInChildren<Image>().sprite = imageShowItem.sprite;
                    newObject.GetComponent<C_PieceItem>().idItem = idItem;
                    C_PlayerPrefs.SaveInventoryItem newItem = new C_PlayerPrefs.SaveInventoryItem { id = idItem, namePicture = imageShowItem.sprite.name, countPieceItem = 1, nameItem = nameObject.text, pie = 0 };
                    saveInventoryItems.Add(newItem);
                    C_PlayerPrefs.mc_this.SaveInventoryItems(saveInventoryItems);
                    C_PlayerPrefs.mc_this.SaveSceneState();
                }
            }
        }
        else
        {
            C_PieceItem[] pieseitems = inventory_transform.GetComponentsInChildren<C_PieceItem>();
            foreach (var item in pieseitems)
            {
                if (item.idItem == idItem)
                {
                    if (item.text.text == "(2/3)")
                    {
                        for (int i = 0; i < saveInventoryItems.Count; i++)
                            if (saveInventoryItems[i].id == idItem)
                                saveInventoryItems.Remove(saveInventoryItems[i]);

                        Destroy(item.gameObject);
                        GameObject newObjects = Instantiate(prefab_Item, inventory_transform);

                        newObjects.GetComponentInChildren<Image>().sprite = imageShowItem.sprite;
                        newObjects.GetComponentInChildren<C_Item_In_Inventory>().idItem = idItem;
                        newObjects.GetComponentInChildren<Text>().text = nameObject.text;
                        StartCoroutine("DropItem", newObjects.GetComponentInChildren<Image>().rectTransform);
                        C_PlayerPrefs.SaveInventoryItem newItems = new C_PlayerPrefs.SaveInventoryItem { id = idItem, namePicture = imageShowItem.sprite.name, countPieceItem = 0, nameItem = nameObject.text, pie = 0 };
                        saveInventoryItems.Add(newItems);
                        C_PlayerPrefs.mc_this.SaveInventoryItems(saveInventoryItems);
                        return;
                    }
                    StartCoroutine("DropItem", item.GetComponentInChildren<Image>().rectTransform);

                    for (int i = 0; i < saveInventoryItems.Count; i++)
                        if (saveInventoryItems[i].id == idItem)
                            saveInventoryItems[i].countPieceItem = 2;
                    item.text.text = "(2/3)";
                    C_PlayerPrefs.mc_this.SaveInventoryItems(saveInventoryItems);
                    C_PlayerPrefs.mc_this.SaveSceneState();
                    return;
                }
            }
            GameObject newObject = Instantiate(prefab_pieceItem, inventory_transform);
            newObject.GetComponentInChildren<Text>().text = nameObject.text;
            StartCoroutine("DropItem", newObject.GetComponentInChildren<Image>().rectTransform);
            newObject.GetComponentInChildren<Image>().sprite = imageShowItem.sprite;
            newObject.GetComponent<C_PieceItem>().idItem = idItem;
            C_PlayerPrefs.SaveInventoryItem newItem = new C_PlayerPrefs.SaveInventoryItem { id = idItem, namePicture = imageShowItem.sprite.name, countPieceItem = 1, nameItem = nameObject.text, pie = 0 };
            saveInventoryItems.Add(newItem);
            C_PlayerPrefs.mc_this.SaveInventoryItems(saveInventoryItems);
            C_PlayerPrefs.mc_this.SaveSceneState();
        }        
    }

    IEnumerator DropItem(RectTransform newObject)
    {
        float stepX = (imageShowItem.transform.position.x - (newObject.position.x - 0.05f)) / 50;
        float stepY = (imageShowItem.transform.position.y - (newObject.position.y + 0.05f)) / 50;

        for (int i = 0; i <= 25; i++)
        {
            imageShowItem.rectTransform.position = new Vector3(imageShowItem.transform.position.x - stepX, imageShowItem.transform.position.y - stepY, 0);
            imageShowItem.rectTransform.localScale = new Vector3(imageShowItem.transform.localScale.x - 0.02f, imageShowItem.transform.localScale.y - 0.02f, 1);
            yield return new WaitForSeconds(0.015f);
        }
        imageShowItem.color = new Color(255, 255, 255, 0);
        newObject.GetComponentInChildren<Image>().color = new Color(255, 255, 255, 1);
        CloseAnimation();
    }

    public void MissClick()
    {
        C_Take_Drop_Item.mc_this.DropItem();
        C_CursorManager.instance.UpdateToDefault();
        StartCoroutine("MoveDragandDropItem");
    }

    IEnumerator MoveDragandDropItem()
    { 
        if (DragAndDropItem.gameObject.activeSelf == true)
        {
            blockclick.SetActive(false);
            Cursor.visible = true;
            Drag_and_Drop.mc_init.StopCoroutine("NewPosition");

            Transform itmeInventory = null;
            C_Item_In_Inventory[] inventory = inventory_transform.GetComponentsInChildren<C_Item_In_Inventory>();

            for (int i = 0; i < 20; i++)
            {
                if (i < 8)
                    DragAndDropItem.transform.rotation = new Quaternion(DragAndDropItem.transform.rotation.x,
                                                                             DragAndDropItem.transform.rotation.y,
                                                                             DragAndDropItem.transform.rotation.z + 0.03f,
                                                                             DragAndDropItem.transform.rotation.w);
                else if (i < 16)
                    DragAndDropItem.transform.rotation = new Quaternion(DragAndDropItem.transform.rotation.x,
                                                                             DragAndDropItem.transform.rotation.y,
                                                                             DragAndDropItem.transform.rotation.z - 0.05f,
                                                                             DragAndDropItem.transform.rotation.w);

                else
                    DragAndDropItem.transform.rotation = new Quaternion(DragAndDropItem.transform.rotation.x,
                                                                             DragAndDropItem.transform.rotation.y,
                                                                             DragAndDropItem.transform.rotation.z + 0.04f,
                                                                             DragAndDropItem.transform.rotation.w);
                yield return new WaitForSeconds(0.001f);
            }

            for (int y = 0; y < inventory.Length; y++)
            {
                if (inventory[y].GetComponentInChildren<Image>().color.a != 1)
                {
                    itmeInventory = inventory[y].transform;
                    break;
                }
            }
            if (itmeInventory == null)
                itmeInventory = inventory_transform.GetComponentInChildren<C_PieItem>().transform;

            float stepX = (DragAndDropItem.transform.position.x - (itmeInventory.position.x + 0.13f)) / 50;
            float stepY = (DragAndDropItem.transform.position.y - (itmeInventory.position.y + 0.1f)) / 50;

            for (int iStep = 0; iStep < 50; iStep++)
            {
                DragAndDropItem.transform.position = new Vector3(DragAndDropItem.transform.position.x - stepX, DragAndDropItem.transform.position.y - stepY, 0);
                yield return new WaitForSeconds(0.01f);
            }
            DragAndDropItem.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            itmeInventory.gameObject.GetComponentInChildren<Image>().color = new Color(255, 255, 255, 1);
            DragAndDropItem.gameObject.SetActive(false);           
        }
    }
}
