using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMusicScript : MonoBehaviour
{
    private AudioSource _as;
    private AudioManagerScript _ams;

    private void Awake()
    {
        _as = this.GetComponent<AudioSource>();
        _ams = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        SetMusicForScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void SetMusicForScene(int sceneIndex)
    {
        if (sceneIndex == 0)
        {
            _as.clip = _ams.mainMenuTheme;
        }
        else if (sceneIndex == 1)
        {
            _as.clip = _ams.leve1Theme;
        }
        else if (sceneIndex == 2)
        {
            _as.clip = _ams.level2Theme;
        }
        _as.Play();
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetMusicForScene(scene.buildIndex);
    }
}
