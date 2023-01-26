using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollControllerV2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 newPose;
    public bool SelectMove;
    public bool isTouch = false;
    public Transform scrollContainer;
    public float lerpTime = 5f;
    public int index = 0;
    public List<GameObject> slides = new List<GameObject>();

    public Transform centerScroll;

    public float speed = 0;

    private void Start()
    {
        SwipeDetector.OnSwipe += Handle_OnSwipe;
        newPose = scrollContainer.position;

        //scrollRect.normalizedPosition = new Vector2(1, 0);

        /*
        int position = slidesText[index].IndexOf("_");
        imageText.text = slidesText[index].Remove(0, position + 1);*/
    }

    public void FindAllSlides()
    {
        int i = 0;
        foreach (var slide in slides)
        {
            slide.GetComponent<ScrollElement>().id = i++;
        }
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
        isMoving = true;
        if (isImmediately)
        {
            scrollContainer.position = newPose;
        }
        /*
        int position = slidesText[index].IndexOf("_");
        imageText.text = slidesText[index].Remove(0, position + 1);*/
    }
    
    

    private GameObject FindClosestSlide()
    {
        float min = Vector3.Distance(slides[0].transform.position, centerScroll.position);
        Transform closestSlide = slides[0].transform;
        foreach (var slide in slides)
        {
            var t = Vector3.Distance(slide.transform.position, centerScroll.position);
            if (t < min)
            {
                closestSlide = slide.transform;
                min = t;
            }
        }

        return closestSlide.gameObject;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.47f, 0.13f);
        Gizmos.DrawSphere(newPose, 100f);
    }

    private void Update()
    {
        //движемся к новой позиции

        if (!isMoving)
        {
            var se = FindClosestSlide().GetComponent<ScrollElement>();
            index = se.id;
        }
        if (isAuto)
        {
            //newPose = new Vector3(-FindClosestSlide().x, scrollContainer.position.y, scrollContainer.position.z);
            scrollContainer.position = Vector3.Lerp(scrollContainer.position, newPose, lerpTime * Time.deltaTime);
        }

        //стопоримся если прибыли на место
        if (Vector3.Distance(scrollContainer.position, newPose) < 0.01f)
        {
            scrollContainer.position = newPose;
            isMoving = false;
        } /*
        if (Input.GetMouseButtonDown(0))
        {
            SelectMove = false;
        }*/
    }

    private float oldPos = 0;
    public float range = 500f;
    public bool isAuto = false;
    public bool isMoving = false;

    private void FixedUpdate()
    {
        var position = scrollContainer.position;
        speed = (position.magnitude - oldPos) / Time.fixedDeltaTime;
        oldPos = position.magnitude;

        /*
        if (Mathf.Abs(speed) < range)
        {
            isAuto = true;
            Debug.Log("true");
        }
        else
        {
            isAuto = false;
        }
        */
    }


    public void OnPointerDown(PointerEventData pointerEventData)
    {
        isTouch = true;
        isAuto = false;
        isMoving = false;
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isTouch = false;
        isAuto = true;
    }
}