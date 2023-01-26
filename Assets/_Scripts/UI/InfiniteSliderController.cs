using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class InfiniteSliderController : MonoBehaviour
{
    [SerializeField] public HorizontalScrollSnap _snap;
    
    [Header("Sprites")]
    public List<Sprite> Sprites;
    
    [Header("Other")]
    public Transform content;
    public ToggleGroup pagination;
    public GameObject pagePrefab;
    public GameObject togglePrefab;
    [Header("OnStart")] 
    public int pageOnStart = 0;
    private List<Toggle> _toggles = new List<Toggle>();
    private List<GameObject> _pages = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Clear()
    {
        Sprites.Clear();
        _pages.Clear();

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
    }
    
    public void Initialize()
    {
        _snap.OnSelectionPageChangedEvent.RemoveAllListeners();
        _snap.enabled = false;

        _snap.OnSelectionChangeStartEvent.AddListener(() =>
        {
//            _nextButton.interactable = false;
//            _prevButton.interactable = false;
        });
        _snap.OnSelectionChangeEndEvent.AddListener((value) =>
        {
//            _nextButton.interactable = true;
//            _prevButton.interactable = true;
        });

        foreach (var sprite in Sprites)
        {
            var page = Instantiate(pagePrefab, content).GetComponent<Image>();
            page.sprite = sprite;
            _pages.Add(page.gameObject);
            
            _snap.AddChild(page.gameObject);

            /*if (onClick && showToggles)
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

            id++;*/
        }
        _snap.GoToScreen(pageOnStart);
        //_toggles[pageOnStart].isOn = true;
    }
    
    public void SelectScreen(int id)
    {
        if(_pages.Count == 0) return;
        _snap.GoToScreen(id);
    }
    public void ResetSprites(Sprite[] sprites)
    {
        Clear();
        Sprites.AddRange(sprites);
        Initialize();
        SelectScreen(pageOnStart);
        Debug.Log($"Загружено спрайтов: {Sprites.Count}");
    }
}
