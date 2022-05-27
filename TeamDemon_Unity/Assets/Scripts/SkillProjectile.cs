using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float flySpeed;
    [SerializeField] private float skillDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * flySpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            EnemyGeneral enemyScript = collision.GetComponent<EnemyGeneral>();
            if (enemyScript != null)
            {
                enemyScript.DecreaseHP(skillDamage);
            }
            if (collision.gameObject.tag != "Interactable")
            {
                Destroy(gameObject);
            }
        }
    }
}
