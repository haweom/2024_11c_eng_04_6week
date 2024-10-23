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

    private void Start()
    {
        _currentHealth = maxHealth;
        healthBar.SetHealth(_currentHealth);
        _playerRespawn = FindObjectOfType<PlayerRespawn>();
    }

    public void Die()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InstaDeath"))
        {
            Die();
        }
    }
}
