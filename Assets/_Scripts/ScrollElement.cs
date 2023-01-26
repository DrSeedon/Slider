using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI.Extensions;

public class ScrollElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UIHorizontalScroller UIHorizontalScroller;
    public int id;

    public void Click()
    {
        UIHorizontalScroller.ScrollToID(id);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        UIHorizontalScroller.isHold = true;
        UIHorizontalScroller.isAuto = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UIHorizontalScroller.isHold = false;
    }
}
