using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrCrabsScript : MonoBehaviour
{
    [SerializeField] private LeftRightDetector leftDetector;
    [SerializeField] private float speed;
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject questionMark;

    private Rigidbody2D _rb;
    private Animator _animator;

    //walking
    private bool _isWalkingActive;
    private float _xInput = 1;
    
    //animations
    private bool _idle;
    private bool _walking;
    private bool _isTalking;

    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _isWalkingActive = true;
        
        StartCoroutine(RandomStopRoutine());
    }

    private void Update()
    {
        if (!leftDetector.GroundCheck())
        {
            _xInput *= -1;
        }
        
        AnimationCheck();
        AnimationSetter();
    }

    private void FixedUpdate()
    {
        if (_isWalkingActive)
        {
            Movement();
        }
    }
    
    private void Movement()
    {
        DirectionChanger();
        _rb.velocity = new Vector2(_xInput * speed, _rb.velocity.y);
    }
    
    private void DirectionChanger()
    {
        if (_xInput < 0f)
        {
            transform.localScale = new Vector2(1f, 1f);
            interactButton.transform.localScale = new Vector2(1f, 1f);
            if (questionMark != null)
            {
                questionMark.transform.localScale = new Vector2(1f, 1f);
            }
        }
        else if (_xInput > 0f)
        {
            transform.localScale = new Vector2(-1f, 1f);
            interactButton.transform.localScale = new Vector2(-1f, 1f);
            if (questionMark != null)
            {
                questionMark.transform.localScale = new Vector2(-1f, 1f);
            }
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
        _walking = _rb.velocity.x != 0;
        _idle = _isWalkingActive;
    }
    
    private void AnimationSetter()
    {
        if (!_isTalking && _isWalkingActive)
        {
            _isWalkingActive = true;
            
            _animator.SetBool("Walking", _walking);
            _animator.SetBool("Idle", _idle);
        }
        else
        {
            TalkingAnimationSetter();
        }
    }

    private void TalkingAnimationSetter()
    {
        _isWalkingActive = false;
        
        _animator.SetBool("Walking", false);
        _animator.SetBool("Idle", true);
    }
    
    public bool IsTalking
    {
        get => _isTalking;
        set => _isTalking = value;
    }
    
    private IEnumerator RandomStopRoutine()
    {
        while (true)
        {
            if (UnityEngine.Random.value > 0.5f)
            {
                _isWalkingActive = false;
                yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
                _isWalkingActive = true;
            }
            
            yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 4f));
        }
    }

}
