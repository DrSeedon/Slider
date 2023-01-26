using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactViewer : DataViewer
{
    public override void ShowData(Data data)
    {
        titleText.text = data.jsonData.DataString[Keys.TitleKey];
    }
}
