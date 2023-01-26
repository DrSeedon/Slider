using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UISwitchAnimator : MonoBehaviour
{
    public List<TogglePanelBind> panelBinds;

    public CanvasGroup togglesGroup;

    public bool switchPageOnEnable = true;
    public int startPage = 0;


    public TogglePanelBind curBind;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var b in panelBinds)
        {
            if (b.toggleToActivate != null)
                b.toggleToActivate.onValueChanged.AddListener((value) =>
                {
                    if (value)
                    {
                        curBind = b;
                        OpenPanel(curBind, false);
                    }
                    else
                    {
                        curBind = b;
                        ClosePanel(curBind, false);
                    }
                });
        }
    }

    public void CloseAll()
    {
        foreach (var bind in panelBinds)
        {
            bind.cv_groupsToHide
                .ForEach(cv => cv.DOFade(0, fadeInDuration));
            //.OnComplete(() => { targetBind.panel.SetActive(false); }));
        }
    }

    private void OnEnable()
    {
        if (switchPageOnEnable)
        {
            OpenPanel(startPage);
        }
    }

    public void OpenPanel(int value)
    {
        curBind = panelBinds[value];
        if (curBind.toggleToActivate != null) curBind.toggleToActivate.isOn = true;
        OpenPanel(curBind, false);
    }
    public void OpenPanelWithoutCloseOther(int value)
    {
        curBind = panelBinds[value];
        if (curBind.toggleToActivate != null) curBind.toggleToActivate.isOn = true;
        OpenPanel(curBind, false, false);
    }
    public void ClosePanel(int value)
    {
        curBind = panelBinds[value];
        curBind.toggleToActivate.isOn = false;
        ChangeFadePanel(curBind, false, 0, fadeInDuration);
    }

    public void OpenPanelImmediately(int value)
    {
        curBind = panelBinds[value];
        OpenPanel(curBind, true);
    }

    public float fadeInDuration = 0.5f;
    public float delayBetweenTransitions = 0.5f;
    public float fadeOutDuration = 0.5f;

    public async void ChangeFadePanel(TogglePanelBind targetBind, bool isImmediately, float fade, float fadeDir)
    {
        foreach (var cv in targetBind.cv_groupsToHide)
        {
            if (!isImmediately)
            {
                cv.DOFade(fade, fadeDir);
            }
            else
            {
                cv.alpha = fade;
            }
        }
    }

    public async void OpenPanel(TogglePanelBind targetBind, bool isImmediately, bool closeOther = true)
    {

        targetBind.OnShow?.Invoke();
        togglesGroup.interactable = false;
        if (targetBind.dataViewer)
            targetBind.dataViewer.SetDefaultPos();

        if (closeOther)
        {
            foreach (var bind in panelBinds)
            {
                ChangeFadePanel(bind, isImmediately, 0, fadeInDuration);
            }

            await Task.Delay((int) (delayBetweenTransitions * 1000));

            foreach (var bind in panelBinds)
            {
                bind.panel.SetActive(false);
            }
        }

        targetBind.panel.gameObject.SetActive(true);
        ChangeFadePanel(targetBind, isImmediately, 1, fadeOutDuration);

        if (targetBind.uiPanel)
            targetBind.uiPanel.OnShow?.Invoke();
        
        togglesGroup.interactable = true;

        await Task.Delay((int) (delayBetweenTransitions * 1000));

        return;

        targetBind.gmToDeactivate.ForEach(t => t.SetActive(false));
        targetBind.gmToActivate.ForEach(t => t.SetActive(true));
        //проявляем целевые элемент

        targetBind.cv_groupsToHide.ForEach(g =>
        {
            g.interactable = true;
            g.blocksRaycasts = true;
        });

        targetBind.cv_groupsToHide
            .ForEach(cv => cv.DOFade(1, fadeOutDuration)
                .OnComplete(() =>
                {
                    foreach (var bind in panelBinds)
                    {
                        if (bind != targetBind)
                        {
                            bind.cv_groupsToHide.ForEach(g =>
                            {
                                
                            });
                            //bind.panel.SetActive(false);
                        }
                    }

                    togglesGroup.interactable = true;
                }));


    }
    
    public async void ClosePanel(TogglePanelBind targetBind, bool isImmediately)
    {
        ChangeFadePanel(targetBind, isImmediately, 0, fadeOutDuration);

        if (targetBind.uiPanel)
            targetBind.uiPanel.OnHide?.Invoke();

        await Task.Delay((int) (delayBetweenTransitions * 1000));

        targetBind.OnHide?.Invoke();
        targetBind.panel.gameObject.SetActive(false);

        togglesGroup.interactable = true;
        return;

        targetBind.gmToDeactivate.ForEach(t => t.SetActive(false));
        targetBind.gmToActivate.ForEach(t => t.SetActive(true));
        //проявляем целевые элемент

        targetBind.cv_groupsToHide.ForEach(g =>
        {
            g.interactable = true;
            g.blocksRaycasts = true;
        });

        targetBind.cv_groupsToHide
            .ForEach(cv => cv.DOFade(1, fadeOutDuration)
                .OnComplete(() =>
                {
                    foreach (var bind in panelBinds)
                    {
                        if (bind != targetBind)
                        {
                            bind.cv_groupsToHide.ForEach(g =>
                            {
                                
                            });
                            //bind.panel.SetActive(false);
                        }
                    }

                    togglesGroup.interactable = true;
                }));


    }
}

[Serializable]
public class TogglePanelBind
{
    public GameObject panel;
    public UIPanel uiPanel => panel.GetComponent<UIPanel>();
    public DataManageViewer dataViewer;
    public List<CanvasGroup> cv_groupsToHide;
    public List<GameObject> gmToDeactivate;
    public List<GameObject> gmToActivate;
    public Toggle toggleToActivate;
    public UnityEvent OnShow;
    public UnityEvent OnHide;
}