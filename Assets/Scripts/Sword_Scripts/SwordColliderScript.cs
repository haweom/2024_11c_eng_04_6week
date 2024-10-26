using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderScript : MonoBehaviour
{
    [SerializeField] private SwordThrow swordThrow;
    
    private BoxCollider2D _boxCollider;
    private Rigidbody2D _rigidbody;

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") && _boxCollider.IsTouching(other) )
        {
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;
            swordThrow.SetIsFlying(false);
        }
    }
}