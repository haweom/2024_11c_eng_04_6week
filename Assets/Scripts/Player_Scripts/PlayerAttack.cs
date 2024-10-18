using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    //TODO add knockback
    [SerializeField] private float knockback;
    [SerializeField] private float attackSpeed = 0.2f;
    
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;
    
    private RaycastHit2D[] _hits;

    private float _attackTimeCounter;
    
    private Animator _animator;
    private bool _isAttacking;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _attackTimeCounter = attackSpeed;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _attackTimeCounter >= attackSpeed)
        {
            _animator.SetBool("Attack-1", true);
            _attackTimeCounter = 0f;
            Attack();
        }
        _attackTimeCounter += Time.deltaTime;
    }

    private void Attack()
    {
        _hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f);
        
        for (int i = 0; i < _hits.Length; i++)
        {
            if (_hits[i].collider.CompareTag("Player"))
                continue;
            
            IDamageable damageable = _hits[i].collider.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damage);

                Rigidbody2D enemyRb = _hits[i].collider.gameObject.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    Vector2 knockbackDirection = _hits[i].transform.position - transform.position;
                    knockbackDirection.Normalize();
                    
                    enemyRb.AddForce(knockbackDirection * knockback, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }

    public void StopAttackAnimation()
    {
        _animator.SetBool("Attack-1", false);
    }
}
