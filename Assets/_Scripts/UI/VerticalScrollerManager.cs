using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class VerticalScrollerManager : MonoBehaviour
{
    private UIVerticalScroller _vScroller;
    [Header("Prefabs")]
    public GameObject leftPagePrefab;
    public GameObject rightPagePrefab;

    private void Awake()
    {
        _vScroller = GetComponent<UIVerticalScroller>();
    }

    public void OnScroll()
    {
        
    }

    public void SetChildrens()
    {
        
    }
}
