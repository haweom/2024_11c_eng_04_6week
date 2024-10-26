using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private List<GameObject> coinPrefabs;
    [SerializeField] private List<Vector2> coinSpawnOffsets;
    private Animator animator;
    private bool isOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            isOpened = true;
            animator.SetBool("Open", true);
            SpawnCoins();
        }
    }

    private void SpawnCoins()
    {
        if (coinPrefabs.Count == 0 || coinSpawnOffsets.Count == 0)
        {
            Debug.LogWarning("No coin prefabs or spawn offsets assigned!");
            return;
        }

        for (int i = 0; i < coinSpawnOffsets.Count; i++)
        {
            GameObject coinPrefab = coinPrefabs[Random.Range(0, coinPrefabs.Count)];
            Vector2 spawnPosition = (Vector2)transform.position + coinSpawnOffsets[i];
            GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 jumpForce = new Vector2(Random.Range(-1f, 1f), 1f) * 5f;
                rb.AddForce(jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Open", false);
        }
    }
}