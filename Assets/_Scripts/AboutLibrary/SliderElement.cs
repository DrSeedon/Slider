using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderElement : DataElement
{
    public override void SetData(Data data)
    {
        this.data = data;
        image.texture = data.DataTextures[Keys.ImageKey];
    }
}
