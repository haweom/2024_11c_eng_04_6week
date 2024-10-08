using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingHook : MonoBehaviour
{
    private Rigidbody2D _rb;
    private GameObject _player;
    private Rigidbody2D _playerRb;

    [SerializeField] Vector2 downwardForce = new Vector2(0, -5f);
    [SerializeField] private float _maxDistance;
    //calculating Distance travelled
    private Vector2 _startingPosition;
    //want to anchor with player
    private DistanceJoint2D joint;
    private bool isAttached = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _startingPosition = transform.position;
        joint = gameObject.GetComponent<DistanceJoint2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRb = _player.GetComponent<Rigidbody2D>();
        joint.enabled = false;
    }

    private void Update()
    {
        float distanceTraveled = Vector2.Distance(_startingPosition, transform.position);
        
        if (distanceTraveled >= _maxDistance)
        {
            Destroy(gameObject);
            isAttached = false;
        }
        

        if (isAttached)
        {
            joint.connectedBody = _playerRb;
            joint.enabled = true;
            float playerDistance = Vector2.Distance(_player.transform.position, transform.position);
            joint.distance = Mathf.Clamp(playerDistance, 0, _maxDistance);
            
            _playerRb.AddForce(downwardForce, ForceMode2D.Force);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _rb.velocity = Vector2.zero;
            isAttached = true;
        }
    }
    
}