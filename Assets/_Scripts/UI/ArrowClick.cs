using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArrowClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject image;

    public void OnPointerDown(PointerEventData eventData)
    {
        image.transform.DOScale(0.7f, 0.5f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.transform.DOScale(1f, 0.5f);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
