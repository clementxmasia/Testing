using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 30.0f;  // Adjust the rotation speed as needed
    public Vector3 rotationAxis = Vector3.up; // Change the axis of rotation as needed

    void Update()
    {
        // Rotate the object around the specified axis at a constant speed
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
