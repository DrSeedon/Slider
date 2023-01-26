using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManager
{
    /// <summary>
    /// Базовый класс сущности, содержащей основные свойства
    /// </summary>
    [Serializable]
    public class InfoDataBase
    {
        public string title;                        //заголовок
        public string description;                  //описание (основной текст)
        public string[] imagesPath;                 //пути к картинкам

        [NonSerialized]
        public Sprite[] sliderSprites;
        public InfoDataBase()
        {
        }

        public InfoDataBase(string title, string description, string[] imagesPath)
        {
            this.title = title;
            this.description = description;
            this.imagesPath = imagesPath;
        }
    }
}
