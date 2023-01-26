using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public TMP_Text dateText;
    public TMP_Text timeText;

    public DateTime curTime;
    void Awake()
    {
        
    }

    private string dayOfWeek;
    // Update is called once per frame
    void Update()
    {
        curTime = DateTime.Now;
        dayOfWeek = curTime.ToString("dddd");
        dateText.text =   dayOfWeek.Substring(0, 1).ToUpper() + dayOfWeek.Substring(1) + ", " +
            curTime.ToString("D");
        
        timeText.text = curTime.ToString("t");
    }
}
