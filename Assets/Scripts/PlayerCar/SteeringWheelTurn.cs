using UnityEngine;

public class SteeringWheelTurn : MonoBehaviour
{
    public float maxSteeringAngle = 270f;
    public float turnSpeed = 1f;
    private float currentRotation = 0f;

    void Update()
    {
        float input = Input.GetAxis("Horizontal"); 
        float targetAngle = input * maxSteeringAngle;

        currentRotation = Mathf.Lerp(currentRotation, targetAngle, Time.deltaTime * turnSpeed);
        transform.localRotation = Quaternion.Euler(currentRotation, 0f, 0f);
    }
}