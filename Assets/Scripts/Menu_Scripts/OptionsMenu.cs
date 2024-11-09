using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject sfxManager;
    public GameObject runManager;
    public GameObject musicManager;
    private AudioSource _musicSrc;
    private AudioSource _sfxSrc;
    private AudioSource _runSrc;

    private void Awake()
    {
        _musicSrc = musicManager.GetComponent<AudioSource>();
        _sfxSrc = sfxManager.GetComponent<AudioSource>();
        _runSrc = runManager.GetComponent<AudioSource>();
    }

    private void Start()
    {
        optionsPanel.SetActive(false);
    }
    
    private void Update()
    {
        
    }

    public void Show()
    {
        optionsPanel.SetActive(true);
    }
    
    public void Hide()
    {
        optionsPanel.SetActive(false);
    }
    
    public void ChangeSfxVolume(float value)
    {
        _sfxSrc.volume = value;
        _runSrc.volume = value;
    }

    public void ChangeMusicVolume(float value)
    {
        _musicSrc.volume = value;
    }
}
