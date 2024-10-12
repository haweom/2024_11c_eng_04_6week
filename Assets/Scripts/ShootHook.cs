using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHook : MonoBehaviour
{
    [SerializeField] private GameObject graplingHook;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float speed;

    private GameObject _hook;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_hook == null)
            {
                ShootGraplingHook();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(_hook);
        }
    }

    private void ShootGraplingHook()
    {
        _hook = Instantiate(graplingHook, shootPoint.position, Quaternion.identity);

        Vector2 shootDirection = GetMouseDirection();

        Rigidbody2D hookRb = _hook.GetComponent<Rigidbody2D>();
        hookRb.velocity = shootDirection * speed;
    }

    private Vector2 GetMouseDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - shootPoint.position).normalized;

        return direction;
    }
}