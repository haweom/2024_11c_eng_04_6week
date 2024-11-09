using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICoin
{
    [SerializeField] public CoinConfig coinData;
    private Animator _animator;
    private AudioManagerScript _ams;
    private bool _isCollected;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Collected", false);
        _ams = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
        _isCollected = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isCollected)
        {
            _ams.PlaySfx(_ams.coinPickUp);
            _isCollected = true;
            _animator.SetBool("Collected", true);
            PlayerScore playerScore = other.GetComponent<PlayerScore>();
            playerScore.AddScore(coinData.value);
        }
    }

    public void DestroyCoin()
    {
        Destroy(gameObject);
    }

    public int GetScore()
    {
        return coinData.value;
    }
}
