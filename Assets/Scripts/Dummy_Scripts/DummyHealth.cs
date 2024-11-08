using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Damage(float damage, Vector2 knockback)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
}