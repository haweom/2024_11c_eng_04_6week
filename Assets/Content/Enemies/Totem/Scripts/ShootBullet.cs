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
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void Update()
    {
        if (_playerTransform != null && Vector2.Distance(bulletSpawn.position, _playerTransform.position) <= 10)
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
        Vector2 shotDirection = bulletSpawn.right;
        shotDirection.Normalize();
        
        Rigidbody2D rb = _bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shotDirection * bulletSpeed;
    }
}
