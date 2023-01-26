using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;


public class PosterViewer : DataManageViewer
{
    public PosterDataManager posterDataManager;
    
    public GameObject leftPagePrefab;
    public GameObject rightPagePrefab;

    public UIVerticalScroller verticalScroller;
    public RectTransform scrollerContent;

    public int nearestElementId = 0;
    public bool topOnEnable = true;

    private List<PosterPage> _pages = new List<PosterPage>();
    private void Awake()
    {
        posterDataManager.onDataLoaded += SetDataView;
    }

    void OnEnable()
    {
        if (verticalScroller._arrayOfElements.Length > 0 && topOnEnable)
        {
            //SetDataView();
            SetDefaultPos();
            base.OnEnable();
        }
    }

    public override async void SetDefaultPos()
    {
        if (verticalScroller._arrayOfElements.Length > 0 && topOnEnable)
        {
            float speedOld = verticalScroller.scrollSpeedOnOutput;
            //verticalScroller.scrollSpeedOnOutput = 150;
            verticalScroller.SnapToElement(nearestElementId);
            await Task.Delay(100);
            verticalScroller.scrollSpeedOnOutput = speedOld;
        }
    }
    
    
    private void SetDataView()
    {
        ClearScroller();
        SortRecordByDate();
        var onLeft = true;
        int id = 0;
        foreach (var data in posterDataManager.data)
        {
            var page = onLeft
                ? Instantiate(leftPagePrefab, scrollerContent).GetComponent<PosterPage>()
                : Instantiate(rightPagePrefab, scrollerContent).GetComponent<PosterPage>();

            _pages.Add(page);
            page.SetDataOnGUI(data, id);
            onLeft = !onLeft;
            id++;
        }
        verticalScroller.updateChildren();
    }

    private void ClearScroller()
    {
        foreach (var p in _pages)
        {
            DestroyImmediate(p.gameObject);
        }
        _pages.Clear();
        verticalScroller.updateChildren();
    }
    
    private void SortRecordByDate()
    {
        
        Regex dateReg = new Regex(@"\d?\d? \w*");
        posterDataManager.data
            .Sort(
                (x,y)=>DateTime.Compare(
                    DateTime.Parse(dateReg.Match(x.date).Value),DateTime.Parse(dateReg.Match(y.date).Value)));
        
        posterDataManager.data.Reverse();

        var nowDate = DateTime.Now;
        var minTime = (DateTime.Parse("01.01.2150") - nowDate).TotalDays;
        int i = 0;
        foreach (var data in posterDataManager.data)
        {
            if (Math.Abs((DateTime.Parse(dateReg.Match(data.date).Value) - nowDate).TotalDays) < minTime)
            {
                minTime = (DateTime.Parse(dateReg.Match(data.date).Value) - nowDate).TotalDays;
                nearestElementId = i;
            }

            i++;
        }
    }
}
