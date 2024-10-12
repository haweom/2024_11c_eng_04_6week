using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void Damage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
}
