using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    Vector3 move;
    Vector3 velocity;

    public float gravity = -9.81f;
    public float speed = 5f;
    public float crouchSpeed = 2f;
    public float sprintSpeed = 8f;
    public float jumpHeight = 2f;

    public CharacterController controller;
    public Animator animator;

    private bool isCrouching = false;
    private bool isSprinting = false;
    private bool isGrounded;
    private bool isJumping = false;

    public bool IsCrouching => isCrouching;
    public bool IsGrounded => isGrounded;

    // New for crouching adjustment
    private float originalHeight;
    private Vector3 originalCenter;
    public float crouchHeight = 1f;
    public Vector3 crouchCenter = new Vector3(0, 0.5f, 0);

    void Start()
    {
        originalHeight = controller.height;
        originalCenter = controller.center;
    }

    void Update()
    {
        // Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        move = transform.right * x + transform.forward * z;

        // Ground check
        isGrounded = controller.isGrounded;
        animator.SetBool("isGrounded", isGrounded);

        // Falling check
        if (!isGrounded && velocity.y < 0)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        // Landing logic
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isJumping = false;
            animator.SetBool("isJumping", false);
        }

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (isCrouching)
            {
                isCrouching = false;
                animator.SetBool("isCrouching", false);
                controller.height = originalHeight;
                controller.center = originalCenter;
            }

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
            animator.SetBool("isJumping", true);
        }

        // Sprint logic
        isSprinting = Input.GetKey(KeyCode.LeftShift) && z > 0 && !isCrouching;
        animator.SetBool("isSprinting", isSprinting);

        // Crouch toggle
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
            animator.SetBool("isCrouching", isCrouching);

            if (isCrouching)
            {
                controller.height = crouchHeight;
                controller.center = crouchCenter;
            }
            else
            {
                controller.height = originalHeight;
                controller.center = originalCenter;
            }
        }

        // Set speed
        float currentSpeed = isCrouching ? crouchSpeed : isSprinting ? sprintSpeed : speed;

        // Pass movement to Animator
        animator.SetFloat("Horizontal", x);
        animator.SetFloat("Vertical", z);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply movement
        controller.Move((move * currentSpeed + velocity) * Time.deltaTime);
    }
}
