using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookViewer : DataViewer
{
    public static BookViewer Instance { get; private set; }
    protected virtual void Awake() => Instance = this;

    public GameObject AuthorInfo;
    public GameObject TitleInfo;
    public GameObject GenreInfo;
    public GameObject DescriptionInfo;

    public VerticalLayoutGroup descriptionVerticalLayoutGroup;

    public int ShowingId = 0;
    public int OldShowingId = 0;
    public override void ShowData(Data data)
    {
        if (OldShowingId == ShowingId)
            return;
        OldShowingId = ShowingId;
        
        
        GenreInfo.SetActive(true);
        AuthorInfo.SetActive(true);
        TitleInfo.SetActive(true);
        DescriptionInfo.SetActive(true);
        
        
        image.texture = data.DataTextures[Keys.ImageKey];
        //descriptionText.text = "";


        if (data.jsonData.DataString[Keys.TitleKey] != "")
            titleText.text = data.jsonData.DataString[Keys.TitleKey];
        else
            TitleInfo.SetActive(false);
        
        if (data.jsonData.DataString[Keys.AuthorKey] != "")
            authorText.text = data.jsonData.DataString[Keys.AuthorKey];
        else
            AuthorInfo.SetActive(false);
        
        if (data.jsonData.DataString[Keys.DescriptionKey] != "")
            descriptionText.text = data.jsonData.DataString[Keys.DescriptionKey] + "\t \t";
        else
            DescriptionInfo.SetActive(false);
        
        if (data.jsonData.DataListString[Keys.GenreKey].Count > 0)
            genreText.text = GenreToRus(data.jsonData.DataListString[Keys.GenreKey][0]);
        else
            GenreInfo.SetActive(false);
        
        Canvas.ForceUpdateCanvases(); // *
        descriptionVerticalLayoutGroup.enabled = false; // **
        descriptionVerticalLayoutGroup.enabled = true;
    }


    public string GenreToRus(string genreString)
    {
        Enum.TryParse(genreString, out Genre genre);
        switch (genre)
        {
            case Genre.Artistic:
                return "Художественная";
            case Genre.Industry:
                return "Отраслевая";
            case Genre.Poetry:
                return "Поэзия";
            case Genre.LocalHistory:
                return "Краеведение";
            case Genre.ForParentsAndChildren:
                return "Для родителей и детей";
        }

        return null;
    }

}
