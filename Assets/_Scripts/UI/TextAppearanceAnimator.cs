using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextAppearanceAnimator : MonoBehaviour
{
    enum AnimationType
    {
        Fade,
        Print
    }

    [Header("Анимация проявления")]
    [SerializeField]
    private AnimationType animIn;
    [SerializeField] 
    private float animInDuration = 0.25f;
    
    [Header("Анимация исчезновения")]
    [SerializeField]
    private AnimationType animOut;
    [SerializeField] 
    private float animOutDuration = 0.25f;

    //параметры
    
    
    private TMP_Text _text;
    private float _curTweenValue = 0;
    private bool _onAnimating;
    
    private void Awake()
    {
        if (!_text) _text = GetComponent<TMP_Text>();
    }

    private TweenerCore<Color, Color, ColorOptions> _fadeTweener;
    
    private void FadeAnimation(float targetValue, float duration)
    {
        _onAnimating = true;
        _fadeTweener = _text.DOFade(targetValue, duration)
            .OnComplete(() =>
            {
                _onAnimating = false;
                _curTweenValue = targetValue;
            });
    }

    private void PrintAnimation(bool reversed = false)
    {
        var text_buff = _text.text;

        if (!reversed)
        {
            
        }
        
    }
}
