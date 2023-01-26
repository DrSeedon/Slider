using System;
using System.Collections;
using System.Collections.Generic;
using DataManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class PosterPage : MonoBehaviour
{
    
    public TMP_Text titleFieldOn;
    public TMP_Text titleFieldOff;
    
    public TMP_Text dateFieldOn;
    public TMP_Text dateFieldOff;

    public TMP_Text descriptionField;
    public Image qrCodeField;
    public Button pageButton;
    
    private int _pageId;
    private UIVerticalScroller _verticalScroller;
    private void Awake()
    {
        _verticalScroller = GetComponentInParent<UIVerticalScroller>();
        pageButton.onClick.RemoveAllListeners();
        pageButton.onClick.AddListener(()=>_verticalScroller.ScrollToElement(_pageId));
    }

    public void SetDataOnGUI(PosterDataManager.PosterData data, int pageId)
    {
        _pageId = pageId;
        
        titleFieldOn.text = data.title;
        titleFieldOff.text = data.title;
        
        dateFieldOn.text = data.date;
        dateFieldOff.text = data.date;
        
        descriptionField.text = data.description;
        
        if(data.qrCodeSprite)
            qrCodeField.sprite = data.qrCodeSprite;
        else
            qrCodeField.enabled = false;
    }
}
