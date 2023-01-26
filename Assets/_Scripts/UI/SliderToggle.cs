using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderToggle : MonoBehaviour, IPointerClickHandler
{
    public Toggle toggle;

    public UnityEvent onClick;
    // Start is called before the first frame update
    void Awake()
    {
        toggle = GetComponent<Toggle>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }
}
