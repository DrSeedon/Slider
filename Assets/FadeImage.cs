using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public float fadeDuration = 0.5f;
    public float durationBetweenFading = 0f;

    public float fadeMin = 0f;
    public float fadeMax = 1f;

    public List<Image> images;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var image in images)
        {
            LoopAnimation(image);
        }
    }

    private async void LoopAnimation(Image image)
    {
        while (true)
        {
            await image.DOFade(fadeMin, fadeDuration).AsyncWaitForCompletion();
            await Task.Delay((int) (durationBetweenFading * 1000));
            await image.DOFade(fadeMax, fadeDuration).AsyncWaitForCompletion();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
