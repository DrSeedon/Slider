using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UIPanels;
using UnityEngine;

public class SovietDistrictPanelController : UIPanelBaseController
{
    public CanvasGroup panelCV;

    public Animator frontLayerAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        if (!panelCV) panelCV = GetComponent<CanvasGroup>();
    }

    public override IEnumerator ShowPanelRoutine()
    {
        //анимируем передний слой
        frontLayerAnimator.SetTrigger("Show");
        //Ждем, пока завершится анимация
        var delay = frontLayerAnimator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(delay);
        
        //показываем панель
        panelCV.DOFade(1, animationDuration);
        yield return new WaitForSeconds(animationDuration);
        panelCV.interactable = true;
        panelCV.blocksRaycasts = true;
        
        OnShowEnd?.Invoke();
    }

    public override IEnumerator HidePanelRoutine()
    {
        //прячем панель
        panelCV.interactable = false;
        panelCV.blocksRaycasts = false;
        panelCV.DOFade(0, animationDuration);
        yield return new WaitForSeconds(animationDuration);
        
        //анимируем передний слой
        frontLayerAnimator.SetTrigger("Hide");
        //Ждем, пока завершится анимация
        var delay = frontLayerAnimator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(delay);
        
        OnCloseEnd?.Invoke();
    }
}