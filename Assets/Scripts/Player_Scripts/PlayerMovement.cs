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
    private PlayerAttack _playerAttack;
    public bool _enabled;
    public bool _isGrappled;

    [SerializeField] private float speed = 7.5f;

    [SerializeField] private float jumpForce = 7.5f;

    [SerializeField] private float coyoteTime = 0.5f;
    private float _coyoteCounter;

    [SerializeField] private float jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;
    
    [SerializeField] private GroundDetectorScript groundDetector;
    
    [SerializeField] private float forceMultiplier = 50f;
    [SerializeField] private float maxGrappleVelocity = 10f;
    
    public Vector2 _grapplePoint;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerAttack = GetComponent<PlayerAttack>();
        _enabled = true;
    }

    private void Update()
    {
        if (_enabled && !_isGrappled)
        {
            _xInput = Input.GetAxis("Horizontal");
            _isGrounded = groundDetector.GroundCheck();
            _playerAttack.SetIsFalling(!_isGrounded);

            PlayerDirectionChanger();

            AnimationChecker();
            AnimationSetter();

            Coyote();

            JumpBuffer();
        }

        if (_isGrappled && _enabled)
        {
            _xInput = Input.GetAxis("Horizontal");
            
            ApplyGrappleForce();
            
            AnimationChecker();
            AnimationSetter();
            PlayerDirectionChanger();
        }
    }

    private void FixedUpdate()
    {
        if (_isGrounded)
        {
            GroundedMovement();
        }
        if (!_isGrappled && !_isGrounded)
        {
            AerialMovement();
        }
        
        if (!_enabled)
        {
            _rb.velocity = Vector2.zero;
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

    private void ApplyGrappleForce()
    {
        if (_xInput != 0f)
        {
            Vector2 grappleToPlayer = (Vector2)transform.position - _grapplePoint;
            grappleToPlayer.Normalize();
            
            Vector2 tangentDirection = Vector2.Perpendicular(grappleToPlayer);
            if (_xInput < 0)
            {
                tangentDirection = -tangentDirection;
            }
            
            _rb.AddForce(tangentDirection * forceMultiplier * Mathf.Abs(_xInput), ForceMode2D.Force);
            
            if (_rb.velocity.magnitude > maxGrappleVelocity)
            {
                _rb.velocity = _rb.velocity.normalized * maxGrappleVelocity;
            }
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
        //Animations Without Sword
        if (!_playerAttack.GetHasSword())
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
        }

        //Animations with sword
        if (_playerAttack.GetHasSword())
        {
            if (_jumping)
            {
                _animator.SetBool("JumpSword", true);
            }
            else
            {
                _animator.SetBool("JumpSword", false);
            }

            if (_running)
            {
                _animator.SetBool("runningSword", true);
            }
            else
            {
                _animator.SetBool("runningSword", false);
            }

            if (_falling)
            {
                _animator.SetBool("FallSword", true);
            }
            else
            {
                _animator.SetBool("FallSword", false);
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        
    }
}