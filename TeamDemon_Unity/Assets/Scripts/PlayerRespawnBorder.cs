using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnBorder : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        player.transform.position = respawnPoint.transform.position;
    }
}
