using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Might change to using InputSystem later
public class PlayerControls : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private Animator anim;

    // Movement
    [SerializeField] private float moveSpeed;
    private float moveInputHorizontal = 0f;
    private bool isFacingLeft = true;

    // Jump
    [SerializeField] private float jumpForce;
    private bool isJumping = false;
    private bool isGrounded = false;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int jumpTimesMax;
    private int jumpTimes = 0;

    // Combat
    // Transformation
    private bool isDemonForm = false;
    [SerializeField] private GameObject demonFormAura;
    // Melee Attack
    [SerializeField] private float attackDamage_Normal;
    [SerializeField] private float attackDamage_DemonForm;
    [SerializeField] private Transform attackCenterTransform_Normal;
    [SerializeField] private float attackRadius_Normal;
    [SerializeField] private Transform attackCenterTransform_DemonForm;
    [SerializeField] private float attackRadius_DemonForm;
    [SerializeField] private LayerMask attackLayers;
    [SerializeField] private GameObject attackEffectTemp_Normal;
    [SerializeField] private GameObject attackEffectTemp_DemonForm;
    [SerializeField] private float attackEffectDuration;
    [SerializeField] private float attackCooldown;
    private bool isInAttackCooldown = false;
    // Skill
    [SerializeField] private Transform skillStartingTransform;
    [SerializeField] private GameObject skillPrefab;
    [SerializeField] private float skillCooldown;
    private bool isInSkillCooldown = false;

    // Recovery
    [SerializeField] private float attackRecovery;
    [SerializeField] private float skillRecovery;
    private bool isInRecovery = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Movement
        if (!isInRecovery)
        {
            moveInputHorizontal = Input.GetAxisRaw("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(moveInputHorizontal));
        } else
        {
            anim.SetFloat("Speed", Mathf.Abs(moveInputHorizontal));
        }
        
        // Jump
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayer);
        if (Input.GetButtonDown("Jump") && !isInRecovery)
        {
            if (jumpTimes > 0)
            {
                isJumping = true;
            }
        }

        // Combat
        // Transformation
        if (Input.GetKeyDown(KeyCode.T) && !isInRecovery)
        {
            DemonFormTransform();
        }
        // Melee Attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isInAttackCooldown && !isInRecovery)
        {
            MeleeAttack();
        }
        // Skill
        if (Input.GetKeyDown(KeyCode.Mouse1) && isDemonForm && !isInSkillCooldown && !isInRecovery)
        {
            UseSkill();
        }
    }

    void FixedUpdate()
    {
        // Movement
        rb.velocity = new Vector2(moveInputHorizontal * moveSpeed, rb.velocity.y);
        if ((isFacingLeft && moveInputHorizontal > 0) || (!isFacingLeft && moveInputHorizontal < 0))
        {
            Flip();
        }

        // Jump
        if (isGrounded)
        {
            anim.SetBool("IsJumpInAir", false);
            ResetJumpTimes();
        }
        if (isJumping)
        {
            anim.SetBool("IsJumpInAir", false);
            anim.SetBool("IsJumpInAir", true);
            Jump();
        }
    }

    // Movement
    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        transform.Rotate(0f, 180f, 0f);
    }

    // Jump
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpTimes--;
        isJumping = false;
    }

    private void ResetJumpTimes()
    {
        jumpTimes = jumpTimesMax;
    }

    // Combat
    // Transformation
    private void DemonFormTransform()
    {
        isDemonForm = !isDemonForm;
        //Debug.Log(isDemonForm);
        demonFormAura.SetActive(isDemonForm);
    }

    // Melee Attack
    private void MeleeAttack()
    {
        Transform attackCenterTransform;
        float attackRadius;
        float attackDamage;
        if (isDemonForm)
        {
            attackCenterTransform = attackCenterTransform_DemonForm;
            attackRadius = attackRadius_DemonForm;
            attackDamage = attackDamage_DemonForm;
            attackEffectTemp_DemonForm.SetActive(true);
            StartCoroutine(AttackEffectCoroutine(attackEffectTemp_DemonForm));
        } else
        {
            attackCenterTransform = attackCenterTransform_Normal;
            attackRadius = attackRadius_Normal;
            attackDamage = attackDamage_Normal;
            attackEffectTemp_Normal.SetActive(true);
            StartCoroutine(AttackEffectCoroutine(attackEffectTemp_Normal));
        }
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackCenterTransform.position, attackRadius, attackLayers);
        foreach(Collider2D enemy in enemiesHit)
        {
            EnemyGeneral enemy_script = enemy.GetComponent<EnemyGeneral>();
            if (enemy_script != null)
            {
                enemy_script.DecreaseHP(attackDamage);
            }
        }
        StartCoroutine(AttackCooldownCoroutine());
        StartCoroutine(RecoveryCoroutine(attackRecovery));
    }

    private IEnumerator AttackEffectCoroutine(GameObject effect)
    {
        yield return new WaitForSeconds(attackEffectDuration);
        effect.SetActive(false);
    }

    private IEnumerator AttackCooldownCoroutine()
    {
        isInAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isInAttackCooldown = false;
    }

    // Skill
    private void UseSkill()
    {
        Instantiate(skillPrefab, skillStartingTransform.position, skillStartingTransform.rotation);
        StartCoroutine(SkillCooldownCoroutine());
        StartCoroutine(RecoveryCoroutine(skillRecovery));
    }

    private IEnumerator SkillCooldownCoroutine()
    {
        isInSkillCooldown = true;
        yield return new WaitForSeconds(skillCooldown);
        isInSkillCooldown = false;
    }

    private IEnumerator RecoveryCoroutine(float recoveryDuration)
    {
        isInRecovery = true;
        yield return new WaitForSeconds(recoveryDuration);
        isInRecovery = false;
    }

    // Miscellaneous
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackCenterTransform_Normal.position, attackRadius_Normal);
        Gizmos.DrawWireSphere(attackCenterTransform_DemonForm.position, attackRadius_DemonForm);
    }

    public bool getForm()
    {
        return isDemonForm;
    }
}
