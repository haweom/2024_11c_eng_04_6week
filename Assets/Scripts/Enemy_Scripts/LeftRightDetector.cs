using UnityEngine;

public class LeftRightDetector : MonoBehaviour
{
    private bool _isGrounded;
   
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
