using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DataManager
{
    public class DataManager<T> : MonoBehaviour
    {
        [SerializeField]
        protected List<T> _data;

        public List<T> data => _data;
        
        [SerializeField]
        protected string dataFilePath;
        
        [SerializeField] protected bool loadDataOnAwake = true;

        public Action onDataLoaded;
        internal virtual void Start()
        {
            if(loadDataOnAwake)
                LoadDataFromFile(dataFilePath);
        }

        public void LoadDataFromFile(string _path)
        {
            _data = FileUtility.ReadDataFromFile<List<T>>(Path.Combine(Application.streamingAssetsPath,_path));
        }

        public virtual void LoadAllSprites() { }
    
        
        public void CreateDataFileSample(string fileName = "Sample.json")
        {
            if(dataFilePath != "")
                FileUtility.WriteListDataInFile(_data,Path.Combine(Application.streamingAssetsPath,dataFilePath));
            else
                FileUtility.WriteListDataInFile(_data, Path.Combine(Application.streamingAssetsPath, fileName));
        }
    }

}