using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGate : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private bool _isOpen = false;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isOpen)
        {
            _animator.SetBool("IsOpen", true);
        }
        else
        {
            _animator.SetBool("IsOpen", false);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isOpen)
        {
            Invoke(nameof(NextLevel), 0.5f);
        }
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
