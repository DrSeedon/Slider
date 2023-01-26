using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данные для одной единицы данных (книга, журнал, новость)
/// </summary>
[Serializable]
public class Data
{
    public JsonData jsonData;
    public Dictionary<string, Texture> DataTextures = new Dictionary<string, Texture>();
}

/// <summary>
/// Данные строк, используем для Json
/// <param name="DataString">Данные строки типа ключ значение</param>
/// <param name="DataListString"> Данные массива строк типа ключ значение</param>
/// </summary>
[Serializable]
public class JsonData
{
    public Dictionary<string, string> DataString = new Dictionary<string, string>();
    public Dictionary<string, List<string>> DataListString = new Dictionary<string, List<string>>();
}

/// <summary>
/// Ключи для словарей
/// </summary>
[Serializable]
public class Keys
{
    public static string TitleKey = KeysString.Title.ToString();
    public static string DescriptionKey = KeysString.Description.ToString();
    public static string DescriptionAdditionalKey = KeysString.DescriptionAdditional.ToString();
    public static string DateKey = KeysString.Date.ToString();
    public static string TimeKey = KeysString.Time.ToString();
    public static string AuthorKey = KeysString.Author.ToString();
    public static string AgeKey = KeysString.Age.ToString();
    public static string ReleaseKey = KeysString.Release.ToString();
    public static string GenreKey = KeysString.Genre.ToString();

    public static string ImageKey = KeysImage.Image.ToString();
    public static string BigImageKey = KeysImage.BigImage.ToString();
    public static string QRCodeKey = KeysImage.QRCode.ToString();

    public enum KeysString
    {
        Title,
        Description,
        DescriptionAdditional,
        Date,
        Time,
        Author,
        Age,
        Release,
        Genre
    }
    public enum KeysImage
    {
        Image,
        BigImage,
        QRCode
    }

}

