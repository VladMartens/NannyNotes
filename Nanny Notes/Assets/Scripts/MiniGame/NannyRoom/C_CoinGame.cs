using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CoinGame : MonoBehaviour
{
    public GameObject[] levelGame;    
    public C_Stage_Object prize;
    public C_AnimationItem zoomAnimation;

    private int numberLevel = 0;
    private C_CoinItem coin;

    public void Check(C_CoinItem coinItem)
    {
        coin = coinItem;
        coin.ErrorClick.SetActive(true);

        if (coin.trueCoin)
        {            
            levelGame[numberLevel].SetActive(false);
            if (++numberLevel > levelGame.Length-1)
            {
                prize.ClickTarget(false);
                prize.GiveItem();
                zoomAnimation.StartAnimation("BookClose");
            }
            else
            {
                levelGame[numberLevel-1].SetActive(false);
                levelGame[numberLevel].SetActive(true);
            }
                
        }
        else
        {
            coin.blockClick.SetActive(true);
            StartCoroutine("TimerBlockClick");
        }
    }

    private IEnumerator TimerBlockClick()
    {
        yield return new WaitForSeconds(1);
        coin.blockClick.SetActive(false);
        coin.ErrorClick.SetActive(false);
    }
}
