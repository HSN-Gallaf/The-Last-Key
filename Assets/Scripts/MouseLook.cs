using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 150f;
    public Transform playerBody;

    public float standHeight = 1.6f;
    public float crouchHeight = 1.0f;
    public float cameraLerpSpeed = 5f;

    private float xRotation = 0f;
    private bool isCrouching = false;
    private PlayerMovment playerMovement;

    private float targetLocalY;  // Desired local camera height

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement = playerBody.GetComponent<PlayerMovment>();

        // Start camera at standing height
        targetLocalY = standHeight;
        Vector3 localPos = transform.localPosition;
        localPos.y = targetLocalY;
        transform.localPosition = localPos;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (playerMovement != null)
        {
            isCrouching = playerMovement.IsCrouching;
        }

        // Set target local Y based on crouching
        targetLocalY = isCrouching ? crouchHeight : standHeight;

        // Smoothly interpolate camera height
        Vector3 currentLocalPos = transform.localPosition;
        currentLocalPos.y = Mathf.Lerp(currentLocalPos.y, targetLocalY, Time.deltaTime * cameraLerpSpeed);
        transform.localPosition = currentLocalPos;
    }
}
