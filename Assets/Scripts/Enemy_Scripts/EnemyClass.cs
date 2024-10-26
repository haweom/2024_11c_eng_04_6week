using System.Collections;
using UnityEngine;

public class EnemyClass : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rb;
    private Animator _animator;
    [SerializeField] private LeftRightDetector leftDetector;
    
    private float _xInput;
    [SerializeField] private float speed = 5f;
    private bool _running;
    [SerializeField] private float jumpForce = 5f;
    public bool hit;
    [SerializeField] public bool attacking;

    [SerializeField] private float maxHealth = 50f;
    public float currentHealth;
    private Collider2D[] _attackCollider;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackRange = 2f;

    [SerializeField] private VisionDetectorScript visionDetectorScript; 
    private GameObject _player;
    [SerializeField] private float chaseTime = 2.5f;
    private float _chaseCounter;
    
    private bool _alive;
    private bool _patrol;
    private bool _chase;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Start()
    {
        _xInput = 1;
        currentHealth = maxHealth;
        _alive = true;
    }
    
    private void Update()
    {
        if (visionDetectorScript.VisionRayCast(_rb))
        {
            _chase = true;
            _chaseCounter = chaseTime;
        }
        else if (_chase)
        {
            _chaseCounter -= Time.deltaTime;
            if (_chaseCounter <= 0)
            {
                _chase = false;
            }
        }
        
        AnimationCheck();
        if (_alive && !attacking)
        {
            _patrol = !hit && !_chase;

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
            
            
            if (attacking) //tmp loop for attacking tests
            {
                Attack();
            }
            else
            {
                if (_patrol)
                {
                    _xInput = _xInput == 0?  transform.localScale.x : _xInput;
                    Movement();
                }

                if (_chase)
                {
                    _xInput = 0;
                    Chase();
                    
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
        DirectionChanger();
        _rb.velocity = new Vector2(_xInput * speed, _rb.velocity.y);
    }

    private void Chase()
    {
        if (_player.transform.position.x - transform.position.x > 0)
        {
            _xInput = 1;
        }
        else
        {
            _xInput = -1;
        }
        bool jump = visionDetectorScript.JumpRayCast(_rb, _xInput);
        if (jump)
        {
            Jump();
        }
        Movement();
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
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

        if (_xInput == 0)
        {
            _xInput = transform.localScale.x;
        }
        
        if (_rb.velocity.x == 0)
        {
            _xInput *= -1f;
        }
    }

    private void AnimationCheck()
    {
        _running = _rb.velocity.x != 0;
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
