using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingOrb : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerControls playerScript = collision.GetComponent<PlayerControls>();
            if (playerScript != null)
            {
                playerScript.RestoreHPFull();
                Destroy(gameObject);
            }
        }
    }
}
