using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    
    private float projectileSpeed = 25.0f;
    [SerializeField] Transform headPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  

    }

    public void FireProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, headPoint.position, Quaternion.identity);
        // Add force to the projectile (if using Rigidbody)
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * projectileSpeed;
        }
}
}
