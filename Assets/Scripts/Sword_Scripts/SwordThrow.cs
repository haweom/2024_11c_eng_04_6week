using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SwordThrow : MonoBehaviour
{
    [SerializeField] private float damageAmount = 30f;
    [SerializeField] private CircleCollider2D swordCollider;
    [SerializeField] private float throwDetectionRadius = 1.0f;
    [SerializeField] private float timeBeforeSwordReturning;

    private float returnSpeed = 10f;
    private SpriteRenderer _swordSpriteRenderer;
    private Rigidbody2D _swordRb;
    private Transform _parentTransform;
    private Transform _player;
    private bool _isFlying;
    private bool _isReturning;
    
    private HashSet<Collider2D> hasBeenHit = new HashSet<Collider2D>();

    private void Start()
    {
        _isFlying = true;
        _swordRb = GetComponentInParent<Rigidbody2D>();
        _parentTransform = transform.parent;
        _swordSpriteRenderer = GetComponentInParent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ReturnTimer());
    }

    private void Update()
    {
        if (_isFlying)
        {
            RotateSword();
            
            DetectEnemiesInRange();
        } 
        else if (_isReturning)
        {
            ReturnToPlayer();
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
            if (enemy.CompareTag("Player") || hasBeenHit.Contains(enemy)) continue;

            IDamageable damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damageAmount, new Vector2());
                hasBeenHit.Add(enemy);
            }
        }
    }

    private void ReturnToPlayer()
    {
        _swordSpriteRenderer.sortingOrder = 101;
        
        Vector2 directionToPlayer = (_player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
        
        float speed = returnSpeed + distanceToPlayer;
        _swordRb.velocity = directionToPlayer * speed;
        
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        _parentTransform.rotation = Quaternion.Euler(0, 0, angle + 180f);
        
        if (Vector2.Distance(transform.position, _player.position) < 0.5f)
        {
            _swordRb.velocity = Vector2.zero;
        }
    }
    
    private IEnumerator ReturnTimer()
    {
        yield return new WaitForSeconds(timeBeforeSwordReturning);
        
        _isFlying = false;
        _isReturning = true;
    }
    
    public bool IsReturning()
    {
        return _isReturning;
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