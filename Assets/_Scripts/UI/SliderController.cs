using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

[RequireComponent(typeof(HorizontalScrollSnap))]
public class SliderController : MonoBehaviour
{
    [SerializeField] public HorizontalScrollSnap _snap;
    [SerializeField] public UI_InfiniteScroll _infiniteScroll;

    [Header("Sprites")]
    public List<Sprite> Sprites;
    [Header("Buttons")] 
    public Button _nextButton;
    public Button _prevButton;
    [Header("Other")]
    public Transform content;
    public ToggleGroup pagination;
    public GameObject pagePrefab;
    public GameObject togglePrefab;
    [Header("Title")] 
    public TMP_Text titleText;
    public List<string> titles;
    
    [Header("OnStart")] 
    public int pageOnStart = 0;
    public bool resetOnEnable = false;
    [Header("Toggle Behaviour")] 
    public bool onClick = false;
    public int toggleCount;
    public bool showCounter = false;
    public bool showToggles = true;
    
    private List<Toggle> _toggles = new List<Toggle>();
    public List<SliderPage> _pages = new List<SliderPage>();
    private int _curId;
    private bool isInfiniteInit = false;
    private void Start()
    {
        if (!_snap) _snap = GetComponent<HorizontalScrollSnap>();
        if (!_infiniteScroll) _infiniteScroll = GetComponent<UI_InfiniteScroll>();

        _infiniteScroll.Init();
        isInfiniteInit = true;
        
        _snap.StartingScreen = pageOnStart;
        _curId = pageOnStart;

        if (Sprites != null && Sprites.Count > 0)
        {
            Initialize();
        }
        //SelectScreen(pageOnStart);
        
    }

    public async void SetToDefault()
    {
        _infiniteScroll.SetDefaultSiblings();
        var speed = _snap.transitionSpeed;
            
        _snap.transitionSpeed = 20;
        if (_snap.ChildObjects != null && _snap.ChildObjects.Length > 0)
        {
            SelectScreen(_snap.ChildObjects.ToList().IndexOf(_pages[pageOnStart].gameObject));
        }
        _snap.transitionSpeed = speed;
    }
    
   
    public void Initialize()
    {
        _snap.OnSelectionPageChangedEvent.RemoveAllListeners();
        _snap.enabled = false;


        if (_nextButton)
        {
            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(NextPage);
        }

        if (_prevButton)
        {
            _prevButton.onClick.RemoveAllListeners();
            _prevButton.onClick.AddListener(PrevPage);
        }
        int realId= 0;
        int id1 = 0;

        if (titleText)
        {
            _snap.OnSelectionPageChangedEvent.AddListener((value)=>
            {
                _curId = value;
                if( _curId < titles.Count)
                    SetTitle(_snap.ChildObjects[_curId].GetComponent<SliderPage>().label);
                
                if (showCounter)
                {
                    realId = _curId+1 > Sprites.Count ? _curId - Sprites.Count + 1: _curId+1;
                    titleText.text = $"{realId}/{Sprites.Count}";
                }
            });
        }


        var spriteCount = -1;
        //если всего одна картинка, продублируем ее 2 раза, чтобы нормально работал бесконечный слайдер
        if (Sprites.Count == 1)
        {
            var sp = Sprites[0];
            Sprites.Add(sp);
            Sprites.Add(sp);
            Sprites.Add(sp);
            spriteCount = 1;
        }
        //если 2 картинки, продублируем их, чтобы нормально работал бесконечный слайдер
        if (Sprites.Count == 2)
        {
            var sp1 = Sprites[0];
            var sp2 = Sprites[1];
            Sprites.Add(sp1);
            Sprites.Add(sp2);
            spriteCount = 2;
        }
        
        
        
        foreach (var sprite in Sprites)
        {
            var page = Instantiate(pagePrefab, content).GetComponent<SliderPage>();
            page.image.sprite = sprite;
            if (titleText && titles != null && titles.Count == Sprites.Count) 
                page.label = titles[id1];
            
            page.initId = id1;
            page.curId = id1;
            _pages.Add(page);
            
            _snap.AddChild(page.gameObject);

            if (onClick && showToggles)
            {
                var toggle = Instantiate(togglePrefab, pagination.transform).GetComponent<SliderToggle>();
                toggle.toggle.group = pagination;
                _toggles.Add(toggle.toggle);
                toggle.onClick.AddListener(() =>
                {
                    var id = _toggles.IndexOf(toggle.toggle);
                    _snap.GoToScreen(id);
                });
            }

            id1++;
        }

        if (!onClick && showToggles)
        {
            toggleCount = Sprites.Count > 3 ? 3 : Sprites.Count;
            for (int i = 0; i < toggleCount; i++)
            {
                var toggle = Instantiate(togglePrefab, pagination.transform).GetComponent<SliderToggle>();
                toggle.toggle.interactable = false;
                toggle.toggle.group = pagination;
                _toggles.Add(toggle.toggle);
            }
            _snap.OnSelectionPageChangedEvent.AddListener((value) =>
            {
                var rId = _pages.Find(t => t.initId == value).initId;
                int p = Sprites.Count <= 3 ? rId : (int)(3 * ((float)rId / Sprites.Count));
                if (p < toggleCount) 
                    _toggles[p].isOn = true;
            });
        }
        _snap.enabled = true;

            if (!isInfiniteInit)
                _infiniteScroll.Init();
            _infiniteScroll.ResetItems();

        if (titleText && _curId < titles.Count)
        {
            SetTitle(titles[_curId]);
        }
        _snap.GoToScreen(pageOnStart);
        //_toggles[pageOnStart].isOn = true;
    }

    private TweenerCore<Color, Color, ColorOptions> _fadeTweener;
    private async void SetTitle(string title)
    {
        if(titles.Count == 0) return;
        
        _fadeTweener?.Kill();
        
        _fadeTweener = titleText.DOFade(0, 0.25f);
        await _fadeTweener.AsyncWaitForCompletion();
        
        titleText.text = title;

        _fadeTweener = titleText.DOFade(1, 0.25f);
        await _fadeTweener.AsyncWaitForCompletion();
    }

    public void Clear()
    {
        _snap._isInfinite = false;
        Sprites.Clear();
        _pages.Clear();
        _infiniteScroll?.Clear();
        
        foreach (var t in _toggles)
        {
            Destroy(t.gameObject);
        }
        _toggles.Clear();
        toggleCount = 0;
        
        GameObject[] objects;
        _snap.RemoveAllChildren(out objects);
        foreach (var o in objects)
        {
            Destroy(o);
        }
       
        
        if (Sprites.Count == 0 &&
            _pages.Count == 0 &&
            _toggles.Count == 0 &&
            _snap.ChildObjects.Length == 0)
        {
            Debug.Log($"Слайдер успешно очищен!");
        }
        else
        {
            Debug.Log("Очистка слайдера не удалась!");
        }
        _snap._isInfinite = true;
    }

    public void SetSprites(Sprite[] sprites)
    {
        Clear();
        
        Sprites.AddRange(sprites);
        Initialize();
        SelectScreen(pageOnStart);
        //Debug.Log($"Загружено спрайтов: {Sprites.Count}");
    }
    
    
    public void NextPage()
    {
        _snap.NextScreen();
    }
    public void PrevPage()
    {
        _snap.PreviousScreen();
    }

    public void SelectToggle(int id)
    {
        _toggles[id].isOn = true;
    }
    public void SelectScreen(int id)
    {
        if(_pages.Count == 0) return;

        if(_pages.Count <= 2)
            _curId = 0;
        else
        {
            _curId = id;
        }
        
        _snap.GoToScreen(_curId);
        //SetTitle(titles[_curId]);
    }

    
    public void RecalculatePositions()
    {
        _infiniteScroll.ResetItems();
    }
}
