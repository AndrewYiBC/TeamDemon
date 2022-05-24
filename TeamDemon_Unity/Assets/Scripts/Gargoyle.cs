using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargoyle : MonoBehaviour
{
    private bool isFacingLeft = false;
    [SerializeField] private GameObject player;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCooldown;
    private bool isInAttackCooldown = false;

    private void Update()
    {
        if ((isFacingLeft && transform.position.x < player.transform.position.x) || (!isFacingLeft && transform.position.x > player.transform.position.x))
        {
            isFacingLeft = !isFacingLeft;
            transform.Rotate(0f, 180f, 0f);
        }

        if (!isInAttackCooldown && Vector2.Distance(player.transform.position, transform.position) < attackRadius)
        {
            attack();
        }
    }

    private void attack()
    {
        PlayerControls playerScript = player.GetComponent<PlayerControls>();
        if (playerScript != null)
        {
            playerScript.DecreaseHP(attackDamage);
            StartCoroutine(AttackCooldownCoroutine());
        }
    }

    private IEnumerator AttackCooldownCoroutine()
    {
        isInAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isInAttackCooldown = false;
    }
}
