using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float cameraSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    public BottomPlayerMovement bpm;
    float cameraX;
    float cameraY;

    public Transform firePoint;
    public GameObject bulletPrefab;
    GameObject currentBullet;
    public float bulletForce = 13f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (bpm.stillMode == true)
        {
            cameraX = Input.GetAxis("Horizontal") * cameraSensitivity * Time.deltaTime;
            cameraY = Input.GetAxis("Vertical") * cameraSensitivity * Time.deltaTime;
        } 
        xRotation -= cameraY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * cameraX);
    }

    void FixedUpdate()
    {
        bool axisInUse = false;
        if (Input.GetAxis("LeftTrigger") == 1 && bpm.stillMode == true)
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                Debug.Log("+1");
                currentBullet = (Instantiate(bulletPrefab, firePoint.position, firePoint.localRotation) as GameObject);
                Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * bulletForce);
            }
        }
    }
}
