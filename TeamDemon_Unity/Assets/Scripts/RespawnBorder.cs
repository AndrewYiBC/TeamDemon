using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBorder : MonoBehaviour
{
    [SerializeField] private GameObject respawnManager;
    private PlayerRespawn respawnScript;

    void Start()
    {
        respawnScript = respawnManager.GetComponent<PlayerRespawn>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            respawnScript.RespawnPlayer();
        }
    }
}
