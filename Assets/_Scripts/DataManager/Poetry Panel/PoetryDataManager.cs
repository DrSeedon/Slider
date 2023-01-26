using System;
using System.Collections;
using System.Collections.Generic;
using DataManager;
using UnityEngine;

public class PoetryDataManager : DataManager<PoetryDataManager.PoetryData>
{
    [Serializable]
    public class PoetryData : InfoDataBase
    {
        public int fieldPlaceNum;
        
        public PoetryData()
        {
        }

        public PoetryData(int fieldPlaceNum, string title, string description, string[] imagesPath) : base(title, description, imagesPath)
        {
            this.fieldPlaceNum = fieldPlaceNum;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        dataFilePath = MainSettings.Instance.SettingsData.PoetryFilePath;
        base.Start();
        if (loadDataOnAwake)
        {
            //LoadAllSprites();
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
        _data = new List<PoetryData>();
        _data.Add(new PoetryData(0,"","",new[]{""}));
            
        base.CreateDataFileSample();
    }
}
