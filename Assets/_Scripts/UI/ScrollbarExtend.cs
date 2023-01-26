using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollbarExtend : MonoBehaviour
{
    public Scrollbar scrollbarParent;
    public Scrollbar scrollbarChild;

    void Start()
    {
        scrollbarChild.size = scrollbarParent.size;
    }

    // Update is called once per frame
    void Update()
    {
        scrollbarChild.value = scrollbarParent.value;
    }

}
