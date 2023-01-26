using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Toggle = UnityEngine.UI.Toggle;

[RequireComponent(typeof(Toggle))]
public class ToggleBehaviourController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isLockIfActive = true;
    public bool turnOnStart = false;
    public ToggleGroup _group;
    public Image background;
    public Image checkmark;

    public TMP_Text label;
    public Color colorTextOn;
    public Color colorTextOff;

    //доп.объекты которые нужно прятать, когда тугл выделен
    public Image[] objToHide;

    public UnityEvent OnSelect;
    public UnityEvent OnDeselect;

    private RectTransform _rect;
    private Toggle _toggle;

    public Toggle toggle => _toggle;

    // Start is called before the first frame update
    void Awake()
    {
        if (!_group) _group = GetComponentInParent<ToggleGroup>();
        _toggle = GetComponent<Toggle>();
        _rect = GetComponent<RectTransform>();

        //set toggle group
        if (_group)
            _toggle.group = _group;

        _toggle.onValueChanged.AddListener(OnValueChange);
    }

    private void Start()
    {
        if (turnOnStart)
            OnValueChange(turnOnStart);
    }

    private async void OnValueChange(bool value)
    {
        if (value)
        {
            if (label != null) label.color = colorTextOn;
            checkmark.gameObject.SetActive(true);
            background.gameObject.SetActive(false);
            if (isLockIfActive)
                Disable();

            if (objToHide != null)
                foreach (var o in objToHide)
                {
                    o.gameObject.SetActive(false);
                }

            OnSelect?.Invoke();
        }
        else
        {
            if (label != null) label.color = colorTextOff;
            background.gameObject.SetActive(true);
            checkmark.gameObject.SetActive(false);
            Enable();

            if (objToHide != null)
                foreach (var o in objToHide)
                {
                    o.gameObject.SetActive(true);
                }

            OnDeselect?.Invoke();
        }
    }


    public void ToggleOn()
    {
        _toggle.isOn = true;
    }

    public void ToggleOff()
    {
        _toggle.isOn = false;
    }

    public bool AnyTogglesOn()
    {
        return _group.AnyTogglesOn();
    }

    public void Disable() => _toggle.interactable = false;
    public void Enable() => _toggle.interactable = true;


    [Space] [Header("Animation")] public Vector2 showPos;
    public Vector2 hidePos;
    public float animDuration = 0.25f;
    public bool isAnimated = true;

    public IEnumerator ShowAnimation()
    {
        if (!isAnimated) yield break;
        _rect.DOAnchorPos(showPos, animDuration);
        yield return new WaitForSeconds(animDuration);
    }

    public IEnumerator HideAnimation()
    {
        if (!isAnimated) yield break;
        _rect.DOAnchorPos(hidePos, animDuration);
        yield return new WaitForSeconds(animDuration);
    }

    public bool isPointerResponse = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_toggle.interactable) return;
        if (!isPointerResponse) return;
        if (label != null) label.color = colorTextOn;
        checkmark.gameObject.SetActive(true);
        background.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_toggle.interactable) return;
        if (!isPointerResponse) return;
        if (label != null) label.color = colorTextOff;
        background.gameObject.SetActive(true);
        checkmark.gameObject.SetActive(false);
    }
}