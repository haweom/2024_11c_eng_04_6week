using UnityEngine;

public class GroundDetectorScript : MonoBehaviour
{

    private bool _isGrounded;
    void Start()
    {
    }

    void Update()
    {
    }

    public bool GroundCheck()
    {
        return _isGrounded;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _isGrounded = true; 
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _isGrounded = false; 
        }
    }
}