using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingEnemy_Patrol : StateMachineBehaviour
{

    Transform player;
    Rigidbody2D rb;

    private bool isFacingLeft = true;
    private float xLimLeft;
    private float xLimRight;

    public float speed = 2.5f;
    public float attackRange = 3f;
    public float patrolRange = 20f;
    float patrolTimeTracker = 0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        PatrollingEnemyRangeLimit rangeLimitScript = animator.GetComponent<PatrollingEnemyRangeLimit>();
        xLimLeft = rangeLimitScript.xLimLeft;
        xLimRight = rangeLimitScript.xLimRight;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        if (newPos.x > xLimLeft && newPos.x < xLimRight)
        {
            rb.MovePosition(newPos); //should not follow player, shouls patrol a region
        }
        if ((isFacingLeft && newPos.x < player.position.x) || (!isFacingLeft && newPos.x > player.position.x))
        {
            isFacingLeft = !isFacingLeft;
            animator.transform.Rotate(0f, 180f, 0f);
        }

        //move to attack stuff
        //if (Vector2.Distance(player.position, rb.position) <= attackRange)
        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }

        //move to idle stuff
        //if (Vector2.Distance(player.position, rb.position) >= animator.GetFloat("IdleDist"))
        //{
        //    animator.SetTrigger("Idle");
        //}

        //move to patrol stuff
        //if out of range for too long
        //  go to patrol
        if (Vector2.Distance(player.position, rb.position) >= patrolRange)
        {
            patrolTimeTracker += Time.fixedDeltaTime;
        }
        else 
        {
            patrolTimeTracker = 0;
        }
        
        if (patrolTimeTracker >= animator.GetFloat("PatrolTime"))
        {
            animator.SetTrigger("Patrol");
        }

        if (Vector2.Distance(player.position, rb.position) < animator.GetFloat("IdleDist"))
        {
            animator.SetTrigger("Chase");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        //animator.ResetTrigger("Idle");
        //animator.ResetTrigger("Patrol");
        animator.ResetTrigger("Chase");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
