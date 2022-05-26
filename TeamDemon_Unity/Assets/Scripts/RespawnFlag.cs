using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFlag : MonoBehaviour
{
    [SerializeField] private GameObject respawnManager;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerRespawn respawnScript = respawnManager.GetComponent<PlayerRespawn>();
            RespawnIndexRecorder.RespawnIndex = transform.GetSiblingIndex();
        }
    }
}
