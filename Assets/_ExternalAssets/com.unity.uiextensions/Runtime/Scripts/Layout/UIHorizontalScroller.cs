/// Credit Mrs. YakaYocha 
/// Sourced from - https://www.youtube.com/channel/UCHp8LZ_0-iCvl-5pjHATsgw
/// Please donate: https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=RJ8D9FRFQF9VS

using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
    [RequireComponent(typeof(ScrollRect))]
    [AddComponentMenu("Layout/Extensions/Horizontal Scroller")]
    public class UIHorizontalScroller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Tooltip("desired ScrollRect")]
        public ScrollRect scrollRect;
        [Tooltip("Center display area (position of zoomed content)")]
        public RectTransform center;
        [Tooltip("Size / spacing of elements")]
        public RectTransform elementSize;
        [Tooltip("Scale = 1/ (1+distance from center * shrinkage)")]
        public Vector2 elementShrinkage = new Vector2(1f / 200, 1f / 200);
        [Tooltip("Minimum element scale (furthest from center)")]
        public Vector2 minScale = new Vector2(0.7f, 0.7f);
        [Tooltip("Select the item to be in center on start.")]
        public int startingIndex = -1;
        [Tooltip("Stop scrolling past last element from inertia.")]
        public bool stopMomentumOnEnd = true;
        [Tooltip("Set Items out of center to not interactible.")]
        public bool disableUnfocused = true;
        [Tooltip("Button to go to the next page. (optional)")]
        public GameObject scrollUpButton;
        [Tooltip("Button to go to the previous page. (optional)")]
        public GameObject scrollDownButton;
        [Tooltip("Event fired when a specific item is clicked, exposes index number of item. (optional)")]
        public IntEvent OnButtonClicked;
        [Tooltip("Event fired when the focused item is Changed. (optional)")]
        public IntEvent OnFocusChanged;
        
        public List<GameObject> _arrayOfElements;

        public int focusedElementIndex { get; private set; }

        public string result { get; private set; }

        private float[] distReposition;
        private float[] distance;
        //private int elementsDistance;


        //Scrollable area (content of desired ScrollRect)
        [HideInInspector]
        public RectTransform scrollingPanel{ get { return scrollRect.content; } }

        public HorizontalLayoutGroup horizontalLayoutGroup;

        /// <summary>
        /// Constructor when not used as component but called from other script, don't forget to set the non-optional properties.
        /// </summary>
        public UIHorizontalScroller()
        {
        }

        /// <summary>
        /// Constructor when not used as component but called from other script
        /// </summary>
        public UIHorizontalScroller(RectTransform center, RectTransform elementSize, ScrollRect scrollRect, List<GameObject> arrayOfElements)
        {
            this.center = center;
            this.elementSize = elementSize;
            this.scrollRect = scrollRect;
            _arrayOfElements = arrayOfElements;
        }

        /// <summary>
        /// Awake this instance.
        /// </summary>
        public void Awake()
        {
            if (!scrollRect)
            {
                scrollRect = GetComponent<ScrollRect>();
            }
            if (!center)
            {
                Debug.LogError("Please define the RectTransform for the Center viewport of the scrollable area");
            }
            if (!elementSize)
            {
                elementSize = center;
            }
            if (_arrayOfElements == null || _arrayOfElements.Count == 0)
            {
                _arrayOfElements = new List<GameObject>();
                for (int i = 0; i < scrollingPanel.childCount; i++)
                {
                    if (scrollingPanel.GetChild(i).gameObject.activeSelf)
                        _arrayOfElements.Add(scrollingPanel.GetChild(i).gameObject);
                }     
            }
        }

        /// <summary>
        /// Recognises and resizes the children.
        /// </summary>
        /// <param name="startingIndex">Starting index.</param>
        /// <param name="arrayOfElements">Array of elements.</param>
        public void updateChildren(int startingIndex = -1, List<GameObject> arrayOfElements = null)
        {
            // Set _arrayOfElements to arrayOfElements if given, otherwise to child objects of the scrolling panel.
            if (arrayOfElements != null)
            {
                _arrayOfElements = arrayOfElements;
            }
            else
            {
                _arrayOfElements = new List<GameObject>();
                for (int i = 0; i < scrollingPanel.childCount; i++)
                {
                    if(scrollingPanel.GetChild(i).gameObject.activeSelf)
                        _arrayOfElements.Add(scrollingPanel.GetChild(i).gameObject);
                }
            }

            // resize the elements to match elementSize rect
            for (var i = 0; i < _arrayOfElements.Count; i++)
            {
                int j = i;
                _arrayOfElements[i].GetComponent<Button>().onClick.RemoveAllListeners();
                _arrayOfElements[i].GetComponent<ScrollElement>().id = i;
                if (OnButtonClicked != null)
                {
                    _arrayOfElements[i].GetComponent<Button>().onClick.AddListener(() => OnButtonClicked.Invoke(j));
                }
                RectTransform r = _arrayOfElements[i].GetComponent<RectTransform>();
                r.anchorMax = r.anchorMin = r.pivot = new Vector2(0.5f, 0.5f);
                //r.localPosition = new Vector2(0, i * elementSize.rect.size.y);
                r.localPosition = new Vector2(i * elementSize.rect.size.x, 0);
                r.sizeDelta = elementSize.rect.size;
            }

            // prepare for scrolling
            distance = new float[_arrayOfElements.Count];
            distReposition = new float[_arrayOfElements.Count];
            focusedElementIndex = 0;

            //scrollRect.scrollSensitivity = elementSize.rect.height / 5;

            // if starting index is given, snap to respective element
            if (startingIndex > -1)
            {
                startingIndex = startingIndex > _arrayOfElements.Count ? _arrayOfElements.Count - 1 : startingIndex;
                SnapToElement(startingIndex);
            }
        }

        public void Start()
        {

            if (scrollUpButton)
                scrollUpButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        ScrollLeft();
                    });

            if (scrollDownButton)
                scrollDownButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        ScrollRight();
                    });
            updateChildren(startingIndex, _arrayOfElements);
        }

        public void UpdateChildren()
        {
            updateChildren(startingIndex);
        }

        public bool isAuto = false;
        public void Update()
        {
            if (UIExtensionsInputManager.GetMouseButton(0))
            {
            }
            if (_arrayOfElements.Count < 1)
            {
                return;
            }

            for (var i = 0; i < _arrayOfElements.Count; i++)
            {
                distReposition[i] = center.GetComponent<RectTransform>().position.x - _arrayOfElements[i].GetComponent<RectTransform>().position.x;
                distance[i] = Mathf.Abs(distReposition[i]);

                //Magnifying effect
                Vector2 scale = Vector2.Max(minScale, new Vector2(1 / (1 + distance[i] * elementShrinkage.x), (1 / (1 + distance[i] * elementShrinkage.y))));
                _arrayOfElements[i].GetComponent<RectTransform>().transform.localScale = new Vector3(scale.x, scale.y, 1f);
                _arrayOfElements[i].GetComponent<BookElement>().ChangeOpaсityFrame(scale.x);
            }

            if (!isAuto)
            {
                // detect focused element
                float minDistance = Mathf.Min(distance);
                int oldFocusedElement = focusedElementIndex;
                for (var i = 0; i < _arrayOfElements.Count; i++)
                {
                    _arrayOfElements[i].GetComponent<CanvasGroup>().interactable =
                        !disableUnfocused || minDistance == distance[i];
                    if (minDistance == distance[i])
                    {
                        focusedElementIndex = i;
                        //result = _arrayOfElements[i].GetComponentInChildren<Text>().text;
                    }
                }

                if (focusedElementIndex != oldFocusedElement && OnFocusChanged != null)
                {
                    OnFocusChanged.Invoke(focusedElementIndex);
                }
            }


            if (!UIExtensionsInputManager.GetMouseButton(0))
            {
                isHold = false;
            }
            else
            {
                isHold = true;
            }

            if (!isHold)
            {
                // scroll slowly to nearest element when not dragged
                ScrollingElements();
            }

            _arrayOfElements[focusedElementIndex].GetComponent<BookElement>().ButtonClicked(focusedElementIndex);
            
            // stop scrolling past last element from inertia
            if (stopMomentumOnEnd
                && (_arrayOfElements[0].GetComponent<RectTransform>().position.x > center.position.x
                || _arrayOfElements[_arrayOfElements.Count - 1].GetComponent<RectTransform>().position.x < center.position.x))
            {
                scrollRect.velocity = Vector2.zero;
            }


            Canvas.ForceUpdateCanvases();
            horizontalLayoutGroup.enabled = false;
            horizontalLayoutGroup.enabled = true;
        }
        
        public float LerpTime = 2f;
        private void ScrollingElements()
        {
            float newX = Mathf.Lerp(scrollingPanel.anchoredPosition.x, scrollingPanel.anchoredPosition.x + distReposition[focusedElementIndex], Time.deltaTime *
                LerpTime);
            Vector2 newPosition = new Vector2(newX, scrollingPanel.anchoredPosition.y);
            scrollingPanel.anchoredPosition = newPosition;
        }

        public void SnapToElement(int element)
        {
            float deltaElementPositionX = elementSize.rect.width * element;
            Vector2 newPosition = new Vector2(-deltaElementPositionX, scrollingPanel.anchoredPosition.y);
            scrollingPanel.anchoredPosition = newPosition;

        }

        public void ScrollLeft()
        {
            MoveSlideSide(true);
        }

        public void ScrollRight()
        {
            MoveSlideSide(false);
        }
        
        public void ScrollToID(int index)
        {
            focusedElementIndex = index;
            isAuto = true;
        }


        public void MoveSlideSide(bool isLeft)
        {
            if (isLeft)
            {
                if (focusedElementIndex < distReposition.Length - 1)
                {
                    focusedElementIndex++;
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (focusedElementIndex > 0)
                {
                    focusedElementIndex--;
                }
                else
                {
                    return;
                }
            }

            isAuto = true;
        }

        [System.Serializable]
        public class IntEvent:UnityEvent<int>
        {

        }

        public bool isHold = false;
        public void OnPointerDown(PointerEventData eventData)
        {
            isAuto = false;
            isHold = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isHold = false;
        }
    }
}