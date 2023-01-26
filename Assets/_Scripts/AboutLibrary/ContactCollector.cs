using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactCollector : DataCollector
{
    public ContactViewer ContactViewer;

    public override void StartMethods()
    {
        dataFolderDir += additionalDataFolderDir;
        CreateDataFileExample();

        CollectData();
        foreach (var data in Datas)
        {
            ContactViewer.ShowData(data);
        }
    }

    protected override void CreateDataFileExample()
    {
        string path = dataFolderDir + "example.json";

        var jsonData = new JsonData();

        jsonData.DataString.Add(Keys.TitleKey, "Красноярск, ул. Урванцева, 23 "
                                               + "Вторник-суббота: <color=#D50745>с 10.00 до 20.00</color> "
                                               + "Воскресенье: <color=#D50745>с 10.00 до 18.00</color> "
                                               + "Выходной: <color=#D50745>понедельник</color> "
                                               + "Санитарный день: <color=#D50745>последний четверг месяца</color> ");

        jsonData.DataString.Add(Keys.DescriptionKey, "<line-height=75%><color=#D50745>Белохвостова "
                                                     + "Елена Максимовна</color> "
                                                     + "<size=85%>Директор МБУК «ЦБС им.Горького»</size> "
                                                     + "8(391) 227 - 64 - 47");


        jsonData.DataString.Add(Keys.DescriptionAdditionalKey, "<line-height=75%><color=#D50745>Сахарова "
                                                               + "Любовь Александровна</color> "
                                                               + "<size=85%>Руководитель Главного управления культуры г.Красноярска</size> "
                                                               + "8(391) 222 - 39 - 88");

        JsonHelper.ToJsonFile(path, jsonData);
    }
}