using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UIPanels;
using UnityEngine;

public class AboutAuthorPanelController : UIPanelBaseController
{
    private CanvasGroup _canvasGroup;

    [SerializeField] private float animationDuration = 0.25f;
    [SerializeField] private bool hideOnAwake = false;
    // Start is called before the first frame update
    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        if (toggle)
        {
            toggle.onValueChanged.AddListener(value =>
            {
                if (value) Show();
                else Hide();
            });
        }

        if (hideOnAwake)
        {
            toggle.isOn = false;
            Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private TweenerCore<float, float, FloatOptions> _tweener;
    public override IEnumerator ShowPanelRoutine()
    {
        _tweener?.Kill();
        _tweener = _canvasGroup.DOFade(1, animationDuration);
        
        yield return new WaitForSeconds(animationDuration);

        _canvasGroup.interactable = true;

    }

    public override IEnumerator HidePanelRoutine()
    {
        _tweener?.Kill();
        _tweener = _canvasGroup.DOFade(0, animationDuration);
        _canvasGroup.interactable = false;
        yield return new WaitForSeconds(animationDuration);

    }
}
