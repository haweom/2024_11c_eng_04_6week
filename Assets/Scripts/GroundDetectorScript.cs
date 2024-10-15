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

    public bool GroundCheck(Collision2D other)
    {
        //return other.collider.gameObject.layer == LayerMask.NameToLayer("Ground");
        return _isGrounded;
    }

    public bool LeaveCheck(Collision2D other)
    {
        //return other.collider.gameObject.layer == LayerMask.NameToLayer("Ground");
        return _isGrounded;
    }
    

    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _isGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _isGrounded = true;
    }
}