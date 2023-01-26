using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class FileUtility
{
    public static List<string> ImgAvailableExtensions = new List<string>() { ".jpeg", ".jpg", ".png",".bmp"};
    public static List<string> VidAvailableExtensions = new List<string>() { ".avi", ".mov", ".mp4", ".mpeg", ".webm", ".wmv" };
    
    /// <summary>
    /// Запись в файл формата json
    /// </summary>
    /// <param name="data">список</param>
    /// <param name="path">путь к файлу</param>
    public static void WriteDataInFile<T>(List<T> data, string path)
    {
        var json = JsonConvert.SerializeObject(data,Formatting.Indented);
        if (!File.Exists(path)) File.Create(path);
        File.WriteAllText(path, json);
    }
    /// <summary>
    /// возвращает список объектов T
    /// из файла формата json
    /// </summary>
    /// <param name="path">путь к файлу</param>
    public static List<T> ReadListDataFromFile<T>(string path)
    {
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);

            var list = JsonConvert.DeserializeObject<List<T>>(json);

            return list;
        }
        Debug.Log("F001: Указанный файл не существует!");
        return null;
        
    }
    /// <summary>
    /// возвращает объект T
    /// из файла формата json
    /// </summary>
    /// <param name="path">путь к файлу</param>
    public static T ReadDataFromFile<T>(string path)
    {
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);

            var data = JsonConvert.DeserializeObject<T>(json);

            return data;
        }
        Debug.Log("F001: Указанный файл не существует!");

        return default;
    }
    /// <summary>
    /// Загрузка url видео-файлов из указанной папки path
    /// в указанный кольцевой список urls
    /// </summary>
    /// <param name="path">путь к папке с видео</param>
    /// <param name="urls">кольцевой список</param>
    public static CircularLinkedList<string> LoadVideoListFromDirectory(string path)
    {
        CircularLinkedList<string> urls = new CircularLinkedList<string>();
        if (Directory.Exists(path))
        {
            foreach (var f in Directory.EnumerateFiles(path))
            {
                if (VidAvailableExtensions.Contains(new FileInfo(f).Extension))
                {
                    urls.Add(f);
                }
                else
                {
                    Debug.Log("FV001: Неподдерживаемый формат файла: " + f);
                }
            }
        }
        else
        {
            Debug.Log("FV002: Указанная папка не существует: " + path);
        }
        return urls;
    }

    
    public static List<Texture2D> LoadTexturesFromFolder(string path)
    {
        byte[] data;
        List<Texture2D> textures = new List<Texture2D>();
        
        var files = Directory.EnumerateFiles(path);
       
        foreach (var f in files)
        {
            if(!ImgAvailableExtensions.Contains(new FileInfo(f).Extension))
                continue;
            
            data = File.ReadAllBytes(f);
            Texture2D texture = new Texture2D(2,2);
            if (texture.LoadImage(data))
            {
                textures.Add(texture);
            }
        }
        Debug.Log("FU: Загружено текстур - " + textures.Count);
        return textures;
    }
    public static List<Sprite> LoadSpritesFromFolder(string path)
    {
        byte[] data;
        List<Sprite> loadedSprites = new List<Sprite>();
        
        var files = Directory.EnumerateFiles(path);
       
        foreach (var f in files)
        {
            if(!ImgAvailableExtensions.Contains(new FileInfo(f).Extension))
                continue;
            
            data = File.ReadAllBytes(f);
            Texture2D texture = new Texture2D(2,2);
            if (texture.LoadImage(data))
            {
                loadedSprites.Add(
                    Sprite.Create(
                        texture,
                        new Rect(0, 0, texture.width, texture.height),
                        new Vector2(0, 0),
                        100f, 0, SpriteMeshType.Tight)
                    );
            }
        }
        Debug.Log("FU: Загружено спрайтов - " + loadedSprites.Count);
        return loadedSprites;
    }
    public static List<Sprite> LoadSprites(string[] path)
    {
        byte[] data;
        List<Sprite> loadedSprites = new List<Sprite>();
        
        foreach (var f in path)
        {
            if(!File.Exists(f) || !ImgAvailableExtensions.Contains(new FileInfo(f).Extension))
                continue;
            
            data = File.ReadAllBytes(f);
            Texture2D texture = new Texture2D(2,2);
            if (texture.LoadImage(data))
            {
                loadedSprites.Add(
                    Sprite.Create(
                        texture,
                        new Rect(0, 0, texture.width, texture.height),
                        new Vector2(0, 0),
                        100f, 0, SpriteMeshType.Tight)
                );
            }
        }
        
        return loadedSprites;
    }
    public static Sprite LoadSpriteFromFile(string path)
    {
        if (!File.Exists(path)) return null;
        
        byte[] data;
        
        if (!ImgAvailableExtensions.Contains(new FileInfo(path).Extension))
            return null;

        data = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(data))
        {
            return Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0, 0),
                100f, 0, SpriteMeshType.Tight);
        }

        return null;
    }



    public static void WriteListDataInFile<T>(List<T> data, string _path)
    {
        var json = JsonConvert.SerializeObject(data,Formatting.Indented);
        if (!File.Exists(_path)) File.Create(_path);
        File.WriteAllText(_path, json);
    }

    
}
