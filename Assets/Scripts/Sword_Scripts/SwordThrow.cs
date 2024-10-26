using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordThrow : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private CircleCollider2D swordCollider;
    [SerializeField] private float throwDetectionRadius = 1.0f;

    private Rigidbody2D _swordRb;
    private Transform _parentTransform;
    private bool _isFlying;

    private void Start()
    {
        _isFlying = true;
        _swordRb = GetComponentInParent<Rigidbody2D>();
        _parentTransform = transform.parent;
    }

    private void Update()
    {
        if (_isFlying)
        {
            RotateSword();
            
            DetectEnemiesInRange();
        }
    }
    
    private void RotateSword()
    {
        if (_swordRb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(_swordRb.velocity.y, _swordRb.velocity.x) * Mathf.Rad2Deg;
            _parentTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void DetectEnemiesInRange()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, throwDetectionRadius);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Player")) continue;

            IDamageable damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damageAmount);
                _isFlying = false;
                break;
            }
        }
    }

    public void SetIsFlying(bool isFlying)
    {
        _isFlying = isFlying;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, throwDetectionRadius);
    }
}