using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    Vector3 move;
    Vector3 velocity;

    public float gravity = -9.81f;
    public float speed = 5f;
    public CharacterController controller;
    public Animator animator; // Assign this in the Inspector if it's not on the same GameObject

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        // Set animation state using "IsWalking" (capital I)
        bool IsWalking = move.magnitude > 0.1f;
        animator.SetBool("IsWalking", IsWalking);
        Debug.Log("IsWalking: " + IsWalking);

        // Apply gravity
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -2f;
        }

        controller.Move((move * speed + velocity) * Time.deltaTime);
    }
}
