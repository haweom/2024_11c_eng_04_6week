using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
     
    private GameObject _bullet;
    private Transform _shootPostion;
    private Transform _playerTransform;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void Update()
    {
        if (_playerTransform != null && Vector2.Distance(transform.position, _playerTransform.position) <= 10)
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
        _bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 shotDirection = transform.right;
        shotDirection.Normalize();
        
        Rigidbody2D rb = _bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shotDirection * bulletSpeed;
    }
}
