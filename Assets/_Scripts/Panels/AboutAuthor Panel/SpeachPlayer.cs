using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeachPlayer : MonoBehaviour
{
    public ToggleBehaviourController speachManageToggle;

    [Header("Audio Settings")]
    public AudioListener audioListener;
    public AudioSource audioSource;
    public bool playOnAwake = false;
    public AudioClip audioClip;
    [Header("Text Settings")] 
    public ScrollRect _scrollRect;

    [Header("Buttons")] 
    public Toggle soundButton;
    public Toggle playPauseButton;

    private bool _isPaused = false;
    private bool _isStoped = true;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource.loop = false;
        audioSource.playOnAwake = playOnAwake;
        audioSource.clip = audioClip;
        
        //обработчики кнопок
        soundButton.onValueChanged.AddListener((value)=> audioSource.mute = !value);
        playPauseButton.onValueChanged.AddListener((value)=>
        {
            if(value)
                Play();
            else
                Pause();
        });
        speachManageToggle.OnSelect.AddListener(Play);
        speachManageToggle.OnDeselect.AddListener(Stop);
    }

    public void Play()
    {
        audioSource.Play();
        _isPaused = false;
        if (_isStoped)
        {
            playPauseButton.isOn = true;
            if (_scrollRoutine != null) StopCoroutine(_scrollRoutine);
            _scrollRoutine = StartCoroutine(ScrollContent());
        }
        _isStoped = false;
    }
    public void Pause()
    {
        audioSource.Pause();
        _isPaused = true;
    }
    
    public void Stop()
    {
        StartCoroutine(BlockPlayButton());
        _scrollRect.verticalNormalizedPosition = 0;
        audioSource.Stop();
        soundButton.isOn = true;
        
        _isStoped = true;
    }

    private float _curNormValue;
    private Coroutine _scrollRoutine;
    private IEnumerator ScrollContent()
    {
        while (true)
        {
            while (!_isPaused)
            {
                _curNormValue = 1 - audioSource.time / audioClip.length;
                _scrollRect.verticalNormalizedPosition = _curNormValue;

                if (_curNormValue == 0f)
                {
                    if (_waitToBackRoutine != null) StopCoroutine(_waitToBackRoutine);
                    _waitToBackRoutine = StartCoroutine(WaitToBack());
                }
                yield return null;
            } 
            yield return null;
        }
    }
    
    private Coroutine _waitToBackRoutine;

    private IEnumerator WaitToBack()
    {
        var waitTime = 0f;
        while (waitTime <= 3f)
        {
            if(audioSource.isPlaying)
                break;
            waitTime += Time.deltaTime;
            yield return null;
        }
        if(!audioSource.isPlaying)
            speachManageToggle.toggle.isOn = false;
    }

    private IEnumerator BlockPlayButton()
    {
        speachManageToggle.toggle.interactable = false;
        yield return new WaitForSeconds(2f);
        speachManageToggle.toggle.interactable = true;
    }
}
