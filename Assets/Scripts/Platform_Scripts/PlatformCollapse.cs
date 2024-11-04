using UnityEngine;

public class PlatformCollapse : MonoBehaviour
{
    [SerializeField] private float collapseDelay = 2.0f;
    [SerializeField] private float restoreDelay = 5.0f;
    private Collider2D platformCollider;
    private Rigidbody2D platformRigidbody;
    private Vector3 initialPosition;

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        platformRigidbody = GetComponent<Rigidbody2D>();
    
        platformRigidbody.bodyType = RigidbodyType2D.Dynamic;
        platformRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(CollapsePlatform), collapseDelay);
        }
    }

    private void CollapsePlatform()
    {
        platformRigidbody.gravityScale = 2;
        platformRigidbody.constraints = RigidbodyConstraints2D.None;
        platformCollider.enabled = false;
        Invoke(nameof(TeleportPlatformBack), restoreDelay);
    }

    private void TeleportPlatformBack()
    {
        platformRigidbody.velocity = Vector2.zero;
        transform.position = initialPosition;
        platformRigidbody.gravityScale = 0;
        platformRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        platformCollider.enabled = true;
    }
}