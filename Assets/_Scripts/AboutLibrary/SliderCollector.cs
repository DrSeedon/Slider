using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI.Extensions;

public class SliderCollector : DataCollector
{
    public HorizontalScrollSnap horizontalScrollSnap;
    private UI_InfiniteScroll uiInfiniteScroll;
    private DotViewer dotViewer;

    public override void StartMethods()
    {
        uiInfiniteScroll = horizontalScrollSnap.gameObject.GetComponent<UI_InfiniteScroll>();
        dotViewer = horizontalScrollSnap.gameObject.GetComponent<DotViewer>();
        base.StartMethods();
    }

    public override void CollectData()
    {
        List<string[]> imageDirsList = new List<string[]>();
        foreach (var imageFolderDir in imageFolderKeyList)
        {
            string path = dataFolderDir + imageFolderDir.dir;
            imageDirsList.Add(FilesFilter.FilterFilesByExtension(path, extensions));
        }
        foreach (string[] imageDirs in imageDirsList)
        {
            foreach (var imageDir in imageDirs)
            {
                imageList.Clear();
                var imageTexture = new Texture2D(2, 2);
                byte[] pngBytes = File.ReadAllBytes(imageDir);
                imageTexture.LoadImage(pngBytes);
                imageList.Add(imageTexture);
                CreateData(imageList);
            }
        }
    }

    protected void CreateData(List<Texture> imageTextureList)
    {
        Data data = new Data();
        for (int i = 0; i < imageTextureList.Count; i++)
        {
            data.DataTextures.Add(imageFolderKeyList[i].key.ToString(), imageTextureList[i]);
        }

        Datas.Add(data);
    }

    protected async override void CreateElements()
    {
        await Task.Delay(1000);
        foreach (var data in Datas)
        {
            var obj = Instantiate(prefabDataElement, parentDataElement.transform);
            obj.SetActive(true);
            var dataElement = obj.GetComponent<DataElement>();
            dataElement.SetData(data);
            horizontalScrollSnap.AddChild(obj);
        }
        uiInfiniteScroll.Init();
        dotViewer.count = Datas.Count;
    }

    protected override void CreateDataFileExample()
    {
        
    }
}