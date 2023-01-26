using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HallElement : MonoBehaviour
{
    public Button buttonMap;
    private Button _button;
    public RawImage imageButtonMap;
    public RawImage image;

    public bool isActive = false;
    [HideInInspector] public HallManager HallManager;
    public float scrollValue;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Click);
        buttonMap.onClick.AddListener(Click);
    }

    public void Click()
    {
        HallManager.ChangeAll(false);
        HallManager.ScrollMove(scrollValue);
        ChangeState(true);
    }

    public void ChangeState(bool value)
    {
        isActive = value;
        image.gameObject.SetActive(isActive);
        imageButtonMap.gameObject.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
    }
}