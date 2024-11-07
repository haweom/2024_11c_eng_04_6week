using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform bulletSpawn;
     
    private GameObject _bullet;
    private Transform _shootPosition;
    private Transform _playerTransform;
    private BoxCollider2D _boxCollider2D;
    private bool _isPlayerNear;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _isPlayerNear = false;
    }
    
    private void Update()
    {
        if (_isPlayerNear)
        {
            _animator.SetBool("Shoot", true);
        }
        else
        {
            _animator.SetBool("Shoot", false);
        }
    }

    public void Shoot()
    {
        _bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Vector2 shotDirection = -bulletSpawn.right;
        shotDirection.Normalize();
        
        Rigidbody2D rb = _bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shotDirection * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerNear = false;
        }
    }
}
