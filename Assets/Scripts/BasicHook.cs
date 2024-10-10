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
    private Vector2 _startingPosition;
    private SpringJoint2D _joint;
    private bool _isAttached;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _isAttached = false;
        _startingPosition = transform.position;
        _joint = gameObject.GetComponent<SpringJoint2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRb = _player.GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        _joint.enabled = false;
        _joint.enableCollision = true;
    }

    private void Update()
    {
        float distanceTraveled = Vector2.Distance(_playerRb.transform.position, transform.position);

        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }

        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _player.transform.position);

        if (_isAttached)
        {
            _joint.connectedBody = _playerRb;
            _joint.enabled = true;
            
            float verticalInput = Input.GetAxis("Vertical");
            if (verticalInput != 0)
            {
                _joint.distance -= verticalInput * climbSpeed * Time.deltaTime;
                _joint.distance = Mathf.Clamp(_joint.distance, 0.5f, maxDistance);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _rb.velocity = Vector2.zero;
            float playerDistance = Vector2.Distance(_player.transform.position, transform.position);
            _joint.distance = Mathf.Clamp(playerDistance, 0.5f, maxDistance);
            _isAttached = true;
        }
    }
}