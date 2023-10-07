using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ProjectileController: MonoBehaviour {
    [SerializeField] private float _speed = 25.0f;
    [SerializeField] private float damage = 1.0f;
    [SerializeField] private Collider bulletCollider;
    private Rigidbody _rb;
    private Transform target;

    public void Seek(Transform _target){
        target = _target;
    }
    // Start is called before the first frame update
    void Start() {
        bulletCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter( Collider other ) {
        if ( other.CompareTag( "Enemy" ) ) {
            other.GetComponentInParent<EnemyController>().TakeDamage( damage );
            Debug.Log( "Enemy Health: " + other.GetComponentInParent<HealthController>().getCurrentHealth() );
            Destroy(gameObject);
            // I will eventually find another and better way to do this thing below (if necessary)
            // this is just to prevent multiple hits in a single swing
        }
    }

    void Update(){
        if(target ==null){
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = _speed * Time.deltaTime;

        /**if(dir.magnitude <= distanceThisFrame){
            Destroy(gameObject);
            Debug.Log("Hit Target!");
            return;
        }**/
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }


}
