using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSwitcher : MonoBehaviour
{
    [Header("Data")]
    public List<TextElement> textEments;
    [Header("View")]
    public TMP_Text textContainer;
    public CanvasGroup cvText;
    [Header("Params")]
    public float fadeDuration = 0.25f;
    public bool selectOnStart = true;
    //[ShowIf(ShowIfAttribute.ActionOnConditionFail.DontDraw, ShowIfAttribute.ConditionOperator.And, nameof(selectOnStart))]
    public int firstSelectable = 0;
    public bool autoSnap = true;
  //  [ShowIf(ShowIfAttribute.ActionOnConditionFail.DontDraw, ShowIfAttribute.ConditionOperator.And, nameof(autoSnap))]
    public float snapDelay = 7f;
    
    private int _curSelectable;

    private void Start()
    {
        _curSelectable = firstSelectable;

        //устанавливаем начальный текст
        if (selectOnStart)
        {
            if (_switchRoutine != null) StopCoroutine(_switchRoutine);
            _switchRoutine = StartCoroutine(SwitchText(textEments[_curSelectable]));
        }
        //запускаем автоснап, если флаг = true
        if (autoSnap)
        {
            if (_snapRoutine != null) StopCoroutine(_snapRoutine);
            _snapRoutine = StartCoroutine(AutoSnap());
        }
        
    }

    public void ShowNext()
    {
        
        _curSnapTime = 0f;
        _curSelectable = (_curSelectable + 1) % textEments.Count;
        
        if(_switchRoutine != null) StopCoroutine(_switchRoutine);
        _switchRoutine = StartCoroutine(SwitchText(textEments[_curSelectable]));
    }
    public void ShowPrev()
    {
        _curSnapTime = 0f;
        _curSelectable = (_curSelectable - 1) < 0? textEments.Count - 1 :  (_curSelectable - 1) % textEments.Count;
        
        if(_switchRoutine != null) StopCoroutine(_switchRoutine);
        _switchRoutine = StartCoroutine(SwitchText(textEments[_curSelectable]));
    }

    //корутина анимации пролистывания
    private Coroutine _switchRoutine;
    private TweenerCore<float, float, FloatOptions> _fadeTweener;
    private IEnumerator SwitchText(TextElement elem)
    {
        _fadeTweener?.Kill();
        
        _fadeTweener = cvText.DOFade(0, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        textContainer.text = elem.text;
        _fadeTweener = cvText.DOFade(1, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        _switchRoutine = null;
    }

    //корутина автопролистывания
    private Coroutine _snapRoutine;
    
    private float _curSnapTime;
    private IEnumerator AutoSnap()
    {
        while (true)
        {
            _curSnapTime += Time.deltaTime;
            if (_curSnapTime >= snapDelay)
            {
                ShowNext();
            }
            yield return null;
        }
    }
    
}

[Serializable]
public class TextElement
{
    [TextArea]
    public string text;
}
