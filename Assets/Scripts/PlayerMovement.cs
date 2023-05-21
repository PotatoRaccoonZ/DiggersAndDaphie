using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    private float ySpeed = 0f;
    private float currentSpeed = 0f;
    private bool isJumping = false;
    private bool isSprinting = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentSpeed = movementSpeed;
    }

    /**
     * Move the player
     * 
     * @param horizontalInput - Horizontal input
     * @param verticalInput - Vertical input
     */
    public void Move(float horizontalInput, float verticalInput)
    {
        Debug.Log(isSprinting);
        Vector3 movement = transform.forward * verticalInput + transform.right * horizontalInput;
        movement.Normalize(); // Normalize to ensure consistent speed in all directions
        movement *= currentSpeed;

        if (characterController.isGrounded)
        {
            ySpeed = 0f;
            isJumping = false;
        }

        ySpeed += Physics.gravity.y * Time.deltaTime;
        movement.y = ySpeed;

        // TODO - Add possibility to run (accelerate speed) by pressing SHIFT or something

        if (movement.Equals(Vector3.zero))
        {
            // play idle animation
        }
        else
        {
            characterController.Move(movement * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (characterController.isGrounded && !isJumping)
        {
            ySpeed = jumpHeight;
            isJumping = true;
        }
    }

    public void Rotate(float targetAngle)
    {
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }

    public void Sprint(bool start, bool stop)
    {
        if (start && !isSprinting)
        {
            currentSpeed = movementSpeed * 2;
            isSprinting = true;
        }
        else if (stop)
        {
            currentSpeed = movementSpeed;
            isSprinting = false;
        }
    }
}