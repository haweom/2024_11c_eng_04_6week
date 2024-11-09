using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool IsPaused;
    public GameObject pausePanel;
    public GameObject optionsMenu;
    private OptionsMenu _optionsMenu;

    private void Awake()
    {
        _optionsMenu = optionsMenu.GetComponent<OptionsMenu>();
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        IsPaused = false;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        IsPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        IsPaused = false;
        pausePanel.SetActive(false);
        _optionsMenu.Hide();
        Time.timeScale = 1f;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void Options()
    {
        _optionsMenu.Show();
    }
}
