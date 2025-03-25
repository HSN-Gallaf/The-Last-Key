using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float rotationSpeed = 10f; // Added rotation speed

    private Vector2 moveVector;
    private CharacterController characterController;
    private Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();

        // Update animation state
        animator.SetBool("IsWalking", moveVector.magnitude > 0);
    }

    private void Move()
    {
        // Convert moveVector (2D) to a 3D movement direction
        Vector3 moveDirection = new Vector3(moveVector.x, 0f, moveVector.y);

        if (moveDirection.magnitude > 0.1f) // Only rotate if there is movement
        {
            // Rotate towards movement direction smoothly
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move player
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
