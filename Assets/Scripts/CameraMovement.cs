using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;  // The player object
    public GameObject cam;  // The camera object
    public float smoothSpeed = 5f;  // Adjusts smoothness of camera follow
    public Vector3 offset = new Vector3(0, 2, -5);  // Camera offset from the target

    public float standHeight = 1.6f;  // Camera height when standing
    public float crouchHeight = 1.0f;  // Camera height when crouching
    public float cameraLerpSpeed = 5f;  // Smooth speed of the camera height change

    private float xInput, yInput;
    private bool isCrouching = false;  // Track if crouching

    void LateUpdate()
    {
        // Check if the target has a PlayerMovment script and get the crouch state
        PlayerMovment playerMovement = target.GetComponent<PlayerMovment>();
        if (playerMovement != null)
        {
            isCrouching = playerMovement.IsCrouching;  // Get crouch state from PlayerMovment
        }

        // Adjust the camera's Y position based on crouching state
        float targetHeight = isCrouching ? crouchHeight : standHeight;

        // Update the camera's position smoothly
        Vector3 targetPosition = target.transform.position + offset;

        // Smoothly adjust the Y position of the camera to simulate crouch/stand transition
        targetPosition.y = Mathf.Lerp(transform.position.y, targetHeight, Time.deltaTime * cameraLerpSpeed);

        // Apply the new position to the camera
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Rotate the camera based on mouse input
        xInput = Input.GetAxis("Mouse X");
        yInput = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(-yInput, xInput, 0));
        cam.transform.LookAt(target.transform);
    }
}
