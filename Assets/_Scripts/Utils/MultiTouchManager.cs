using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTouchManager : MonoBehaviour
{
    public bool disableMultiTouch = true;
    // Start is called before the first frame update
    void Awake()
    {
        Input.multiTouchEnabled = !disableMultiTouch;
    }

}
