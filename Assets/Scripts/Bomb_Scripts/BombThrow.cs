using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrow : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private Transform bombGun;
    [SerializeField] private float cooldownTime = 10f;
    
    private GameObject _thrown;
    private float _strength;
    private float _nextThrowTime;

    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (_thrown == null && Time.time >= _nextThrowTime)
                {
                    Throw();
                    _nextThrowTime = Time.time + cooldownTime;
                }
            }
        }
    }
    
    public float CooldownTime
    {
        get { return cooldownTime; }
    }
    
    public float NextThrowTime
    {
        get { return _nextThrowTime; }
    }

    private void Throw()
    {
        _thrown = Instantiate(bomb, bombGun.position, Quaternion.identity);

        bool throwDirection = GetThrowDirection();
        BombProjectile script = _thrown.GetComponent<BombProjectile>();
        if (throwDirection)
        {
            script.direction = 1;
        }
        else
        {
            script.direction = -1;
        }
        script.throwStrength = _strength;
    }

    private bool GetThrowDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _strength = Math.Abs(mousePosition.x - bombGun.position.x);
        _strength = _strength > 5 ? 1 : _strength / 5;

        return mousePosition.x - bombGun.position.x >= 0;
    }
}