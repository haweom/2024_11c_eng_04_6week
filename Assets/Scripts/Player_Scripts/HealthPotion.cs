using System;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private AudioManagerScript _ams;
    
    [SerializeField] private float regenValue = 20f;
    
    [SerializeField] private float cooldown = 5f;
    private float _cooldownTimer = 0f;
    private PlayerHealth _playerHealth;
    
    void Awake()
    {
        _ams = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
        _playerHealth = GetComponent<PlayerHealth>();
    }
    
    public float Cooldown
    {
        get { return cooldown; }
    }
    
    public float CooldownTimer
    {
        get { return _cooldownTimer; }
    }

    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_cooldownTimer <= 0)
                {
                    _cooldownTimer = cooldown;
                    _ams.PlaySfx(_ams.potion);
                    Regenerate();

                }
            }
            else if (_cooldownTimer >= 0)
            {
                _cooldownTimer -= Time.deltaTime;
            }
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
