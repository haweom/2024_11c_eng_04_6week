using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicTransition : MonoBehaviour
{
    [SerializeField] private float sceneTime;
    private float _currentTime;
    void Start()
    {
        _currentTime = sceneTime;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
