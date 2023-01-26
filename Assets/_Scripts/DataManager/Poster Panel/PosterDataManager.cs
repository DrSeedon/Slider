using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class PosterDataManager : DataManager<PosterDataManager.PosterData>
    {
        [Serializable]
        public class PosterData: InfoDataBase
        {
            public string date;
            [NonSerialized]
            public Sprite qrCodeSprite;
            
            public PosterData():base()
            {
                
            }
            public PosterData(string title, string description, string[] imagesPath, string date) : base(title, description, imagesPath)
            {
                this.date = date;
            }
        }

        internal override void Start()
        {
            dataFilePath = MainSettings.Instance.SettingsData.PosterFilePath;
            
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
                d.qrCodeSprite = QRGenerator.GenerateQRSprite(d.imagesPath[0]);
            }
        }
        
        [ContextMenu("Create Data File Sample")]
        public void CreateDataFileSample()
        {
            _data = new List<PosterData>();
            _data.Add(new PosterData("","",new[]{""},""));
            
            base.CreateDataFileSample();
        }
    }
}
