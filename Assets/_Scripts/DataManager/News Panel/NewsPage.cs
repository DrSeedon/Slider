using System;
using System.Collections;
using System.Collections.Generic;
using DataManager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class NewsPage : MonoBehaviour
{
    
    public TMP_Text titleField;
    public TMP_Text descriptionField;
    
    public Image mainImageField;
    public Image qrCodeField;
    public SliderController slider;

    public Button showSliderButton;
    public Button closeSliderButton;

    public Button _pageButton;
    private UIVerticalScroller _verticalScroller;
    private NewsDataManager.NewsData _data;
    [SerializeField] private int _pageId;
    private void Awake()
    {
        _verticalScroller = GetComponentInParent<UIVerticalScroller>();
        //_pageButton.onClick.RemoveAllListeners();
        _pageButton.onClick.AddListener(()=>
        {
            _verticalScroller.ScrollToElement(_pageId);
        });
    }

    public void SetDataOnGUI(NewsDataManager.NewsData data, int pageId)
    {
        _data = data;
        _pageId = pageId;
        
        titleField.text = data.title;
        descriptionField.text = data.description;
        
        if(data.qrCodeSprite)
            qrCodeField.sprite = data.qrCodeSprite;
        else
            qrCodeField.enabled = false;
        
        if(data.mainImageSprite)
            mainImageField.sprite = data.mainImageSprite;
        
        if(data.sliderSprites == null || data.sliderSprites.Length == 0)
            showSliderButton.gameObject.SetActive(false);
        
        /*if (slider)
        {
            slider.titles = new List<string>(data.sliderTitles);
            slider.SetSprites(data.sliderSprites);
        }*/
        
        if(showSliderButton)
            showSliderButton.onClick.AddListener(ShowSlider);
        if(closeSliderButton)
            closeSliderButton.onClick.AddListener(CloseSlider);

       
    }

    public void ShowSlider()
    {
        if (slider && _data.sliderSprites != null && _data.sliderSprites.Length > 0)
        {
            slider.titles = new List<string>(_data.sliderTitles);
            slider.SetSprites(_data.sliderSprites);
            //slider._snap.transitionSpeed = 25;
            //slider.SelectScreen(slider.pageOnStart);
            //slider._snap.transitionSpeed = 7.5f;
        }

        slider.transform.parent.parent.GetComponent<Image>().DOFade(0.4f, 0.25f);
        slider.transform.parent.parent.GetComponent<Image>().raycastTarget = true;
        slider.transform.parent.DOScale(Vector3.one, 0.25f);
    }

    public void CloseSlider()
    {
        slider.transform.parent.parent.GetComponent<Image>().DOFade(0, 0.25f);
        slider.transform.parent.parent.GetComponent<Image>().raycastTarget = false;
        
        slider.transform.parent.DOScale(Vector3.zero, 0.25f);
        slider._snap.transitionSpeed = 25;
        slider.SelectScreen(slider.pageOnStart);
        slider._snap.transitionSpeed = 7.5f;
    }
}
