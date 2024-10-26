using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetectorScript : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private float visionRange;
    private bool _isPlayer;
    private GameObject _player;

    private void Start()
    {
        attackRange = 1.5f;
        visionRange = 7.5f;
        _isPlayer = false;
        _player = GameObject.FindGameObjectWithTag("Player");
    }


    public bool JumpRayCast(Rigidbody2D rb, float direction)
    { //Mundek dundek tu był
        RaycastHit2D ray = Physics2D.Raycast(rb.transform.position,
            new Vector2(direction, 0f), 1f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(new Vector3(rb.transform.position.x,rb.transform.position.y + 0.25f),
            new Vector2(direction, 0) * 1f, Color.green);

        if (ray.collider != null)
        {
            if (ray.collider.CompareTag("Ground"))
            {
                //Debug.Log(ray.collider.transform.root.gameObject);
                return true;
            }
        }
        
        return false;
    }
    
    public bool VisionRayCast(Rigidbody2D rb)
    {
        int layerMask = LayerMask.GetMask("Ground", "Player");
        RaycastHit2D ray = Physics2D.Raycast(rb.transform.position, 
            (_player.transform.position - rb.transform.position), visionRange, layerMask);
        
        Debug.DrawRay(rb.transform.position, (_player.transform.position - rb.transform.position), Color.red);
        
        if (ray.collider != null)
        {
            //Debug.Log(ray.transform.root.gameObject);
            if (ray.collider.CompareTag("Player"))
            {
                Debug.DrawRay(rb.transform.position,
                    (_player.transform.position - rb.transform.position), Color.cyan);
                return true;
            }
        }


        return false;
    }
    
    public bool AttackRayCast(Rigidbody2D rb, float direction)
    {
        direction = direction == 0 ? -1 : direction;
        
        
        RaycastHit2D[] ray = Physics2D.RaycastAll(rb.transform.position,
            new Vector2(direction, 0f), attackRange, ~0);
        Debug.DrawRay(rb.transform.position, new Vector2(direction, 0) * attackRange, Color.magenta);

        foreach (var hit in ray)
        {
            if (hit.collider != null)
            {
                _isPlayer = hit.collider.CompareTag("Player");
                if (_isPlayer)
                {
                    //Debug.Log("Player found");    
                    Debug.DrawRay(rb.transform.position, new Vector2(direction, 0) * attackRange, Color.blue);
                    return true;
                }
            }
        }
        return false;
    }
}
