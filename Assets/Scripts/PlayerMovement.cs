using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _xInput;
    [SerializeField] private float speed = 7.5f;
    
    private bool _isGrounded;
    [SerializeField] private float jumpForce = 7.5f;

    [SerializeField] private float coyoteTime = 0.5f;
    private float _coyoteCounter;

    [SerializeField] private float jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;
    
    
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _xInput = Input.GetAxis("Horizontal");

        if (_isGrounded)
        {
            _coyoteCounter = coyoteTime;
        }
        else
        {
            _coyoteCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
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

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_xInput * speed, _rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isGrounded = false;
    }

    private void PerformJump(float jumpModified)
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpModified);
        _isGrounded = false;
    }
}
