using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public bool stillMode = false;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded;

    float x;
    float z;

    //health & gameplay
    public static int health = 5;
    public Slider hpSlider;
    public Text finalText;
    public int finalDistance;

    void Start()
    {
        finalText.gameObject.SetActive(false);
    }

    void Update()
    {
        hpSlider.value = health;
        hpSlider.maxValue = 5;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Vector3 move = transform.right * x + transform.forward * z;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            stillMode = !stillMode;
            Debug.Log("Fire2 stillmode");
        }

        if (stillMode == false)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }  else
        {
            move = Vector3.zero;
        }

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //final

        if (health <= 0)
        {
            finalText.gameObject.SetActive(true);
            finalText.text = "YOU LOST";
        }

        if (transform.position.z > finalDistance)
        {
            finalText.gameObject.SetActive(true);
            finalText.text = "YOU WIN";
        }
    }
}
