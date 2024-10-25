using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private float spawnInterval = 5f; 

    private void Start()
    {
        InvokeRepeating("SpawnBarrel", 0f, spawnInterval);
    }

    public void SpawnBarrel()
    {
        Instantiate(barrelPrefab, transform.position, Quaternion.identity);
    }
}