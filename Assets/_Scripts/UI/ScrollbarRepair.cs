using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarRepair : MonoBehaviour
{
    public ScrollRect _rect;
    public Scrollbar _bar;

    public bool setDefaultOnDisable = true;
    public float defaultValue = 1;
    // Start is called before the first frame update
    void Start()
    {
        _rect.onValueChanged.AddListener((value) =>
        {
            _bar.value = _rect.verticalNormalizedPosition;
        });
        _bar.onValueChanged.AddListener((value) =>
        {
            _rect.verticalNormalizedPosition = _bar.value;
        });
    }

    private void OnDisable()
    {
        SetDefaultValue();
    }
    public void SetDefaultValue()
    {
        _bar.value = defaultValue;
        _rect.verticalNormalizedPosition = 1;
    }

    public void ScrollToUp()
    {
        _rect.normalizedPosition = new Vector2(0, 1);
    }
}
