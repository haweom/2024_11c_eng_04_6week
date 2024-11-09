using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGate : MonoBehaviour
{
    private Animator _animator;
    private PlayerScore _playerScore;

    private bool _isOpen = false;
    private List<Coin> _allCoins = new List<Coin>();
    private int _maxScore;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>();
    }

    private void Start()
    {
        InitializeCoins();
    }

    private void Update()
    {
        if (!_isOpen)
        {
            _isOpen = CheckPoints();
            if (_isOpen)
            {
                _animator.SetBool("IsOpen", true);
            }
            else
            {
                _animator.SetBool("IsOpen", false);

            }
        }
    }

    private bool CheckPoints()
    {
        if (_playerScore.GetScore() > (_maxScore * 0.8f))
        {
            return true;
        }

        return false;
    }
    
    private void InitializeCoins()
    {
        _allCoins.AddRange(FindObjectsOfType<Coin>());
        _maxScore = 0;
        
        foreach (Coin coin in _allCoins)
        {
            _maxScore += coin.GetScore();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isOpen)
        {
            Invoke(nameof(NextLevel), 0.5f);
        }
    }

    private void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        
    }
}
