using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UIPanels;
using UnityEngine;

public class AuthorMidPanel : UIPanelBaseController
{
    public Animator animator;
    
    public CanvasGroup frontPanelCV;
    public CanvasGroup backPanelCV;
    public ScrollbarRepair scrollbarRepair;
    public SliderController sliderController;


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator ShowPanelRoutine()
    {
        OnShowStart?.Invoke();
        //прячем панель
        backPanelCV.interactable = false;
        backPanelCV.blocksRaycasts = false;
        //анимируем срыв листика
        animator.SetTrigger("Show");
        //Ждем, пока завершится анимация
        var delay = animator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(delay);
        
        frontPanelCV.interactable = true;
        frontPanelCV.blocksRaycasts = true;
        
        OnShowEnd?.Invoke();
    }

    public override IEnumerator HidePanelRoutine()
    {
        OnCloseStart?.Invoke();
        //прячем панель
        frontPanelCV.interactable = false;
        frontPanelCV.blocksRaycasts = false;
        //анимируем срыв листика
        animator.SetTrigger("Hide");
        //Ждем, пока завершится анимация
        var delay = animator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(delay);
        
        backPanelCV.interactable = true;
        backPanelCV.blocksRaycasts = true;
        
        OnCloseEnd?.Invoke();
    }
}
