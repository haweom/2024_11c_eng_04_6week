using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] public CoinConfig coinData;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Collected", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetBool("Collected", true);
            PlayerScore playerScore = other.GetComponent<PlayerScore>();
            playerScore.AddScore(coinData.value);
        }
    }

    public void DestroyCoin()
    {
        Destroy(gameObject);
    }
}
