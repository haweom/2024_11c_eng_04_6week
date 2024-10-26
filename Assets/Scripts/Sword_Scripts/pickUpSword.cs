using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pickUpSword : MonoBehaviour
{
    private PlayerAttack _playerAttack;
    private Rigidbody2D _rb2d;
    private SwordThrow _swordThrow;

    private void Start()
    {
        _playerAttack = FindObjectOfType<PlayerAttack>();
        _rb2d = GetComponentInParent<Rigidbody2D>();
        _swordThrow = FindObjectOfType<SwordThrow>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_swordThrow.IsReturning() || _rb2d.velocity == Vector2.zero)
            {
                _playerAttack.SetHasSword(true);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
