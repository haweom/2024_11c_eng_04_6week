using UnityEngine;

public class RollingBarrel : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 250f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float damage = 50f;

    private void Update()
    {
        transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);

        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damage);
            }
            Destroy(gameObject);
        }


    }
}