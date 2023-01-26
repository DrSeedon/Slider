using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AFKManager : MonoBehaviour
{
    //àâòîìàòè÷åñêîå ñêðûâàíèå CanvasGroup ïðè îòñóòñòâèè êëèêîâ
    public bool autoHide;
    public float timeWasted = 0;
    public bool m_isHolding = false;

    public List<DelayInvoke> DelayInvokes;

    [Serializable]
    public class DelayInvoke
    {
        public float hideDelay = 5f;
        public UnityEvent Event;
    }

    void Start()
    {
        
    }

    void Update()
    {
        timeWasted += Time.deltaTime;
        foreach (var delayInvoke in DelayInvokes)
        {
            if (timeWasted > delayInvoke.hideDelay && !m_isHolding && autoHide)
            {
                delayInvoke.Event?.Invoke();
            }
        }

        if (Input.touchCount > 0)
        {
            m_isHolding = true;
            timeWasted = 0;
        }
        else
            m_isHolding = false;
    }
}