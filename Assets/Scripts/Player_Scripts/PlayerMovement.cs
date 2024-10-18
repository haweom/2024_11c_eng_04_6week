using System;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    public bool _isGrounded;

    private Animator _animator;
    private bool _running;
    private bool _jumping;
    private bool _falling;
    private float _xInput;

    [SerializeField] private float speed = 7.5f;

    [SerializeField] private float jumpForce = 7.5f;

    [SerializeField] private float coyoteTime = 0.5f;
    private float _coyoteCounter;

    [SerializeField] private float jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;
    
    [SerializeField] private GroundDetectorScript groundDetector;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _xInput = Input.GetAxis("Horizontal");
        _isGrounded = groundDetector.GroundCheck();
        
        PlayerDirectionChanger();

        AnimationChecker();
        AnimationSetter();

        Coyote();

        JumpBuffer();
    }

    private void FixedUpdate()
    {
        if (_isGrounded)
        {
            GroundedMovement();
        }
        else
        {
            AerialMovement();
        }
        
    }

    private void AerialMovement()
    {
        if (_xInput != 0f)
        {
            _rb.velocity = new Vector2(_xInput * speed, _rb.velocity.y);
        }
    }

    private void GroundedMovement()
    {
        _rb.velocity = new Vector2(_xInput * speed, _rb.velocity.y);
    }
    
    private void PerformJump(float jumpModified)
    {
        _jumping = true; //needs to be readjusted for animationchecker func
        _rb.velocity = new Vector2(_rb.velocity.x, jumpModified);
    }

    private void Coyote()
    {
        if (_isGrounded)
        {
            _coyoteCounter = coyoteTime;
        }
        else
        {
            _coyoteCounter -= Time.deltaTime;
        }

        if (_coyoteCounter > 0f && _jumpBufferCounter > 0f)
        {
            PerformJump(jumpForce);
            _jumpBufferCounter = 0f;
        }

        if (_rb.velocity.y > 0f && Input.GetButtonUp("Jump"))
        {
            PerformJump(_rb.velocity.y * 0.5f);
            _coyoteCounter = 0;
        }
    }

    private void JumpBuffer()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void PlayerDirectionChanger()
    {
        if (_xInput > 0f)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (_xInput < 0f)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    }

    private void AnimationChecker()
    {
        if (!_isGrounded)
        {
            _running = false;
            if (_rb.velocity.y < 0)
            {
                _falling = true;
                _jumping = false;
            }
            else if(_rb.velocity.y > 0)
            {
                _jumping = true;
                _falling = false;
            }
            else
            {
                _jumping = false;
                _falling = false;
            }
        }
        else
        {
            _running = _xInput != 0;
            _jumping = false;
            _falling = false;
        }
    }

    private void AnimationSetter()
    {
        if (_jumping)
        {
            _animator.SetBool("Jump", true);
        }
        else
        {
            _animator.SetBool("Jump", false);
        }

        if (_running)
        {
            _animator.SetBool("running", true);
        }
        else
        {
            _animator.SetBool("running", false);
        }

        if (_falling)
        {
            _animator.SetBool("Fall", true);
        }
        else
        {
            _animator.SetBool("Fall", false);
        }
        
        /*if(_isGrounded)
        {
            _animator.SetBool("Grounded", true);
        }
        else
        {
            _animator.SetBool("Grounded", false);
        }*/
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        
    }
}