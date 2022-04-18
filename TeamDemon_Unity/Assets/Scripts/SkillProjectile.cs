using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float flySpeed = 15f;
    [SerializeField] private float skillDamage = 20f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * flySpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            EnemyGeneral enemy = collision.GetComponent<EnemyGeneral>();
            if (enemy != null)
            {
                enemy.DecreaseHP(skillDamage);
            }
            Destroy(gameObject);
        }
    }
}
