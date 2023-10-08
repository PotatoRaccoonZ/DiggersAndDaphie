using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
Bugs:
    A torre preview quando é criada, muitas vezes não fica logo a seguir o rato, fica numa direção diferente
    
    Para o bug desaparecer é só preciso haver movimento do rato
**/
public class TowerPreview : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject tower;
    private bool ground = false;
    private bool arenaFloor = false;
    private bool otherObj = false;
    private bool notInside = false;
    private float distance;
    Plane plane = new Plane(Vector3.up, 0);
    private Vector3 mousePosition;
    private RaycastHit hit;
    private GameObject greenFloor;
    private GameObject redFloor;




    // Start is called before the first frame update
    void Start()
    {
        greenFloor = GameObject.Find("TurretFloor_Green");
        redFloor = GameObject.Find("TurretFloor_Red");
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            mousePosition = ray.GetPoint(distance);
        }


        Cursor.lockState = CursorLockMode.Confined;
        if (Physics.Raycast(ray, out hit, 1000, ~layer))
        {   
            //check if mouse is colliding with "Plane", Arena Plane object
            if (hit.collider.gameObject.name == "Plane" && notInside)
            {

                //change floor color, ative desativate
                if(redFloor.activeSelf){
                    redFloor.SetActive(false);
                }
                if(!greenFloor.activeSelf){
                    greenFloor.SetActive(true);
                }
                transform.position = mousePosition;
                transform.rotation = transform.rotation;
            }
            else
            {
                //change floor color, ative desativate
                if(greenFloor.activeSelf){
                    greenFloor.SetActive(false);
                }
                if(!redFloor.activeSelf){
                    redFloor.SetActive(true);
                }
                transform.position = mousePosition;
                transform.rotation = transform.rotation;
            }
        }

        if (Input.GetMouseButtonDown(0) && notInside)
        { 
            Instantiate(tower, mousePosition, transform.rotation);

        }

    }

    //Every time the tower preview is touching other objects
    private void OnTriggerStay(Collider other)
    {
        //Only put Ground, because box collider is touching it, its more for bugs, dont need ground if resize box collider
        if (other.CompareTag("Ground"))
        {
            ground = true;
        }
        if (other.CompareTag("ArenaFloor"))
        {
            arenaFloor = true;
        }
        if (!other.CompareTag("Ground") && !other.CompareTag("ArenaFloor"))
        {
            otherObj = true;
        }

        //if is only touching arena floor and ground and not other object
        //it for the tower not spawn inside other objects
        if ((arenaFloor && ground) && !otherObj)
        {
            notInside = true;
        }
        else
        {
            notInside = false;
        }
    }
    //when leave the objects that was touching
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            ground = false;
        }
        if (other.CompareTag("ArenaFloor"))
        {
            arenaFloor = false;
        }

        //if the object is neither ground or arena floor then it's a third object that is leaving
        if (!other.CompareTag("Ground") && !other.CompareTag("ArenaFloor"))
        {
            otherObj = false;
        }
    }

    }

