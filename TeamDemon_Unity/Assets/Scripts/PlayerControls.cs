using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Might change to using InputSystem later
public class PlayerControls : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;

    // Moving
    [SerializeField] private float moveSpeed;
    private float moveInputHorizontal = 0f;

    // Jumping
    [SerializeField] private float jumpForce;
    private bool isJumping = false;
    private bool isGrounded = false;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInputHorizontal = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayer);
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                isJumping = true;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInputHorizontal * moveSpeed, rb.velocity.y);
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = false;
        }
    }
}