using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectDataManager : DataManager<ProjectDataManager.ProjectData>
    {
        [Serializable]
        public class ProjectData: InfoDataBase
        {
            public ProjectData():base()
            {
                
            }
            public ProjectData(string title, string description, string[] imagesPath) : base(title, description, imagesPath)
            {
                
            }
        }

        internal override void Start()
        {
            dataFilePath = MainSettings.Instance.SettingsData.ProjectFilePath;
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
                if(d.imagesPath.Length > 0)
                    d.sliderSprites = FileUtility.LoadSprites(d.imagesPath).ToArray();
            }
        }

        [ContextMenu("Create Data File Sample")]
        public void CreateDataFileSample()
        {
            _data = new List<ProjectData>();
            _data.Add(new ProjectData("","",new[]{""}));
            
            base.CreateDataFileSample();
        }
    }
}
