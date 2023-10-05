using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower: MonoBehaviour {

    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _firePoint;

    private Transform target;

    private float range = 15f;
    private float turnSpeed = 10f;
    private string enemieTag = "Enemy";
    private GameObject[] enemies;
    private float shortestDistance = 0;
    private GameObject nearestEnemy = null;
    private float distanceToEnemy = 0f;
    private Vector3 direction;
    private Quaternion lookRotation;
    private Vector3 rotation;
    private float nextFireTime = 0.0f;
    public float fireRate = 1.0f;

    // Start is called before the first frame update
    public void Start() {
        //this is to call UpdateTarget in x times every sec
        InvokeRepeating( "UpdateTarget", 0f, 0.5f );

    }

    // Update is called once per frame
    public void Update() {
        //if there is no target, dont do nothing
        if ( target == null ) {
            return;
        }

        if ( target != null ) {

            RotateTower();
            if ( Time.time > nextFireTime ) {
                FireProjectile();
                nextFireTime = Time.time + 1.0f / fireRate;
            }
        }
    }

    // Shoot tower projectile
    public void FireProjectile() {
        ProjectileController projectileController = Instantiate( _projectile, _firePoint.position, _firePoint.rotation ).GetComponent<ProjectileController>();
        projectileController.Move( (target.position - transform.position).normalized ); // target - position atual do obj dá-nos a direção
        // com "projectController" podemos fazer alterações às settings da bullet se quisermos (ex: speed, damage...)
    }

    // Show the range of the tower
    public void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere( transform.position, range );
    }

    // Verify closest enemie, this can be on update, but will take more resources
    public void UpdateTarget() {

        //array to catch all enemies
        enemies = GameObject.FindGameObjectsWithTag( enemieTag );

        shortestDistance = Mathf.Infinity;
        nearestEnemy = null;
        //loop trough all enemies
        foreach ( GameObject enemy in enemies ) {
            //Get the distance between this object and the enemie object
            distanceToEnemy = Vector3.Distance( transform.position, enemy.transform.position );

            //if enemie is closest that the last read distance
            if ( distanceToEnemy < shortestDistance ) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        //add the closest enemie as target
        if ( nearestEnemy != null && shortestDistance <= range ) {
            target = nearestEnemy.transform;
        } else {
            target = null;
        }
    }

    // rotate Head of Tower
    public void RotateTower() {

        // get where to rotate
        direction = target.position - transform.position;
        // how to turn
        lookRotation = Quaternion.LookRotation( direction );
        // Convert to Euriol Angels, x y z
        // Lerp was using to smooth the animation
        rotation = Quaternion.Lerp( transform.rotation, lookRotation, Time.deltaTime * turnSpeed ).eulerAngles;
        // Vector3 rotation = lookRotation.eulerAngles; 

        transform.rotation = Quaternion.Euler( 0f, rotation.y, 0f );
    }
}