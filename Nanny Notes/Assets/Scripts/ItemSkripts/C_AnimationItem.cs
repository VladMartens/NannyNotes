using UnityEngine;

public class C_AnimationItem : MonoBehaviour
{
    public void StartAnimation(string nameAnimation)
    {
        GetComponent<Animation>().Play(nameAnimation);
    }
}
