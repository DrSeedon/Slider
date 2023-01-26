using System;
using System.Collections;
using System.Collections.Generic;
using DataManager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DataManager
{
    public class DistrictPage : MonoBehaviour
    {
    
        public TMP_Text descriptionField;
        public SliderController slider;

        private DistrictDataManager.DistrictData _data;
    
        private void OnDisable()
        {
           //slider._snap.transitionSpeed = 15;
           //slider.SelectScreen(slider.pageOnStart);
           //slider._snap.transitionSpeed = 7.5f;
        }

        public void SetDataOnGUI(DistrictDataManager.DistrictData data)
        {
            _data = data;
            descriptionField.text = data.description;

            /*if (slider)
            {
                slider.titles = new List<string>(data.sliderTitles);
                slider.SetSprites(data.sliderSprites);
            }*/
        
        }
    }
    
}
