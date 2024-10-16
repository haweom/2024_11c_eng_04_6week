using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int _currentScore = 0;

    private void Start()
    {
        _currentScore = 0;
    }

    public void AddScore(int score)
    {
        _currentScore += score;
    }
}
