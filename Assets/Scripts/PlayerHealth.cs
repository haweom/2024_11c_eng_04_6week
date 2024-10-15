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

    private void Start()
    {
        _currentHealth = maxHealth;
        healthBar.SetHealth(_currentHealth);
    }

    private void Die()
    {
        Destroy(gameObject);
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
}
