using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UIPanels;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelSwitchManager : MonoBehaviour
{
    public CanvasGroup toggleGroup;
    public List<PanelToggleBind> panelToggleBinds;

    public bool showHideToggles = true;
    public bool hideAllPanelsOnStart;
    public bool openPanelOnStart;
    public int bindId;
    
    private PanelToggleBind _curBind;

    private void Awake()
    {
        //присваиваем событиям туглов корутины анимаций открытия/закрытия панелей
        foreach (var bind in panelToggleBinds)
        {
            bind.toggle.OnSelect.AddListener(()=>StartCoroutine(OpenPanelAnimation(bind)));
            bind.toggle.OnDeselect.AddListener(()=>StartCoroutine(ClosePanelAnimation(bind)));
            if (bind.panel.closeButton)
            {
                //присваиваем кнопке закрытия панели => выключение тугла
                bind.panel.closeButton.onClick.AddListener(()=>bind.toggle.ToggleOff());
            }
        }
    }

    private void Start()
    {
        if (hideAllPanelsOnStart)
        {
            
        }

        if (openPanelOnStart)
        {
            _curBind = panelToggleBinds[bindId];
            StartCoroutine(OpenPanelAnimation(_curBind));
        }
    }

    public IEnumerator OpenPanelAnimation(PanelToggleBind _bind)
    {
        //прячем все туглы
        if(showHideToggles)
            yield return StartCoroutine(HideTogglesAnimation());
        //показываем панель
        yield return StartCoroutine(_bind.panel.ShowPanelRoutine());
    }
    public IEnumerator ClosePanelAnimation(PanelToggleBind _bind)
    {
        //прячем панель
        yield return StartCoroutine(_bind.panel.HidePanelRoutine());
        //показываем все туглы
        if(showHideToggles)
            yield return StartCoroutine(ShowTogglesAnimation());
    }

    public IEnumerator ShowTogglesAnimation()
    {
        //блочим туглы, чтобы не сломать анимацию перехода
        toggleGroup.interactable = false;
        //показываем туглы
        foreach (var bind in panelToggleBinds)
        {
            StartCoroutine(bind.toggle.ShowAnimation());
        }
        //дожидаемся пока не спрячется самый последний тугл
        yield return new WaitForSeconds(
            panelToggleBinds
                .Select(p => p.toggle.animDuration)
                .Max()
        );
        //разблокируем туглы
        toggleGroup.interactable = true;
    }
    public IEnumerator HideTogglesAnimation()
    {
        //блочим туглы, чтобы не сломать анимацию перехода
        toggleGroup.interactable = false;
        //прячем туглы
        foreach (var bind in panelToggleBinds)
        {
            StartCoroutine(bind.toggle.HideAnimation());
        }
        //дожидаемся пока не спрячется самый последний тугл
        yield return new WaitForSeconds(
            panelToggleBinds
                .Select(p => p.toggle.animDuration)
                .Max()
            );
    }
}

[Serializable]
public class PanelToggleBind
{
    public ToggleBehaviourController toggle;
    public UIPanelBaseController panel;
}