using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PoetryPage : MonoBehaviour
{
    public CanvasGroup cvGroup;
    public TMP_Text title;
    public RectTransform content;
    public TMP_Text description;
    public Image qrImage;
    public GameObject qrToggle;
    
    private List<PoetryDataManager.PoetryData> data;
    private int _curPoetryId = 0;
    
    public void SetData(List<PoetryDataManager.PoetryData> data)
    {
        this.data = data;
        SetDataOnGUI(_curPoetryId);
    }

    public void Next()
    {
        _curPoetryId = (_curPoetryId + 1) % data.Count;
        SetDataOnGUI(_curPoetryId);
    }
    public void Prev()
    {
        _curPoetryId = _curPoetryId == 0 ? data.Count - 1 : (_curPoetryId - 1) % data.Count;
        SetDataOnGUI(_curPoetryId);
    }
    private async void SetDataOnGUI(int id)
    {
        await cvGroup.DOFade(0, 0.25f).AsyncWaitForCompletion();
        
        title.text = data[id].title;
        description.text = data[id].description;
        if (data[id].imagesPath != null && data[id].imagesPath.Length > 0 && data[id].imagesPath[0] != "")
        {
            qrToggle.SetActive(true);
            qrImage.sprite =  QRGenerator.GenerateQRSprite(data[id].imagesPath[0]);
        }
        else
        {
            qrToggle.SetActive(false);
        }

        content.sizeDelta = new Vector2(content.sizeDelta.x , content.GetChild(0).GetComponent<RectTransform>().sizeDelta.y);
        
        await cvGroup.DOFade(1, 0.25f).AsyncWaitForCompletion();
    }
}
