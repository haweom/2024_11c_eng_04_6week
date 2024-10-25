using System.Collections;
using UnityEngine;

public class EnemyClass : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rb;
    private Animator _animator;
    [SerializeField] private LeftRightDetector leftDetector;
    
    private float _xInput;
    [SerializeField] private float speed = 5;
    private bool _running;
    public bool hit;
    [SerializeField] public bool attacking;

    [SerializeField] private float maxHealth = 50f;
    public float currentHealth;
    private Collider2D[] _attackCollider;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackRange = 2f;

    [SerializeField] private VisionDetectorScript visionDetectorScript; 
    
    private bool _alive;
    private bool _patrol;
    private bool _chase;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        _xInput = 1;
        currentHealth = maxHealth;
        _alive = true;
    }
    
    private void Update()
    {
        //
       
        //
        if (_alive && !attacking)
        {
            _running = _xInput != 0 && !hit;

            if (!leftDetector.GroundCheck())
            {
                _xInput *= -1;
            }
            
            AnimationSetter();
        }
    }

    private void FixedUpdate()
    {
        if (_alive)
        {
            attacking = visionDetectorScript.AttackRayCast(_rb, _xInput);
            visionDetectorScript.VisionRayCast(_rb);
            
            if (attacking) //tmp loop for attacking tests
            {
                Attack();
            }
            else
            {
                if (_running)
                {
                    DirectionChanger();
                    Movement();
                }
                if (currentHealth <= 0)
                {
                    Die();
                }
            }
        }
    }
    //Combat:
    public void Damage(float damage)
    {
        currentHealth -= damage;
        hit = true;
    }

    public void Die()
    {
        _alive = false;
        _animator.SetTrigger("DeathHit");
        _rb.velocity = new Vector2(0, 0);
    }

    private void Attack()
    {
        _animator.SetTrigger("Attacking");
        _attackCollider = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D o in _attackCollider)
        {
            IDamageable damageable = o.GetComponent<IDamageable>();
            if (damageable != null && o.CompareTag("Player"))
            {
                damageable.Damage(damage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    //Movement:
    private void Movement()
    {
        _rb.velocity = new Vector2(_xInput * speed, _rb.velocity.y);
    }
    private void DirectionChanger()
    {
        if (_xInput < 0f)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (_xInput > 0f)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }

        if (_rb.velocity.x == 0)
        {
            float t = transform.localScale.z * -1f;
            transform.localScale = new Vector2(t, 1f);
        }
    }
    
    private void AnimationSetter()
    {
        if (_running)
        {
            _animator.SetBool("Running", true);
        }
        else
        {
            _animator.SetBool("Running", false);
        }
        if (hit)
        {
            _animator.SetTrigger("Hit");
        }
    }
    
}
