using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    [SerializeField] private LeftRightDetector leftDetector;
    
    private float _xInput;
    [SerializeField] private float speed = 5;

    private bool _running;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        _xInput = -1;
    }

    
    private void Update()
    {
        _running = _xInput != 0;

        if (!leftDetector.GroundCheck())
        {
            _xInput *= -1;
        }

        
        AnimationSetter();
    }

    private void FixedUpdate()
    {
        DirectionChanger();
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
        
    }
    
    
}
