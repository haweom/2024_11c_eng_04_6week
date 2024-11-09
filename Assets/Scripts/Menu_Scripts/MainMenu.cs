using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public GameObject optionsMenu;
    private OptionsMenu _optionsMenu;
    
    private void Awake()
    {
        _optionsMenu = optionsMenu.GetComponent<OptionsMenu>();
    }

    private void Start()
    {
        _optionsMenu.runManager.GetComponent<AudioSource>().Stop();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
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
