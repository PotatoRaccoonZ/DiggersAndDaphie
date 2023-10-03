using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // temp (might convert this to an array when we get to have more enemies)
    [SerializeField] private float mouseSensitivity = 2f;
    private PlayerMovement playerMovement;
    private PlayerAttackController attackController;
    private float rotationAngle = 0f;
    private Animator animator;


    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        attackController = GetComponent<PlayerAttackController>();
    }

    private void Update()
    {
        if (!attackController.isAttacking && Input.GetMouseButtonDown(0))
        {
            attackController.Attack();
        }
        Moving();
        
        // Handle other player actions and behaviors
        if (Input.GetButton("Jump"))
        {
            playerMovement.Jump();
        }



        

        /** 
         * TODO: 
         * - create an health controller and control it from here aswell 
         * - this health controller should be made in a way that player and npcs/enemies are able to use it
         */

        ////
        // DEV TOOLS
        //
        // this is just for testing and stuff
        ////

        // Respawn Enemy
        if (Input.GetKeyDown(KeyCode.T))
        {
            // spawn the enemy in a random place 
            RespawnEnemy();
        }

        // Reposition player to the center
        if (Input.GetKeyDown(KeyCode.R))
        {
            RepositionPlayer();
        }
    }


    public void Moving(){
        // Handle player input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotate the player with the mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationAngle += mouseX;
        playerMovement.Rotate(rotationAngle);

        playerMovement.Move(horizontalInput, verticalInput);

        // Check if should sprint or not
        playerMovement.Sprint(Input.GetKeyDown(KeyCode.LeftShift), Input.GetKeyUp(KeyCode.LeftShift));
        
    }
    private void RespawnEnemy()
    {
        float y = 5;
        float min = -20;
        float max = 20;
        Instantiate(enemyPrefab, new Vector3(Random.Range(min, max), y, Random.Range(min, max)), Quaternion.identity);
    }

    private void RepositionPlayer()
    {
        transform.position = Vector3.zero;
    }
}