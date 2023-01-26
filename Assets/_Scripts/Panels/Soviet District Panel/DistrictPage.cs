using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DataManager;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DistrictPage : MonoBehaviour
{
    public CanvasGroup _canvasGroup;
    public TMP_Text title;
    public TMP_Text description;
    public VerticalLayoutGroup descriptionLayout;
    public SliderController slider;

    private InfoDataBase _data;
    


    public void SetData(InfoDataBase data, Sprite[] sprites)
    {
        _data = data;

        title.text = _data.title;
        description.text = _data.description;
        
        
        slider.SetSprites(sprites);
    }
    
    private TweenerCore<float, float, FloatOptions> _tweener;
    public async void SetDataAnimated(InfoDataBase data, Sprite[] sprites)
    {
        _data = data;
        _tweener = _canvasGroup.DOFade(0, 0.25f);
        await Task.Delay(250);
       
        title.text = _data.title;
        description.text = _data.description;
        descriptionLayout.enabled = false;
        await Task.Delay(25);
        descriptionLayout.enabled = true;
        slider.SetSprites(sprites);
        //description.rectTransform.parent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        
        _tweener = _canvasGroup.DOFade(1, 0.25f);
        await Task.Delay(250);
    }
}
