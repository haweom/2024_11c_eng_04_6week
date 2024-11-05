using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    public bool breakTriggered = false;
    
    private Collider2D _blockCollider;
    private Rigidbody2D _blockRigidbody;
    private SpriteRenderer _spriteRenderer;
 
    void Start()
    {
        _blockRigidbody = GetComponent<Rigidbody2D>();
        _blockCollider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _blockRigidbody.bodyType = RigidbodyType2D.Dynamic;
        _blockRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    
    void Update()
    {
        if (breakTriggered)
        {
            BlockBreaker();
        }
    }
    
    private void BlockBreaker()
    {
        _blockRigidbody.gravityScale = 2;
        _blockRigidbody.constraints = RigidbodyConstraints2D.None;
        _blockCollider.enabled = false;
        _spriteRenderer.enabled = false;
    }
}
