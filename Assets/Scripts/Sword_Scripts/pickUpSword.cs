using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pickUpSword : MonoBehaviour
{
    private PlayerAttack _playerAttack;
    private Rigidbody2D _rb2d;

    private void Start()
    {
        _playerAttack = FindObjectOfType<PlayerAttack>();
        _rb2d = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _rb2d.velocity is { x: 0, y: 0 })
        {
            _playerAttack.SetHasSword(true);
            Destroy(transform.parent.gameObject);
        }
    }
}
