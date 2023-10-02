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
    private Animator animator;
    private int moveXAnimatorID;
    private int moveZAnimatorID;
    private PlayerAttackController attackController;
    private void Start()
    {
        attackController = GetComponent<PlayerAttackController>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        moveXAnimatorID = Animator.StringToHash("MoveX");
        moveZAnimatorID = Animator.StringToHash("MoveZ");
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
        Vector3 movement = transform.forward * verticalInput + transform.right * horizontalInput;
        movement.Normalize(); // Normalize to ensure consistent speed in all directions
        movement *= currentSpeed;

        ySpeed += Physics.gravity.y * Time.deltaTime;
        movement.y = ySpeed;

        characterController.Move(movement * Time.deltaTime);

        Animate(horizontalInput, verticalInput);

        if (characterController.isGrounded)
        {
            ySpeed = 0f;
            isJumping = false;
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

    public void Animate(float horizontalInput, float verticalInput)
    {
        // Move animations
        animator.SetFloat(moveXAnimatorID, horizontalInput);
        animator.SetFloat(moveZAnimatorID, verticalInput);
    }
}