using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingHook : MonoBehaviour
{
    private Rigidbody2D _rb;
    private GameObject _player;
    private Rigidbody2D _playerRb;
    private LineRenderer _lineRenderer;

    [SerializeField] private float maxDistance;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float dragAmount;
    private PlayerMovement playerMovement;
    
    private Vector2 _startingPosition;
    private SpringJoint2D _joint;
    private bool _isAttached;

    private AudioManagerScript _ams;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ams = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
    }

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        _isAttached = false;
        _startingPosition = transform.position;
        _joint = gameObject.GetComponent<SpringJoint2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRb = _player.GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        _joint.enabled = false;
        _joint.enableCollision = true;

        _ams.srcSfx.clip = _ams.hookShoot;
        _ams.srcSfx.Play();
    }

    public void OnDestroy()
    {
        _ams.srcSfx.Stop();
        if (_playerRb != null)
        {
            playerMovement._isGrappled = false;
            _playerRb.drag = 0.0f;
        }
    }

    private void Update()
    {
        if (PauseMenu.IsPaused)
        {
            _ams.srcSfx.Pause();
        }
        else
        {
            _ams.srcSfx.UnPause();
        }
        
        float distanceTraveled = Vector2.Distance(_playerRb.transform.position, transform.position);

        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
        
        Vector2 direction = _player.transform.position - transform.position;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _player.transform.position);

        if (_isAttached)
        {
            _joint.connectedBody = _playerRb;
            _joint.enabled = true;
            _playerRb.drag = dragAmount;
            
            if (playerMovement._isGrappled == false)
            {
                playerMovement._isGrappled = true;
            }

            if (playerMovement._isGrappled == true)
            {
                playerMovement._isGrounded = false;
                playerMovement._grapplePoint = this.transform.position;
            }
            
            float verticalInput = Input.GetAxis("Vertical");
            if (verticalInput != 0)
            {
                _joint.distance -= verticalInput * climbSpeed * Time.deltaTime;
                _joint.distance = Mathf.Clamp(_joint.distance, 0.5f, maxDistance);
            }

            if (_player.transform.position.y > transform.position.y)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _ams.srcSfx.Stop();
            _ams.srcSfx.PlayOneShot(_ams.hookHit);
            _rb.velocity = Vector2.zero;
            float playerDistance = Vector2.Distance(_player.transform.position, transform.position);
            _joint.distance = Mathf.Clamp(playerDistance, 0.5f, maxDistance);
            _isAttached = true;
        }else if (other.CompareTag("HookDen"))
        {
            _ams.srcSfx.PlayOneShot(_ams.hookHit);
            Destroy(gameObject);
        }
    }
}