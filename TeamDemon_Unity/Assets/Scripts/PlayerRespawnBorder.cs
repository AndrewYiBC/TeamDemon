using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnBorder : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    private Vector3 respawnOffset = new Vector3(0f, 3f, 0f);

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RespawnPlayer(collision);
        }
    }

    private void RespawnPlayer(Collider2D collision)
    {
        player.transform.position = respawnPoint.transform.position + respawnOffset;
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }
}
