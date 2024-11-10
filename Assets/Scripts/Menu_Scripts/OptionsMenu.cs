using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    private AudioSource _musicSrc;
    private AudioSource _sfxSrc;
    private AudioSource _runSrc;
    
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        if (AudioManagerScript.Instance != null)
        {
            _musicSrc = AudioManagerScript.Instance.srcMusic;
            _sfxSrc = AudioManagerScript.Instance.srcSfx;
            _runSrc = AudioManagerScript.Instance.srcRun;
            
            float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            float savedSfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
            
            musicSlider.value = savedMusicVolume;
            sfxSlider.value = savedSfxVolume;
        }
        else
        {
            Debug.LogError("AudioManagerScript instance not found.");
        }
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
        
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume(float value)
    {
        _musicSrc.volume = value;
        
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
        
    }

    public AudioSource GetRunSrc()
    {
        return _runSrc;
    }
}