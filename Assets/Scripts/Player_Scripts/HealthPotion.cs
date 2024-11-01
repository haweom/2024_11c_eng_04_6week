using System;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{

    [SerializeField] private float regenValue = 20f;
    
    [SerializeField] private float cooldown = 5f;
    private float _cooldownTimer = 0f;
    private PlayerHealth _playerHealth;
    
    void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_cooldownTimer <= 0)
            {
                _cooldownTimer = cooldown;
                Regenerate();
                
            }
        }else if (_cooldownTimer >= 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        
    }

    void Regenerate()
    {
        _playerHealth.Regen(regenValue);
    }
}
