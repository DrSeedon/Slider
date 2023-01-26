using System;
using UnityEngine;
 
public class ShowFPS : MonoBehaviour
{
    public float FPS;

    private void Awake()
    {
        
    }

    void OnGUI()
    {
        FPS = 1.0f / Time.deltaTime;
        GUI.skin.label.fontSize = 30;
        GUI.skin.label.normal.textColor = Color.red;
        GUILayout.Label("FPS: " + (int)FPS);
    }
}
