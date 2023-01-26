using System;
using System.Collections;
using System.Collections.Generic;
using DataManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;


public class DistrictViewer : MonoBehaviour
{
    public DistrictDataManager districtDataManager;

    public DataManager.DistrictPage[] pages;

    private void Awake()
    {
        districtDataManager.onDataLoaded += SetDataView;
    }

    private void SetDataView()
    {
        var i = 0;
        
        foreach (var data in districtDataManager.data)
        {
            pages[i].SetDataOnGUI(data);
            i++;
        }
    }
}
