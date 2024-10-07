using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingHook : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float _maxDistance = 30f;
    private Vector2 _startingPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _startingPosition = transform.position;
    }

    private void Update()
    {
        float distanceTraveled = Vector2.Distance(_startingPosition, transform.position);

        if (distanceTraveled >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _rb.velocity = Vector2.zero;
        }
    }
}