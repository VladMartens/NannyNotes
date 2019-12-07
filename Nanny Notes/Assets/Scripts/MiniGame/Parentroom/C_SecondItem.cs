using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_SecondItem : MonoBehaviour
{
    public string nameItem;

    public void FoundObject()
    {
        C_Take_Drop_Item.mc_this.HidenObject();
        GetComponent<Button>().interactable = false;
        StartCoroutine("GiveItem");
        C_SecontHidenObject.mc_this.FoundComplete(nameItem);
    }

    private IEnumerator GiveItem()
    {
        for (float i = transform.localScale.x; i <= 1.2f;)
        {
            transform.localScale = new Vector3(i += 0.02f, i, 1);
            yield return new WaitForSeconds(0.015f);
        }
        for (float i = transform.localScale.x; i >= 1;)
        {
            transform.localScale = new Vector3(i -= 0.02f, i, 1);
            yield return new WaitForSeconds(0.015f);
        }
        for (float i = transform.localScale.x; i <= 1.2f;)
        {
            transform.localScale = new Vector3(i += 0.02f, i, 1);
            yield return new WaitForSeconds(0.015f);
        }
        for (float i = transform.localScale.x; i >= 1;)
        {
            transform.localScale = new Vector3(i -= 0.02f, i, 1);
            yield return new WaitForSeconds(0.015f);
        }

        float stepX = (transform.position.x - C_HidenObjects.mc_this.defaultPosition.position.x) / 50;
        float stepY = (transform.position.y - C_HidenObjects.mc_this.defaultPosition.position.y) / 50;

        for (int i = 0; i < 50; i++)
        {
            transform.position = new Vector3(transform.position.x - stepX, transform.position.y - stepY, 0);
            transform.localScale = new Vector3(transform.localScale.x - 0.02f, transform.localScale.y - 0.02f, 0);
            yield return new WaitForSeconds(0.015f);
        }
        gameObject.SetActive(false);
    }
}
