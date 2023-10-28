using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float sensitivity = 2.0f; // Mouse sensitivity
    public bool lockCursor = true;  // Lock the mouse cursor

    private float rotationX = 0;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        // Rotate the player based on mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
       // float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        //rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);

        transform.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Unlock the cursor on Escape key press
        if (lockCursor && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
