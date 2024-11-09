using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private bool isTurningRight;
    [SerializeField] private float maxDistance = 20f;

    private GameObject _bullet;
    private Transform _shootPosition;
    private Transform _playerTransform;
    private BoxCollider2D _boxCollider2D;
    private bool _isPlayerNear;
    
    private Animator _animator;
    private AudioManagerScript _ams;

    private void Awake()
    {
        _ams = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
    }

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

        if (this.CompareTag("TotemHead"))
        {
            float distance = Vector2.Distance(_playerTransform.position, transform.position);
            float volume = Mathf.Clamp01(1 - (distance / maxDistance));
            _ams.PlayDynamicSfx(_ams.totem1Shoot, volume);
        }
        
        Vector2 shotDirection = Vector2.zero;
        
        if (!isTurningRight)
        {
            shotDirection = -bulletSpawn.right;
        }
        else
        {
            shotDirection = bulletSpawn.right;
            Vector3 bulletScale = _bullet.transform.localScale;
            bulletScale.x *= -1;
            _bullet.transform.localScale = bulletScale;
        }
        
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
