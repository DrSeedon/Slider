using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class DistrictDataManager : DataManager<DistrictDataManager.DistrictData>
    {
        [Serializable]
        public class DistrictData: InfoDataBase
        {
            public string[] sliderTitles;
            public DistrictData():base()
            {
                
            }
            public DistrictData(string title, string description, string[] imagesPath, string[] sliderTitles) : base(title, description, imagesPath)
            {
                this.sliderTitles = sliderTitles;
            }
        }

        internal override void Start()
        {
            dataFilePath = MainSettings.Instance.SettingsData.DistrictFilePath;
            base.Start();
            if (loadDataOnAwake)
            {
                LoadAllSprites();
                onDataLoaded?.Invoke();
            }
        }

        public override void LoadAllSprites()
        {
            foreach (var d in _data)
            {
                if(d.imagesPath != null && d.imagesPath.Length > 0)
                    d.sliderSprites = FileUtility.LoadSprites(d.imagesPath).ToArray();
            }
        }

        [ContextMenu("Create Data File Sample")]
        public void CreateDataFileSample()
        {
            _data = new List<DistrictData>();
            _data.Add(new DistrictData("","",new[]{""},new[]{""}));
            
            base.CreateDataFileSample();
        }
    }
}
