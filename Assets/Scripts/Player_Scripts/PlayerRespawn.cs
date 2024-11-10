using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private RespawnPoint firstRespawnPoint;
    
    private RespawnPoint respawnPoint;
    private GameObject player;
    private PlayerHealth playerHealth;
    private HealthPotion healthPotion;

    void Start()
    {
        StartCoroutine(InitializeRespawnPoint());
        
        healthPotion = GetComponent<HealthPotion>();
        
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    private IEnumerator InitializeRespawnPoint()
    {
        yield return new WaitForSeconds(1f);
        
        respawnPoint = firstRespawnPoint;
        respawnPoint.setActive();
    }

    public void SetRespawnPoint(RespawnPoint newRespawnPoint)
    {
        if (respawnPoint != null)
        {
            respawnPoint.setInactive();
        }
        respawnPoint = newRespawnPoint;
    }

    public void Respawn()
    {
        if (respawnPoint != null)
        {
            player.transform.position = respawnPoint.transform.position;
            
            healthPotion.ResetCooldown();
            
            playerHealth.setHealth(20f);
        }
    }
}
