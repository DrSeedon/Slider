using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PoetryViewer: MonoBehaviour
{
    public PoetryDataManager poetryDataManager;

    public PoetryPage poetryPageTop; 
    public PoetryPage poetryPageBot; 
    // Start is called before the first frame update
    void Awake()
    {
        poetryDataManager.onDataLoaded += SetDataView;
    }

    private void SetDataView()
    {
        //стихи для верхнего поля
        var topPoetries = poetryDataManager.data.FindAll(t => t.fieldPlaceNum == 0);
        //стихи для нижнего поля
        var botPoetries = poetryDataManager.data.FindAll(t => t.fieldPlaceNum == 1);

        poetryPageTop.SetData(topPoetries);
        poetryPageBot.SetData(botPoetries);
    }
}
