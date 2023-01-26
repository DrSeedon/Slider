using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExitHandler : MonoBehaviour
{
    public int leftClickCount = 5;
    public int rightClickCount = 5;

    public int curLeftClickCount = 0;
    public int curRightClickCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Counter());
    }

    
    float time = 0;

    public void LeftCount()
    {
        curLeftClickCount++;
        time = 0;
    }
    public void RightCount()
    {
        curRightClickCount++;
        time = 0;
    }

    public IEnumerator Counter()
    {
        while (true)
        {
            time += Time.deltaTime;
            if (time >= 2f)
            {
                curLeftClickCount = 0;
                curRightClickCount = 0;
            }

            if (curLeftClickCount >= leftClickCount && curRightClickCount >= rightClickCount)
            {
                Application.Quit();
            }
            yield return null;
        }
    }
}
