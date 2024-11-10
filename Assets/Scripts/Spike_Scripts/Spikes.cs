using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.Damage(1000f, new Vector2());
        }
    }
}
