using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float knockback;
    [SerializeField] private float attackSpeed = 0.2f;
    
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;
    
    private RaycastHit2D[] _hits;
    private Animator _animator;

    private float _attackTimeCounter;
    private bool _isAttacking;
    private bool _isFalling;
    private bool _hasSword;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _attackTimeCounter = attackSpeed;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _attackTimeCounter >= attackSpeed && !_isFalling && _hasSword)
        {
            int randomNumber = UnityEngine.Random.Range(1, 4);
            if (randomNumber == 1) _animator.SetBool("Attack-1", true);
            if (randomNumber == 2) _animator.SetBool("Attack-2", true);
            if (randomNumber == 3) _animator.SetBool("Attack-3", true);
            
            _attackTimeCounter = 0f;
            Attack();
        }

        if (Input.GetMouseButtonDown(0) && _isFalling && _attackTimeCounter >= attackSpeed && _hasSword)
        {
            
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

    private void fallAttack()
    {
        
    }

    public void SetIsFalling(bool isFalling)
    {
        _isFalling = isFalling;
    }

    public void SetHasSword(bool hasSword)
    {
        _hasSword = hasSword;
        _animator.SetBool("hasSword", true);
    }

    public bool GetHasSword()
    {
        return _hasSword;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }

    public void StopAttackAnimation()
    {
        _animator.SetBool("Attack-1", false);
        _animator.SetBool("Attack-2", false);
        _animator.SetBool("Attack-3", false);
    }
}
