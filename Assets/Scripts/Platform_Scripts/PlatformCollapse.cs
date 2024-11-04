using UnityEngine;

public class PlatformCollapse : MonoBehaviour
{
    [SerializeField] private float collapseDelay = 2.0f;
    [SerializeField] private float restoreDelay = 5.0f;
    private Collider2D platformCollider;
    private SpriteRenderer platformRenderer;
    
    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        platformRenderer = GetComponent<SpriteRenderer>();
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
        platformCollider.enabled = false;
        platformRenderer.enabled = false;
        Invoke(nameof(RestorePlatform), restoreDelay);
    }

    private void RestorePlatform()
    {
        platformCollider.enabled = true;
        platformRenderer.enabled = true;
    }
}
