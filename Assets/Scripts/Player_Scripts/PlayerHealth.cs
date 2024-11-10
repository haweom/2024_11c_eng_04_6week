using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private PlayerMovement _playerMovement;
    private float _currentHealth;

    public HealthBarScript healthBar;
    private PlayerRespawn _playerRespawn;
    private Animator _animator;
    
    private AudioManagerScript _ams;
    
    private void Awake()
    {
        _ams = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
        healthBar.SetHealth(_currentHealth);
        _playerRespawn = FindObjectOfType<PlayerRespawn>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void Die()
    {
        StartCoroutine(DieAndRespawnCoroutine());
    }

    public void Damage(float damage, Vector2 knockback)
    {
        _currentHealth -= damage;
        healthBar.SetHealth(_currentHealth);
        
        if (_currentHealth <= 0)
        {
            var randomSound = UnityEngine.Random.Range(0, 2) == 0 ? _ams.dmgSound1 : _ams.dmgSound2;
            _ams.PlaySfx(randomSound);
            Die();
        }
        else
        {
            var randomSound = UnityEngine.Random.Range(0, 2) == 0 ? _ams.dmgSound3 : _ams.dmgSound4;
            _ams.PlaySfx(randomSound);
        }
    }

    public void Regen(float regen)
    {
        if (_currentHealth + regen < maxHealth)
        {
            _currentHealth += regen;
        }
        else
        {
            _currentHealth = maxHealth;
        }
        healthBar.SetHealth(_currentHealth);
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
    
    private IEnumerator DieAndRespawnCoroutine()
    {
        _playerMovement.enabled = false;
        _animator.SetBool("Death", true);
        
        yield return new WaitForSeconds(0.25f);

        _playerRespawn.Respawn();

        _animator.SetBool("Death", false);
        _playerMovement.enabled = true;
    }
}
