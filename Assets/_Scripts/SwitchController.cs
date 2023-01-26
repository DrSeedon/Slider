using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private RectMask2D mask;

    public RawImage firstRawImage;
    public RawImage secondRawImage;


    [SerializeField] private float width = 1740;

    public List<PictureBeforeAfter> PicturesBeforeAfter = new List<PictureBeforeAfter>();
    public int index;

    // Update is called once per frame
    void Update()
    {
        mask.padding = new Vector4(width * slider.value, 0, 0, 0);
        //Debug.Log(width);
    }

    public void ShowNext()
    {
        if (index < PicturesBeforeAfter.Count - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
        Show(index);
    }

    public void ShowPrevious()
    {
        if (index > 0)
        {
            index--;
        }
        else
        {
            index = PicturesBeforeAfter.Count - 1;
        }
        Show(index);
    }

    public void Show(int id)
    {
        firstRawImage.texture = PicturesBeforeAfter[id].imageBefore;
        secondRawImage.texture = PicturesBeforeAfter[id].imageAfter;
        slider.value = 0.5f;
        Debug.Log("next " + id);
    }

    [Serializable]
    public class PictureBeforeAfter
    {
        public Texture imageBefore;
        public Texture imageAfter;
    }
}