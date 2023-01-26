using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public static class JsonHelper
{
    //??????? ?????? ??? ?????? ? json
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

    public static string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    public static void ToJsonFile(string path, object obj)
    {
        string jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
        //string jsonString = JsonUtility.ToJson(obj, true);
        try
        {
            // Create the file, or overwrite if the file exists.
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(jsonString);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    public static void ToJsonFile(string path, string jsonString)
    {
        try
        {
            // Create the file, or overwrite if the file exists.
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(jsonString);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public static string FromJsonFile(string path)
    {
        string jsonString = "";
        try
        {
            // Open the stream and read it back.
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    jsonString += s;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return jsonString;
    }
}


