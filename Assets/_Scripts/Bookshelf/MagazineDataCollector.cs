using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineDataCollector : DataCollector
{
    protected override void CreateDataFileExample()
    {
        string path = dataFolderDir + "example.json";

        var jsonData = new JsonData();
        jsonData.DataString.Add(Keys.TitleKey, "Название");
        jsonData.DataString.Add(Keys.DateKey, "Дата");
        
        JsonHelper.ToJsonFile(path, jsonData);
    }
}
