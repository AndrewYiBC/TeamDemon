using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public LayerMask attackLayerMask;


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
        Vector3 pos = transform.position;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackLayerMask);
        if (colInfo != null)
        {
            //add stuff about player health deduction here
        }
    }
}
