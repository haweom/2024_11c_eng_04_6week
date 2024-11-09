using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    private OptionsMenu _optionsMenu;
    private AudioManagerScript _ams;
    
    private void Awake()
    {
        _optionsMenu = optionsMenu.GetComponent<OptionsMenu>();
        _ams = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
        _ams.srcMusic.loop = true;
    }

    private void Start()
    {
        _optionsMenu.runManager.GetComponent<AudioSource>().Stop();
        _ams.srcMusic.clip = _ams.mainMenuTheme;
        _ams.srcMusic.Play();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        _ams.srcMusic.Pause();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void Options()
    {
        _optionsMenu.Show();
    }
}