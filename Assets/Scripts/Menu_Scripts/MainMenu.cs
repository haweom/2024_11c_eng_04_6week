using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    private OptionsMenu _optionsMenu;
    public GameObject creditsMenu;
    private Credits _credits;
    private AudioManagerScript _ams;
    
    private void Awake()
    {
        _optionsMenu = optionsMenu.GetComponent<OptionsMenu>();
        _credits = creditsMenu.GetComponent<Credits>();
        _ams = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
        _ams.srcMusic.loop = true;
    }

    private void Start()
    {
        _optionsMenu.GetRunSrc().Stop();
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

    public void Credits()
    {
        _credits.Show();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(2);
            _ams.srcMusic.Pause();
        }
    }
}