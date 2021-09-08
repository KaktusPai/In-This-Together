using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookTop : MonoBehaviour
{
    public float cameraSensitivity = 100f;

    public Transform cameraBody;
    public Transform bottomPlayer;

    float xRotation = 0f;

    public Transform firePoint;
    public GameObject bulletPrefab;
    GameObject currentBullet;
    public float bulletForce = 1000f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        transform.position = new Vector3(bottomPlayer.position.x, transform.position.y, bottomPlayer.position.z);

        float cameraX = Input.GetAxis("RightHorizontal") * cameraSensitivity * Time.deltaTime;
        float cameraY = Input.GetAxis("RightVertical") * cameraSensitivity * Time.deltaTime;

        xRotation -= cameraY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        cameraBody.Rotate(Vector3.up * cameraX);

        bool axisInUse = false;
        if (Input.GetAxis("RightTrigger") == 1)
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
