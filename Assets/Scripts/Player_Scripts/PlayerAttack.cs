using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float knockback;
    [SerializeField] private float attackSpeed = 0.2f;
    
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private float throwSpeed;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float requiredButtonHoldTime;
    private float holdTime;

    [SerializeField] private Transform attackTransform;
    [SerializeField] private Transform attackDownTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;

    [SerializeField] private GameObject attackStroke1;
    [SerializeField] private GameObject attackStroke2;
    [SerializeField] private GameObject attackStroke3;

    [SerializeField] private GameObject airStroke1;
    [SerializeField] private GameObject airStroke2;

    private RaycastHit2D[] _hits;
    private Animator _animator;

    private float _attackTimeCounter;
    private bool _isAttacking;
    private bool _isFalling;
    private bool _hasSword;
    private int _fallAttackCounter;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _attackTimeCounter = attackSpeed;
        _fallAttackCounter = 1;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && _attackTimeCounter >= attackSpeed && !_isFalling && _hasSword)
        {
            GroundAnimation();
            _attackTimeCounter = 0f;
            Attack();
        }

        if (Input.GetMouseButtonUp(0) && _isFalling && _attackTimeCounter >= attackSpeed && _hasSword)
        {
            AirAnimation();
            _attackTimeCounter = 0f;
            fallAttack();
        }
        
        if (Input.GetMouseButton(0) && _hasSword)
        {
            holdTime += Time.deltaTime;
            if (holdTime >= requiredButtonHoldTime)
            {
                throwAttack();
                holdTime = 0f;
            }
        }
        else
        {
            holdTime = 0f;
        }

        _attackTimeCounter += Time.deltaTime;
    }

    private void GroundAnimation()
    {
        int randomNumber = UnityEngine.Random.Range(1, 4);
        GameObject effect = null;

        if (randomNumber == 1)
        {
            _animator.SetBool("Attack-1", true);
            effect = Instantiate(attackStroke1, attackTransform.position, attackTransform.rotation);
        }

        if (randomNumber == 2)
        {
            _animator.SetBool("Attack-2", true);
            effect = Instantiate(attackStroke2, attackTransform.position, attackTransform.rotation);
        }

        if (randomNumber == 3)
        {
            _animator.SetBool("Attack-3", true);
            effect = Instantiate(attackStroke3, attackTransform.position, attackTransform.rotation);
        }

        if (effect != null && transform.localScale.x < 0)
        {
            Vector3 effectScale = effect.transform.localScale;
            effectScale.x *= -1;
            effect.transform.localScale = effectScale;
        }
    }

    private void AirAnimation()
    {
        GameObject effect = null;

        if (_fallAttackCounter == 1)
        {
            _animator.SetBool("FallAttack-1", true);
            effect = Instantiate(airStroke1, attackDownTransform.position, attackDownTransform.rotation);
        }

        if (_fallAttackCounter == 2)
        {
            _animator.SetBool("FallAttack-2", true);
            effect = Instantiate(airStroke2, attackDownTransform.position, attackDownTransform.rotation);
            _fallAttackCounter = 0;
        }

        if (effect != null && transform.localScale.x < 0)
        {
            Vector3 effectScale = effect.transform.localScale;
            effectScale.x *= -1;
            effect.transform.localScale = effectScale;
        }

        _fallAttackCounter++;
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
                //damageable.Damage(damage, );

                Rigidbody2D enemyRb = _hits[i].collider.gameObject.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    Vector2 knockbackDirection = _hits[i].transform.position - transform.position;
                    knockbackDirection.Normalize();
                    
                    damageable.Damage(damage, knockbackDirection * knockback);
                    //enemyRb.AddForce(knockbackDirection * knockback, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void fallAttack()
    {
        _hits = Physics2D.CircleCastAll(attackDownTransform.position, attackRange, Vector2.down, 0f);

        for (int i = 0; i < _hits.Length; i++)
        {
            if (_hits[i].collider.CompareTag("Player"))
                continue;

            IDamageable damageable = _hits[i].collider.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                //damageable.Damage(damage);

                Rigidbody2D enemyRb = _hits[i].collider.gameObject.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    Vector2 knockbackDirection = _hits[i].transform.position - transform.position;
                    knockbackDirection.Normalize();

                    damageable.Damage(damage, knockbackDirection * knockback);
                   // enemyRb.AddForce(knockbackDirection * knockback, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void throwAttack()
    {
        _animator.SetBool("hasSword", false);
        
        Vector2 direction = GetMouseDirection();
        
        GameObject thrownSword = Instantiate(swordPrefab, throwPoint.position, Quaternion.identity);
        
        Rigidbody2D thrownSwordRb = thrownSword.GetComponent<Rigidbody2D>();
        
        thrownSwordRb.velocity = direction * throwSpeed;

        _hasSword = false;
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
        Gizmos.DrawWireSphere(attackDownTransform.position, attackRange);
    }
    
    private Vector2 GetMouseDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - throwPoint.position).normalized;

        return direction;
    }

    public void StopAttackAnimation()
    {
        _animator.SetBool("Attack-1", false);
        _animator.SetBool("Attack-2", false);
        _animator.SetBool("Attack-3", false);
        _animator.SetBool("FallAttack-1", false);
        _animator.SetBool("FallAttack-2", false);
    }
}