using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float gravity;
    private Vector3 moveDirection = Vector3.zero;

    //References
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        SetVariables();
    }

    private void SetVariables()
    {
        speed = 6.0f;
        rotationSpeed = 60f;
        jumpSpeed = 5.0f;
        gravity = 10.0f;
    }

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            if (Input.GetKey(KeyCode.LeftShift))
                RotationMovement(3);
            else
            {
                RotationMovement(1);
            }
            //Multiply it by speed.
            if (Input.GetKey(KeyCode.LeftShift))
                ForwardMovement(2);
            else
            {
                ForwardMovement(1);
            }
            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
    }

    void RotationMovement(float roSpeed)
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * (rotationSpeed * roSpeed) * Time.deltaTime, 0);
    }

    void ForwardMovement(float foSpeed)
    {
        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);

        moveDirection *= speed * foSpeed;
    }
}
