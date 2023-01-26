using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HallManager : MonoBehaviour
{
    public List<HallElement> hallElements;

    public Scrollbar scrollbar;

    public float durationScrollMove = 1f;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var hallElement in hallElements)
        {
            hallElement.HallManager = this;
        }
    }

    public void ChangeAll(bool value)
    {
        foreach (var hallElement in hallElements)
        {
            hallElement.ChangeState(false);
        }
    }

    public void ScrollMove(float value)
    {
        DOTween.To(() => scrollbar.value, x => scrollbar.value = x, value, durationScrollMove);
    }
}
