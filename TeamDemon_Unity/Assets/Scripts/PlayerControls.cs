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
    [SerializeField] private Transform skillStartingTransform;
    [SerializeField] private GameObject skillPrefab;

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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            UseSkill();
        }
    }

    void FixedUpdate()
    {
        // Moving
        rb.velocity = new Vector2(moveInputHorizontal * moveSpeed, rb.velocity.y);
        if (isFacingLeft && moveInputHorizontal > 0)
        {
            Flip();
        } else if (!isFacingLeft && moveInputHorizontal < 0)
        {
            Flip();
        }

        // Jumping
        if (isGrounded)
        {
            ResetJumpTimes();
        }
        if (isJumping)
        {
            Jump();
        }
    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        transform.Rotate(0f, 180f, 0f);
    }

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

    private void UseSkill()
    {
        Instantiate(skillPrefab, skillStartingTransform.position, skillStartingTransform.rotation);
    }
}
