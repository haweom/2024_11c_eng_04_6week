using System;
using UnityEngine;

public class GroundDetectorScript : MonoBehaviour
{
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    private Transform groundCheckPosition;
    private bool _isGrounded;

    private void Awake()
    {
        groundCheckPosition = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    public bool GroundCheck()
    {
        return _isGrounded;
    }

    private void CheckGround()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundLayer);
        _isGrounded = collider;
    }
}