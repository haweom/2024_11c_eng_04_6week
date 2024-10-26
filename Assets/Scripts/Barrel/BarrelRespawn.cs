using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float spawnRange = 10f;
    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("CheckAndSpawnBarrel", 0f, spawnInterval);
    }

    private void CheckAndSpawnBarrel()
    {
        if (_playerTransform != null && Vector2.Distance(transform.position, _playerTransform.position) <= spawnRange)
        {
            SpawnBarrel();
        }
    }

    public void SpawnBarrel()
    {
        Instantiate(barrelPrefab, transform.position, Quaternion.identity);
    }
}