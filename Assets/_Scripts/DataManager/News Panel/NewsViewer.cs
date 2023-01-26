using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataManager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;


public class NewsViewer : DataManageViewer
{
    public NewsDataManager newsDataManager;
    
    public GameObject leftPagePrefab;
    public GameObject rightPagePrefab;

    public UIVerticalScroller verticalScroller;
    public RectTransform scrollerContent;
    
    public SliderController imageSlider;
    public Button closeSliderButton;
    public Button closeSliderBack;

    
    public bool topOnEnable = true;
    private void Awake()
    {
        newsDataManager.onDataLoaded += SetDataView;
        
        closeSliderBack.onClick.AddListener(() =>
        {
            imageSlider.transform.parent.parent.GetComponent<Image>().DOFade(0, 0.25f);
            imageSlider.transform.parent.parent.GetComponent<Image>().raycastTarget = false;
        
            imageSlider.transform.parent.DOScale(Vector3.zero, 0.25f);
            imageSlider._snap.transitionSpeed = 20;
            imageSlider.SelectScreen(imageSlider.pageOnStart);
            imageSlider._snap.transitionSpeed = 7.5f;
        });
    }
    private void OnEnable()
    {
        SetDefaultPos();
        base.OnEnable();
    }

    public override async void SetDefaultPos()
    {
        if (verticalScroller._arrayOfElements.Length > 0 && topOnEnable)
        {
            float speedOld = verticalScroller.scrollSpeedOnOutput;
            //verticalScroller.scrollSpeedOnOutput = 150f;
            
            if (verticalScroller._arrayOfElements.Length > 1) 
                verticalScroller.SnapToElement(2);
            else 
                verticalScroller.SnapToElement(0);
            await Task.Delay(100);
            verticalScroller.scrollSpeedOnOutput = speedOld;
        }
    }
    
    
    private void SetDataView()
    {
        newsDataManager.data.Reverse();
        
        
        var onLeft = true;
        int id = 0;
        foreach (var data in newsDataManager.data)
        {
            var page = onLeft
                ? Instantiate(leftPagePrefab, scrollerContent).GetComponent<NewsPage>()
                : Instantiate(rightPagePrefab, scrollerContent).GetComponent<NewsPage>();

            page.slider = imageSlider;
            page.closeSliderButton = closeSliderButton;
            page.SetDataOnGUI(data, id);
            id++;
            
            onLeft = !onLeft;
        }
        verticalScroller.updateChildren();
    }
}
