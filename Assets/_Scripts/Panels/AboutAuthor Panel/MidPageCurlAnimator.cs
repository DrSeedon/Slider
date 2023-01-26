using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MidPageCurlAnimator : MonoBehaviour
{
    public RectTransform mainMask;
    public Vector2 mainStartPosition;
    public Vector3 mainStartRotation;  
    public Vector2 mainEndPosition;
    public Vector3 mainEndRotation;
    [Space]
    public RectTransform page1Mask;
    public Vector2 page1StartPosition;
    public Vector3 page1StartRotation;
    public Vector2 page1EndPosition;
    public Vector3 page1EndRotation;
    [Space]
    public RectTransform page2Mask;
    public Vector2 page2StartPosition;
    public Vector3 page2StartRotation;
    public Vector2 page2EndPosition;
    public Vector3 page2EndRotation;
    [Space] 
    public float animDuration = 1;

    public void Hide()
    {
        StartCoroutine(CurlFront());
    }
    public void Show()
    {
        StartCoroutine(CurlBack());
    }
    public IEnumerator CurlFront()
    {
        var page1GlobalPos = page1Mask.transform.position;
        var page1GlobalRot = page1Mask.transform.rotation;
        mainMask.DOAnchorPos(mainEndPosition, animDuration);
        mainMask.DORotate(mainEndRotation, animDuration)
            .OnUpdate(()=>
            {
                page1Mask.transform.position = page1GlobalPos;
                page1Mask.transform.rotation = page1GlobalRot;
            });
        
        yield return new WaitForSeconds(animDuration);
    }
    public IEnumerator CurlBack()
    {
        yield return new WaitForSeconds(animDuration);
    }
}
