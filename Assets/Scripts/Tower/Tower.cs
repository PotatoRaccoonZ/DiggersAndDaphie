using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //only to check, change to private later
    public Transform target;

    public float range = 15f;
    public float turnSpeed = 10f;
    public string enemieTag = "Enemy";
    public Transform headOfTower;

    // Start is called before the first frame update
    void Start()
    {  
        //this is to call UpdateTarget in x times every sec
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        //if there is no target, dont do nothing
        if(target == null){
            return;
        }

        RotateTower();
        

    }

    //Show the range of the tower
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    //Verify closest enemie, this can be on update, but will take more resources
    void UpdateTarget(){

        //array to catch all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemieTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        //loop trough all enemies
        foreach(GameObject enemy in enemies)
        {
            //Get the distance between this object and the enemie object
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);    
            
            //if enemie is closest that the last read distance
            if(distanceToEnemy < shortestDistance){
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        //add the closest enemie as target
        if(nearestEnemy != null && shortestDistance <= range){
            target = nearestEnemy.transform; 
        } else{
            target = null;
        }
    }

    //rotate Head of Tower
    void RotateTower(){
        
        //get where to rotate
        Vector3 dir = target.position - transform.position;
        //how to turn
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Convert to Euriol Angels, x y z
        //Lerp was using to smooth the animation
        Vector3 rotation = Quaternion.Lerp(headOfTower.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; 
        //Vector3 rotation = lookRotation.eulerAngles; 

        headOfTower.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
