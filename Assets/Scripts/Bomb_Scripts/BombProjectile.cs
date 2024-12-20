using System;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D[] _explosionCollider = null;
    private Animator _animator;
    
    [SerializeField] private float explosionForceMultiplier = 5f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float timeAlive = 3f;
    [SerializeField] private float damage = 20f;
    public float direction;
    public float throwStrength;

    private AudioManagerScript _ams;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _ams = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
    }

    private void Start()
    {
        _rb.velocity = new Vector2(speed * throwStrength * direction, 0);
    }

    private void Update()
    {
        timeAlive -= Time.deltaTime;
        if (timeAlive <= 0.13f)
        {
            transform.rotation = Quaternion.identity;
            _animator.SetBool("Destroy", true);
        }
        if (timeAlive <= 0)
        {
            Explode();
            _ams.srcSfx.PlayOneShot(_ams.explodeBomb);
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate()
    {
    }

    void Explode()
    {
        _explosionCollider = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D o in _explosionCollider)
        {
            Rigidbody2D orb = o.GetComponent<Rigidbody2D>();
            if (orb != null)
            {
                Vector2 distanceV = o.transform.position - transform.position;
                if (distanceV.magnitude > 0)
                {
                    float distance = distanceV.magnitude < 0.5 ? 0.5f : distanceV.magnitude;
                    float explosionForce = explosionForceMultiplier / distance;
                    orb.AddForce(distanceV.normalized * explosionForce);
                    /*orb.velocity = new Vector2(orb.velocity.x + (distanceV.x * explosionForce),
                        orb.velocity.y + (distanceV.y * explosionForce));*/
                }
            }
            //attack implement:
            IDamageable damageable = o.GetComponent<IDamageable>();
            if (damageable != null && !o.CompareTag("Player"))
            {
                damageable.Damage(damage, new Vector2());
            }
            BreakBlock breakBlock = o.GetComponent<BreakBlock>();
            if (breakBlock != null && o.CompareTag("Ground"))
            {
                breakBlock.breakTriggered = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}