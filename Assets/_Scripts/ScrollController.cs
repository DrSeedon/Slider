using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 newPose;
    public bool SelectMove;
    public bool isTouch = false;
    public Transform scrollContainer;
    public float lerpTime;
    public int index = 0;
    public List<GameObject> slides = new List<GameObject>();
    //public List<string> slidesText = new List<string>();
    public List<Image> dotImages = new List<Image>();

    public ScrollRect scrollRect;

    public Sprite DotOn;
    public Sprite DotOff;

    public Transform centerScroll;


    public GameObject rigthArrow;
    public GameObject leftArrow;

    




    private void Start()
    {
        SwipeDetector.OnSwipe += Handle_OnSwipe;
        newPose = scrollContainer.position;


        dotImages[0].sprite = DotOn;

        //scrollRect.normalizedPosition = new Vector2(1, 0);

        /*
        int position = slidesText[index].IndexOf("_");
        imageText.text = slidesText[index].Remove(0, position + 1);*/
    }

    private void OnDestroy()
    {
        SwipeDetector.OnSwipe -= Handle_OnSwipe;

    }

    public void MoveSlideId(int id)
    {
        index = id;
        MoveSlide(false);
    }
    public void MoveSlideIdImmediately(int id)
    {
        index = id;
        MoveSlide(true);
    }
    public void MoveSlideImmediatelyCenter()
    {
        index = slides.Count / 2;
        MoveSlide(true);
        ChangeDotAll(false);
        ChangeDot(1, true);
        leftArrow.SetActive(true);
        rigthArrow.SetActive(true);
    }

    public void MoveSlideSide(bool isLeft)
    {
        if (isLeft)
        {
            if (index < slides.Count - 1)
            {
                index++;
            }
            else
            {
                return;
            }
        }
        else
        {
            if (index > 0)
            {
                index--;
            }
            else
            {
                return;
            }

        }
        MoveSlide(false);
    }

    //переместится на след слайд (параметр в лево или в право)
    public void MoveSlide(bool isImmediately)
    {
        if (SelectMove)
            return;

        //поиск новой позиции для перемещения
        float dis = centerScroll.position.x - slides[index].transform.position.x;
        newPose = new Vector3(scrollContainer.position.x + dis, scrollContainer.position.y, scrollContainer.position.z);
        if (isImmediately)
        {
            scrollContainer.position = newPose;
        }

        var count = slides.Count - 1;

        //логика исчезновения стрелок (баг если слайд один кнопка справа все равно есть)
        if (index == 0)        
            leftArrow.SetActive(false);

        if (index == 1)
            leftArrow.SetActive(true);

        if (index == count)
            rigthArrow.SetActive(false);
        
        if (index == count - 1)
            rigthArrow.SetActive(true);

        
        //заполнение точек как процентный бар
        if (index == 0 || index == count / 2 - 1)
        {
            ChangeDotAll(false);
            ChangeDot(0, true);
            return;
        }

        if (index == count / 2 || index == count - 2)
        {
            ChangeDotAll(false);
            ChangeDot(1, true);
            return;
        }

        if (index == count)
        {
            ChangeDotAll(false);
            ChangeDot(2, true);
            return;
        }

        
        /*
        int position = slidesText[index].IndexOf("_");
        imageText.text = slidesText[index].Remove(0, position + 1);*/
    }

    private void ChangeDotAll(bool value)
    {
        for (int i = 0; i < dotImages.Count; i++)
        {
            ChangeDot(i, value);
        }
    }

    private void ChangeDot(int id, bool value)
    {
        dotImages[id].sprite = value ? DotOn : DotOff;
        dotImages[id].rectTransform.sizeDelta = value ? new Vector2 (53, 53) : new Vector2 (33, 33);
    }

    private void Handle_OnSwipe(SwipeData swipeData)
    {
        if (!gameObject.activeSelf)
            return;

        switch (swipeData.Direction)
        {
            case SwipeDirection.Left:
                MoveSlideSide(true);
                break;
            case SwipeDirection.Right:
                MoveSlideSide(false);
                break;
            default:
                print("Неправильный жест " + swipeData.Direction);
                break;
        }
    }



    private void Update()
    {
        //движемся к новой позиции
        if (scrollContainer.position != newPose && !isTouch)
        {
            scrollContainer.position = Vector3.Lerp(scrollContainer.position, newPose, lerpTime * Time.deltaTime);
        }
        //стопоримся если прибыли на место
        if (Vector3.Distance(scrollContainer.position, newPose) < 0.01f)
        {
            scrollContainer.position = newPose;
            SelectMove = false;
        }/*
        if (Input.GetMouseButtonDown(0))
        {
            SelectMove = false;
        }*/
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        isTouch = true;
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isTouch = false;
    }
}
