using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TotemBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float maxRange;
    
    private Rigidbody2D _rb;
    private Vector2 _startPoint;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPoint = transform.position;
    }
    
    private void Update()
    {
        float distance = Vector2.Distance(_startPoint, transform.position);

        if (distance > maxRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null && other.CompareTag("Player"))
        {
            damageable.Damage(damage);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
