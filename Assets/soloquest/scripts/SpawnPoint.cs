using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float cooldown = 1;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(cooldown);
        }
    }
}
