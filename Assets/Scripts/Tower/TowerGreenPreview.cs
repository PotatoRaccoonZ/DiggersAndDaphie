using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TowerGreenPreview : MonoBehaviour
{  
    private bool ground = false;
    private bool arenaFloor = false;
    private bool otherObj = false;
    private bool notInside = false;
    public bool teste = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }

     private void OnTriggerStay(Collider other){
        //Only put Ground, because box collider is touching it, its more for bugs, dont need ground if resize box collider
        if(other.CompareTag("Ground")){
            ground = true;
        }
        if(other.CompareTag("ArenaFloor")){
            arenaFloor = true;
        } 
        if (!other.CompareTag("Ground") && !other.CompareTag("ArenaFloor"))
        {
            otherObj = true;
        }
        if((arenaFloor && ground) && !otherObj){
            notInside = true;
            Debug.Log("Sem objetos");
        } else{
            notInside = false;
        }
    }
    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Ground")){
            ground = false;
        }
        if(other.CompareTag("ArenaFloor")){
            arenaFloor = false;
        } 

        if (!other.CompareTag("Ground") && !other.CompareTag("ArenaFloor"))
        {
            otherObj = false;
        }
    }

    public bool NotInsideOthers(){
        return notInside;
    }
}
