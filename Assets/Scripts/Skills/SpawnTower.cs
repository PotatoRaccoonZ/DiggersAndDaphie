using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    [SerializeField] private GameObject tower;
    [SerializeField] private GameObject tower_green;
    [SerializeField] private GameObject tower_red;
    private GameObject previewTowerGreen;
    private GameObject previewTowerRed;
    
    private Vector3 spawnPoint;
    private float distanceSpawn = 10f;
    private Vector3 mousePosition;
    Plane plane = new Plane(Vector3.up, 0);
    float distance;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        //Estudar melhor esta solução !!! Raycast !!! Pois dá para escolher o objeto em que pode dar spawn!!!!
        //Código so de teste
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance)){
            mousePosition = ray.GetPoint(distance);
        }

        if(Input.GetKey(KeyCode.Alpha1)){
            Cursor.lockState = CursorLockMode.Confined;
            if(previewTowerGreen == null){
                previewTowerGreen = Instantiate(tower_green, mousePosition, transform.rotation);
            }
            previewTowerGreen.transform.position = mousePosition;
            previewTowerGreen.transform.rotation = transform.rotation;
            
            if(Input.GetMouseButtonDown(0)){
                Destroy(previewTowerGreen);
                spawnPoint = transform.position + transform.forward * distanceSpawn;
                Instantiate(tower, mousePosition, transform.rotation);
            }
        }
        
         if (Input.GetKeyUp(KeyCode.Alpha1)){
                Cursor.lockState = CursorLockMode.Locked;
                Destroy(previewTowerGreen);
        }
    }

}
