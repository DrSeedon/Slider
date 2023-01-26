using System.Collections;
using System.Collections.Generic;
using DataManager;
using DG.Tweening;
using TMPro;
using UIPanels;
using UnityEngine;
using UnityEngine.UI;

public class DistrictPagePanel : UIPanelBaseController
{
    public TMP_Text title;
    public TMP_Text description;
    public Scrollbar descriptionScrollbar;
    public List<CanvasGroup> hidableObjects;
    public ScrollbarRepair scrollRepair;
    public SliderController slider;

    private InfoDataBase _data;
    
    public void SetData(InfoDataBase data, Sprite[] sprites)
    {
        _data = data;
        
        title.text = _data.title;
        description.text = _data.description;
        
        
        slider.SetSprites(sprites);
    }

    public void SetScrollbar()
    {
        scrollRepair._bar = descriptionScrollbar;
    }
    
    public override IEnumerator ShowPanelRoutine()
    {
        OnShowStart?.Invoke();
        
        hidableObjects.ForEach(o=>o.DOFade(1,animationDuration));
        yield return new WaitForSeconds(animationDuration);
        
        OnShowEnd?.Invoke();
    }

    public override IEnumerator HidePanelRoutine()
    {
        OnCloseStart?.Invoke();
        
        hidableObjects.ForEach(o=>o.DOFade(0,animationDuration));
        yield return new WaitForSeconds(animationDuration);
        
        OnCloseEnd?.Invoke();
    }
}
