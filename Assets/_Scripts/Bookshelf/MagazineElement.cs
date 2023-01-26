using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineElement : DataElement
{
    public override void SetData(Data data)
    {
        this.data = data;
        image.texture = data.DataTextures[Keys.ImageKey];
        titleText.text = data.jsonData.DataString[Keys.TitleKey];
        dateText.text = data.jsonData.DataString[Keys.DateKey];
    }
}
