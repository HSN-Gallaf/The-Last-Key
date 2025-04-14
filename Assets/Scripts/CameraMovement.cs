using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject target;
    public GameObject cam;
    public float smoothSpeed = 5f; // Adjusts smoothness of camera follow
    public Vector3 offset = new Vector3(0, 2, -5); // Camera offset from the target

    private float xInput, yInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        // Smoothly follow the target
        Vector3 desiredPosition = target.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        cam.transform.LookAt(target.transform);

        xInput = Input.GetAxis("Mouse X");
        yInput = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(yInput, xInput, 0));
    }
}
