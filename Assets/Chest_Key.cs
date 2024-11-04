using System.Collections;
using UnityEngine;

public class ChestKey : MonoBehaviour
{
    private Animator _animator;
    private bool _isCollected;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Collected", false);
        _isCollected = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isCollected)
        {
            _isCollected = true;
            _animator.SetBool("Collected", true);
            GetComponent<Collider2D>().enabled = false;

        }
    }

    public void DestroyKey()
    {
        Destroy(gameObject);
    }
}