using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pickUpSword : MonoBehaviour
{
    private PlayerAttack _playerAttack;

    private void Start()
    {
        _playerAttack = FindObjectOfType<PlayerAttack>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerAttack.SetHasSword(true);
            Destroy(this.gameObject);
        }
    }
}
