using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI.Extensions;

public class BookCollector : DataCollector
{
    protected override void CreateDataFileExample()
    {
        string path = dataFolderDir + "example.json";
        
        var jsonData = new JsonData();
        jsonData.DataString.Add(Keys.TitleKey, "Название");
        jsonData.DataString.Add(Keys.DescriptionKey, "Описание");
        jsonData.DataString.Add(Keys.AuthorKey, "Автор");
        jsonData.DataString.Add(Keys.AgeKey, "Год");
        
        List<string> genre = new List<string>();
        genre.Add(Genre.Artistic.ToString());
        genre.Add(Genre.Industry.ToString());
        genre.Add(Genre.Poetry.ToString());
        genre.Add(Genre.LocalHistory.ToString());
        genre.Add(Genre.ForParentsAndChildren.ToString());
        jsonData.DataListString.Add(Keys.GenreKey, genre);
        
        JsonHelper.ToJsonFile(path, jsonData);
    }

    protected override void CreateElements()
    {
        foreach (var data in Datas)
        {
            var obj = Instantiate(prefabDataElement, parentDataElement.transform);
            obj.SetActive(true);
            var dataElement = obj.GetComponent<DataElement>();
            dataElement.SetData(data);
            
            BookshelfFilter.Instance.booksElements.Add(dataElement);
        }
    }
}
