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
    private bool isInDemonForm = false;
    [SerializeField] private GameObject demonFormIndicatorTemp;
    // Skill
    [SerializeField] private Transform skillStartingTransform;
    [SerializeField] private GameObject skillPrefab;
    [SerializeField] private float skillCooldown;
    private bool isInSkillCooldown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Movement
        moveInputHorizontal = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(moveInputHorizontal));
        
        // Jump
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayer);
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpTimes > 0)
            {
                isJumping = true;
            }
        }

        // Combat
        // Transformation
        if (Input.GetKeyDown(KeyCode.T))
        {
            DemonFormTransform();
        }
        // Skill
        if (Input.GetKeyDown(KeyCode.Mouse1) && isInDemonForm && !isInSkillCooldown)
        {
            UseSkill();
        }
    }

    void FixedUpdate()
    {
        // Movement
        rb.velocity = new Vector2(moveInputHorizontal * moveSpeed, rb.velocity.y);
        if (isFacingLeft && moveInputHorizontal > 0)
        {
            Flip();
        } else if (!isFacingLeft && moveInputHorizontal < 0)
        {
            Flip();
        }

        // Jump
        if (isGrounded)
        {
            ResetJumpTimes();
        }
        if (isJumping)
        {
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
        isInDemonForm = !isInDemonForm;
        demonFormIndicatorTemp.SetActive(isInDemonForm);
    }

    // Skill
    private void UseSkill()
    {
        Instantiate(skillPrefab, skillStartingTransform.position, skillStartingTransform.rotation);
        isInSkillCooldown = true;
        StartCoroutine(SkillCooldownCoroutine());
    }

    private IEnumerator SkillCooldownCoroutine()
    {
        yield return new WaitForSeconds(skillCooldown);
        isInSkillCooldown = false;
    }
}
