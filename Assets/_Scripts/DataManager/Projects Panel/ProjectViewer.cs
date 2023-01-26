using System;
using System.Collections;
using System.Collections.Generic;
using DataManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;


public class ProjectViewer : MonoBehaviour
{
    public ProjectDataManager projectDataManager;

    public ProjectPage[] pages;

    private void Awake()
    {
        projectDataManager.onDataLoaded += SetDataView;
    }

    private void SetDataView()
    {
        var i = 0;
        
        foreach (var data in projectDataManager.data)
        {
            pages[i].SetDataOnGUI(data);
            i++;
        }
    }
}
