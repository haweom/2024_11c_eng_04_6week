using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicPlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _xInput;
    [SerializeField] private float speed = 5;

    private bool _performJump;
    private bool _isGrounded;
    [SerializeField] private float jumpForce = 5;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _xInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _performJump = true;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_xInput * speed, _rb.velocity.y);

        if (_performJump)
        {
            _performJump = false;
            _isGrounded = false;
            _rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isGrounded = false;
    }
}
