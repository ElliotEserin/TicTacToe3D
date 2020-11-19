using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform cam;
    public float rotationAmount = 45;
    public float rotationSpeed = 1;
    public float cameraDistance = -15f;
    public float scollSensitivity = 1f;
    public float CameraDistance
    {
        get
        {
            return cameraDistance;
        }
        set
        {
            cameraDistance = value;
            cam.localPosition = new Vector3(0, 0, cameraDistance);
        }
    }

    public Vector3 targetRotation = new Vector3();

    private void Start()
    {
        cam = Camera.main.transform;
        cam.localPosition = new Vector3(0, 0, cameraDistance);
        cam.LookAt(transform);
    }

    private void Update()
    {
        //rotation of camera
        if (Input.GetKeyDown(KeyCode.A))
            targetRotation.y += rotationAmount;
        if (Input.GetKeyDown(KeyCode.D))
            targetRotation.y -= rotationAmount;

        if (Input.GetKeyDown(KeyCode.W))
            targetRotation.x += rotationAmount;
        if (Input.GetKeyDown(KeyCode.S))
            targetRotation.x -= rotationAmount;

        targetRotation.x = Mathf.Clamp(targetRotation.x, 0, 90);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), rotationSpeed * Time.deltaTime);

        //distance of camera
        var amount = Input.GetAxis("Mouse ScrollWheel");
        CameraDistance += amount * scollSensitivity;
        CameraDistance = Mathf.Clamp(cameraDistance, -20, -10);
    }
}
