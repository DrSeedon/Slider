using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DataElement : MonoBehaviour
{
    public Data data;

    public RawImage image;
    public RawImage bigImage;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text descriptionAdditionalText;
    public TMP_Text authorText;
    public TMP_Text ageText;
    public TMP_Text dateText;
    public TMP_Text timeText;
    public TMP_Text releaseText;
    
    public Button button;

    private void Start()
    {
        if (button != null) button.onClick.AddListener(ButtonClicked);
    }

    public virtual void ButtonClicked()
    {
    }

    public virtual void SetData(Data data)
    {
        this.data = data;
        image.texture = data.DataTextures[Keys.ImageKey];
        titleText.text = data.jsonData.DataString[Keys.TitleKey];
    }
}
