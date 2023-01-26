using System;
using System.Collections;
using System.Collections.Generic;
using DataManager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectPage : MonoBehaviour
{
    
    public TMP_Text titleField;
    public TMP_Text descriptionField;
    public SliderController slider;

    private ProjectDataManager.ProjectData _data;
    
    private void OnDisable()
    {
        slider._snap.transitionSpeed = 15;
        slider.SelectScreen(slider.pageOnStart);
        slider._snap.transitionSpeed = 7.5f;
    }

    public void SetDataOnGUI(ProjectDataManager.ProjectData data)
    {
        _data = data;
        
        titleField.text = data.title;
        descriptionField.text = data.description;

        if (slider)
        {
            slider.SetSprites(data.sliderSprites);
        }
        
    }
}
