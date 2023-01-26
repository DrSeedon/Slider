using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainSettings : MonoBehaviour
{
    private static MainSettings _instance;

    public static MainSettings Instance => _instance;

    [SerializeField]
    private string settingsFileName = "Settings.cfg";

    //полный путь к файлу настроек
    private string _settingsFullPath;

    private SettingsData _settingsData;
    public SettingsData SettingsData => _settingsData;
    
    // Start is called before the first frame update
    void Awake()
    {
        //чтение файла настроек
        _settingsFullPath = Path.Combine(Application.streamingAssetsPath, settingsFileName);
        _settingsData = FileUtility.ReadDataFromFile<SettingsData>(_settingsFullPath);
        
        //инициализация
        if (!_instance)
            _instance = this;

        Input.multiTouchEnabled = false;
    }

    
}

[Serializable]
public class SettingsData
{
    /// <summary>
    /// путь к файлу "О Светлове"
    /// </summary>
    public string AboutAuthorFilePath;

    /// <summary>
    /// путь к файлу "Советский район"
    /// </summary>
    public string DistrictFilePath;
    
    /// <summary>
    /// путь к файлу "Афиша"
    /// </summary>
    public string PosterFilePath;
    
    /// <summary>
    /// путь к файлу "Афиша"
    /// </summary>
    public string NewsFilePath;
    
    /// <summary>
    /// путь к файлу "проекты и объединения"
    /// </summary>
    public string ProjectFilePath;
    /// <summary>
    /// путь к файлу "Поэзия"
    /// </summary>
    public string PoetryFilePath;
}