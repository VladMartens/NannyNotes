using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class C_MenuController : MonoBehaviour
{
    public Image fade;
    public Animation menu;

    private bool pause = false;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            On_Of_Menu();
    }

    public void On_Of_Menu()
    {
        if (!pause)
        {
            pause = true;
            //Time.timeScale = 0;
            menu.Play("OpenMenu");
           // StartCoroutine(ReverseNonTimeScale(menu, menu.GetClip("OpenMenu")));
        }
        else
        {
            pause = false;
            //Time.timeScale = 1;
            menu.Play("CloseMenu");
        }
    }

    IEnumerator ReverseNonTimeScale(Animation animation, AnimationClip _clip)
    {
        AnimationState _currState = animation[_clip.name];
        bool isPlaying = true;
        float _progressTime = 0F;
        float _timeAtLastFrame = 0F;
        float _timeAtCurrentFrame = 0F;
        float deltaTime = 0F;

        animation.clip = _clip;

        _timeAtLastFrame = Time.realtimeSinceStartup;
        while (isPlaying)
        {
            _timeAtCurrentFrame = Time.realtimeSinceStartup;
            deltaTime = _timeAtCurrentFrame - _timeAtLastFrame;
            _timeAtLastFrame = _timeAtCurrentFrame;

            _progressTime += deltaTime;
            animation.Play();
            _currState.normalizedTime = 1.0f - (_progressTime / _currState.length);

            animation.Sample();
            animation.Stop();

            if (_progressTime >= _clip.length)
            {
                _currState.normalizedTime = 0.0f;
                isPlaying = false;
            }

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
