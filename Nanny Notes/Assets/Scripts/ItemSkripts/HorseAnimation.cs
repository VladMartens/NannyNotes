using UnityEngine;

public class HorseAnimation : MonoBehaviour
{
    Animation animationHorse;
    int i = 2;

    public void AnimHorse()
    {
        animationHorse = GetComponent<Animation>();
        --i;
        if (i == 0)
            animationHorse.Play("Horse");
    }
}