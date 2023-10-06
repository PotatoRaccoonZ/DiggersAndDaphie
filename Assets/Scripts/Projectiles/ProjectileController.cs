using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectileController: MonoBehaviour {
    [SerializeField] private float _speed = 25.0f;
    private Rigidbody _rb;
    private Transform target;

    public void Seek(Transform _target){
        target = _target;
    }
    // Start is called before the first frame update
    void Start() {
    }

    void Update(){
        if(target ==null){
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = _speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame){
            Destroy(gameObject);
            Debug.Log("Hit Target!");
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
}
