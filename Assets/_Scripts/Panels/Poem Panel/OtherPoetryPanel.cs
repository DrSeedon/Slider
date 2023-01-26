using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UIPanels;
using UnityEngine;

public class OtherPoetryPanel : UIPanelBaseController
{
    public CanvasGroup panelCV;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator ShowPanelRoutine()
    {
        OnShowStart?.Invoke();
        
        panelCV.interactable = false;
        panelCV.blocksRaycasts = false;
        
        //panelCV.DOFade(1, animationDuration);
        animator.SetTrigger("Show");
        var delay = animator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(delay);
        
        panelCV.interactable = true;
        panelCV.blocksRaycasts = true;
        
        OnShowEnd?.Invoke();
    }

    public override IEnumerator HidePanelRoutine()
    {
        OnCloseStart?.Invoke();
        
        panelCV.interactable = false;
        panelCV.blocksRaycasts = false;
        
        //panelCV.DOFade(0, animationDuration);
        animator.SetTrigger("Hide");
        var delay = animator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(delay);
        
        //panelCV.interactable = true;
        //panelCV.blocksRaycasts = true;
        
        OnCloseEnd?.Invoke();
    }
}
