using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private List<Coin> _allCoins = new List<Coin>();
    private int _maxScore;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        InitializeCoins();
        UpdateScore(0);
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

    public void UpdateScore(int score)
    {
        if (score > (_maxScore * 0.8))
        {
            scoreText.color = Color.green;
        }
        scoreText.text = score.ToString() + " / " + _maxScore.ToString();
    }
}
