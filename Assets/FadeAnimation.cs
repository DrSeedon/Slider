using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
    private CanvasGroup cv;

    public float fadeOutDuration = 0.5f;

    private void Start()
    {
        cv = GetComponent<CanvasGroup>();
    }
    public void FadeOut()
    {
        cv.DOFade(1, fadeOutDuration);
    }
    public void Fade(float value)
    {
        cv.DOFade(value, fadeOutDuration);
    }
    public void FadeIn()
    {
        cv.DOFade(0, fadeOutDuration);
    }
    public void FadeZero()
    {
        cv.alpha = 0;
    }
}
