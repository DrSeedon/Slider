using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibrariesPanelsController : MonoBehaviour
{
    public List<Animator> animators;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    private void Start()
    {
        OpenPanel(0);
    }

    public void CloseAllExcept(int id)
    {
        for (int i = 0; i < animators.Count; i++)
        {
            if (i != id)
                ClosePanel(i);
        }
    }

    public void OpenAllExcept(int id)
    {
        for (int i = 0; i < animators.Count; i++)
        {
            if (i != id)
                OpenPanel(i);
        }
    }
    
    public void OpenPanel(int id)
    {
        /* //если открываем уже открытый то закрываем и открываем другой
        if (animators[id].GetBool(IsOpen))
        {
            OpenAllExcept(id);
            return;
        }
        */
        CloseAllExcept(id);
        animators[id].SetBool(IsOpen, true);
    }
    public void ClosePanel(int id)
    {
        animators[id].SetBool(IsOpen, false);
    }
}
