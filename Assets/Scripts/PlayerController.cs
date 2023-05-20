using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 2f;
    private PlayerMovement playerMovement;
    private float rotationAngle = 0f;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Handle player input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotate the player with the mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationAngle += mouseX;
        playerMovement.Rotate(rotationAngle);

        playerMovement.Move(horizontalInput, verticalInput);

        // Handle other player actions and behaviors
        if (Input.GetButtonDown("Jump"))
        {
            playerMovement.Jump();
        }

        /** 
         * TODO: 
         * - create an health controller and control it from here aswell 
         * - this health controller should be made in a way that player and npcs/enemies are able to use it
         */ 
    }
}