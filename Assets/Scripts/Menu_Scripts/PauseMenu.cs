using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    private bool _isPaused;
    public GameObject pausePanel;
    void Start()
    {
        pausePanel.SetActive(false);
        _isPaused = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("aaa");
            if (_isPaused)
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
        _isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        _isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
