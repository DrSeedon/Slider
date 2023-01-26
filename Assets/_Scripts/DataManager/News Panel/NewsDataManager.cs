using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class NewsDataManager : DataManager<NewsDataManager.NewsData>
    {
        [Serializable]
        public class NewsData : InfoDataBase
        {
            public string mainImagePath;
            public string qrImagePath;

            public string[] sliderTitles;
            [NonSerialized] public Sprite mainImageSprite;
            [NonSerialized] public Sprite qrCodeSprite;

            public NewsData() : base()
            {
            }

            public NewsData(string title, string description, string[] imagesPath, string mainImagePath, string qrImagePath, string[] sliderTitles) : base(title, description, imagesPath)
            {
                this.mainImagePath = mainImagePath;
                this.qrImagePath = qrImagePath;
                this.sliderTitles = sliderTitles;
            }
        }

        internal override void Start()
        {
            dataFilePath = MainSettings.Instance.SettingsData.NewsFilePath;

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
                d.qrCodeSprite = QRGenerator.GenerateQRSprite(d.qrImagePath);
                
                d.mainImageSprite = FileUtility.LoadSpriteFromFile(d.mainImagePath);
                d.sliderSprites = FileUtility.LoadSprites(d.imagesPath).ToArray();
            }
        }

        [ContextMenu("Create Data File Sample")]
        public void CreateDataFileSample()
        {
            _data = new List<NewsData>();
            _data.Add(new NewsData("", "", new[] {""}, "", "", new[] {""}));

            base.CreateDataFileSample();
        }
    }
}