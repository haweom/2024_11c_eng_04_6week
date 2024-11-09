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
    }

    private void Start()
    {
        _optionsMenu.runManager.GetComponent<AudioSource>().Stop();
        _ams.srcSfx.clip = _ams.mainMenuTheme;
        _ams.srcSfx.Play();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        _ams.srcSfx.Pause();
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
