using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
/// <summary>
/// Сбор данных из файла Json. И создание объектов из этих данных
/// </summary>
public class DataCollector : MonoBehaviour
{
    public List<Data> Datas = new List<Data>();

    public string[] extensions = {"*.jpeg", "*.jpg", "*.png", "*.bmp"};

    //что создаем и кто родитель
    public GameObject prefabDataElement;
    public GameObject parentDataElement;

    protected string dataFolderDir = Application.streamingAssetsPath;
    //к примеру /Bookshelf/Books/
    public string additionalDataFolderDir;

    public List<KeyForFolderImage> imageFolderKeyList = new List<KeyForFolderImage>(1);

    [HideInInspector]
    public List<Texture> imageList = new List<Texture>();

    public Texture defaultImage;


    public UnityEvent EventAfterAll;

    void Start()
    {
        StartMethods();
    }

    public virtual void StartMethods()
    {
        dataFolderDir += additionalDataFolderDir;
        //Debug.Log(dataFolderDir + " " + name);
        CreateDataFileExample();

        CollectData();
        CreateElements();
        EventAfterAll?.Invoke();
    }

    public virtual void CollectData()
    {
        var dataDirs = FilesFilter.FilterFilesByExtension(dataFolderDir, new[] {"*.json"});
        List<string[]> imageDirsList = new List<string[]>();
        foreach (var imageFolderDir in imageFolderKeyList)
        {
            string path = dataFolderDir + imageFolderDir.dir;
            imageDirsList.Add(FilesFilter.FilterFilesByExtension(path, extensions));
        }
        foreach (var path in dataDirs)
        {
            if (Path.GetFileNameWithoutExtension(path) == "example")
                continue;
            var simpleData = JsonConvert.DeserializeObject<JsonData>(JsonHelper.FromJsonFile(path));
            imageList.Clear();
            foreach (string[] imageDirs in imageDirsList)
            {
                foreach (var imageDir in imageDirs)
                {
                    //ищем такое же название картинки как и название json файла
                    if (Path.GetFileNameWithoutExtension(path) == Path.GetFileNameWithoutExtension(imageDir))
                    {
                        var imageTexture = new Texture2D(2, 2);
                        byte[] pngBytes = File.ReadAllBytes(imageDir);
                        imageTexture.LoadImage(pngBytes);
                        imageList.Add(imageTexture);
                    }
                }
            }
            
            
            CreateData(simpleData, imageList);
        }
    }
    protected virtual void CreateData(JsonData jsonData, List<Texture> imageTextureList)
    {
        Data data = new Data();
        data.jsonData = jsonData;
        for (int i = 0; i < imageTextureList.Count; i++)
        {
            data.DataTextures.Add(imageFolderKeyList[i].key.ToString(), imageTextureList[i]);
        }

        if (imageTextureList.Count == 0)
        {
            if (defaultImage != null) data.DataTextures.Add(imageFolderKeyList[0].key.ToString(), defaultImage);
        }
        Datas.Add(data);
    }

    protected virtual void CreateElements()
    {
        foreach (var data in Datas)
        {
            var obj = Instantiate(prefabDataElement, parentDataElement.transform);
            obj.SetActive(true);
            var dataElement = obj.GetComponent<DataElement>();
            dataElement.SetData(data);
        }
    }

    protected virtual void CreateDataFileExample()
    {
        string path = dataFolderDir + "example.json";
        var jsonData = new JsonData();
        jsonData.DataString.Add(Keys.TitleKey, "Название");
        jsonData.DataString.Add(Keys.DescriptionKey, "Описание");
        jsonData.DataString.Add(Keys.DateKey, "Дата");
        jsonData.DataString.Add(Keys.TimeKey, "Время");
        jsonData.DataString.Add(Keys.AuthorKey, "Автор");
        jsonData.DataString.Add(Keys.AgeKey, "Год");
        jsonData.DataString.Add(Keys.ReleaseKey, "Выпуск");
        List<string> listString = new List<string>
        {
            "1",
            "2",
            "3"
        };
        jsonData.DataListString.Add(Keys.GenreKey, listString);
        JsonHelper.ToJsonFile(path, jsonData);
    }

    

}

[Serializable]
public class KeyForFolderImage
{
    public string dir;
    public Keys.KeysImage key;
}
