using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookElement : DataElement
{
    public Image frame;
    public float opacity;
    public void ButtonClicked(int id)
    {
        BookViewer.Instance.ShowingId = id;
        BookViewer.Instance.ShowData(data);
    }

    public override void SetData(Data data)
    {
        this.data = data;
        image.texture = data.DataTextures[Keys.ImageKey];
    }
    
    public void ChangeOpaсityFrame(float opacity)
    {
        opacity = Mathf.InverseLerp(0.7f, 1f, opacity);

        opacity = opacity > 0.4f ? 1 : 0;


        var color = frame.color;
        color.a = opacity;
        frame.color = color;
        
        this.opacity = opacity;
    }
}
