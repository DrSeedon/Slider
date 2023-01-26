using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VScrollAndBar : MonoBehaviour
{
    public ScrollRect _rect;
    public Scrollbar _bar;
    
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

    public void ScrollToUp()
    {
        _rect.normalizedPosition = new Vector2(0, 1);
    }

    
}
