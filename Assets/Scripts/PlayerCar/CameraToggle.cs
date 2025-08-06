using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    //References to exterior and interior cameras.
    public Camera exteriorCamera;
    public Camera firstPersonCamera;

    //References to exterior and interior car body.
    public GameObject exteriorCar;
    public GameObject interiorCar;

    private bool isFirstPerson = true;

    void Start()
    {
        SetCameraView(isFirstPerson);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 2"))
        {
            isFirstPerson = !isFirstPerson;
            SetCameraView(isFirstPerson);
        }
    }
    void SetCameraView(bool firstPerson)
    {
        firstPersonCamera.gameObject.SetActive(firstPerson);
        exteriorCamera.gameObject.SetActive(!firstPerson);
    }
}