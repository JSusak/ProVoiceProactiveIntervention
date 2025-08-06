using UnityEngine;

public class SteeringWheelTurn : MonoBehaviour
{
    public float maxSteeringAngle = 270f;
    public float turnSpeed = 1f;

    private float currentRotation = 0f;

    void Update()
    {
        float input = Input.GetAxis("Horizontal"); // -1 (left) to +1 (right)
        float targetAngle = input * maxSteeringAngle;

        // Smooth rotation
        currentRotation = Mathf.Lerp(currentRotation, targetAngle, Time.deltaTime * turnSpeed);

        // Apply rotation (adjust axis if needed)
        transform.localRotation = Quaternion.Euler(currentRotation, 0f, 0f);
    }
}