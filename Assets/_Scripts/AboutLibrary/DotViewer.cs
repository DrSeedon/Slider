using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotViewer : MonoBehaviour
{
    public List<Image> dotImages = new List<Image>();
    public Sprite DotOn;
    public Sprite DotOff;
    public int count;
    public int id;
    public void SetDot(int index)
    {
        id = index;
        //заполнение точек как процентный бар
        if (index < count / 3)
        {
            ChangeDotAll(false);
            ChangeDot(1, true);
            return;
        }

        if (index > count / 3 * 2)
        {
            ChangeDotAll(false);
            ChangeDot(0, true);
            return;
        }

        ChangeDotAll(false);
        ChangeDot(2, true);
        
    }

    private void ChangeDotAll(bool value)
    {
        for (int i = 0; i < dotImages.Count; i++)
        {
            ChangeDot(i, value);
        }
    }

    private void ChangeDot(int id, bool value)
    {
        dotImages[id].sprite = value ? DotOn : DotOff;
        dotImages[id].rectTransform.sizeDelta = value ? new Vector2(53, 53) : new Vector2(33, 33);
    }

}
