using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public LayerMask attackLayerMask;

    [SerializeField] private Transform attackCenterPointTransform;
    [SerializeField] private float attackDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(attackCenterPointTransform.position, attackRange, attackLayerMask);
        if (colInfo != null)
        {
            //add stuff about player health deduction here
            PlayerControls playerScript = colInfo.GetComponent<PlayerControls>();
            if (playerScript != null)
            {
                playerScript.DecreaseHP(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackCenterPointTransform.position, attackRange);
    }
}
