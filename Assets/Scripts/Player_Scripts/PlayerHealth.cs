using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;

    public HealthBarScript healthBar;
    private PlayerRespawn _playerRespawn;
    private GameObject _player;

    private void Start()
    {
        _currentHealth = maxHealth;
        healthBar.SetHealth(_currentHealth);
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRespawn = FindObjectOfType<PlayerRespawn>();
    }

    private void Update()
    {
        if (_player.transform.position.y < -50f)
        {
            _playerRespawn.Respawn();
        }
    }

    private void Die()
    {
        _playerRespawn.Respawn();
    }

    public void Damage(float damage)
    {
        _currentHealth -= damage;
        healthBar.SetHealth(_currentHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void setHealth(float health)
    {
        _currentHealth = health;
        healthBar.SetHealth(_currentHealth);
    }
}
