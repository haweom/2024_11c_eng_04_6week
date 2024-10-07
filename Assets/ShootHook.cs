using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHook : MonoBehaviour
{
    [SerializeField] private GameObject _graplingHook;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _speed;

    private GameObject hook;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hook == null)
            {
                shootGraplingHook();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(hook);
        }
    }

    private void shootGraplingHook()
    {
        hook = Instantiate(_graplingHook, _shootPoint.position, Quaternion.identity);

        Vector2 shootDirection = getMouseDirection();

        Rigidbody2D hookRb = hook.GetComponent<Rigidbody2D>();
        hookRb.velocity = shootDirection * _speed;
    }

    private Vector2 getMouseDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - _shootPoint.position).normalized;

        return direction;
    }
}