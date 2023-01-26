using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DataViewer : MonoBehaviour
{
    public RawImage image;
    public RawImage bigImage;
    public RawImage qrCodeImage;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text descriptionAdditionalText;
    public TMP_Text authorText;
    public TMP_Text ageText;
    public TMP_Text dateText;
    public TMP_Text timeText;
    public TMP_Text releaseText;
    public TMP_Text genreText;

    public virtual void ShowData(Data data)
    {
        image.texture = data.DataTextures[Keys.ImageKey];
        bigImage.texture = data.DataTextures[Keys.BigImageKey];
        titleText.text = data.jsonData.DataString[Keys.TitleKey];
        descriptionText.text = data.jsonData.DataString[Keys.DescriptionKey];
        descriptionAdditionalText.text = data.jsonData.DataString[Keys.DescriptionAdditionalKey];
        authorText.text = data.jsonData.DataString[Keys.AuthorKey];
        ageText.text = data.jsonData.DataString[Keys.AgeKey];
        dateText.text = data.jsonData.DataString[Keys.DateKey];
        timeText.text = data.jsonData.DataString[Keys.TimeKey];
        releaseText.text = data.jsonData.DataString[Keys.ReleaseKey];
        genreText.text = data.jsonData.DataString[Keys.GenreKey];
    }

}
