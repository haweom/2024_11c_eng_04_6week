using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderScript : MonoBehaviour
{
    [SerializeField] private SwordThrow swordThrow;
    
    private BoxCollider2D _boxCollider;
    private Rigidbody2D _rigidbody;
    private AudioManagerScript _ams;
    void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _ams = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") && _boxCollider.IsTouching(other) )
        {
            _ams.srcSfx.PlayOneShot(_ams.swordThrowHit);
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;
            swordThrow.SetIsFlying(false);
        }
    }
}