using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemyProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float flySpeed;
    [SerializeField] private float projectileDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * flySpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            PlayerControls player = collision.GetComponent<PlayerControls>();
            if (player != null)
            {
                player.DecreaseHP(projectileDamage);
            }
            if (collision.gameObject.tag != "Interactable")
            {
                Destroy(gameObject);
            }
        }
    }
}
