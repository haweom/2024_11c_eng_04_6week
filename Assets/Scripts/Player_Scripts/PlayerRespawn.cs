using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private RespawnPoint respawnPoint;
    private GameObject player;
    private PlayerHealth playerHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
        respawnPoint = GetComponent<RespawnPoint>();
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
            
            playerHealth.setHealth(20f);
        }
    }
}
