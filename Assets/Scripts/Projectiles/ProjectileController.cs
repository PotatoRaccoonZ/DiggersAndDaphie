using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController: MonoBehaviour {
    [SerializeField] private float _speed = 25.0f;
    private Rigidbody _rb;


    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Awake() {
        if ( _rb == null ) {
            _rb = GetComponent<Rigidbody>();
        }
    }

    /**
     * Move the projectile.
     */
    public void Move( Vector3 direction ) {
        _rb.AddForce( direction * _speed, ForceMode.Impulse ); // isto é que é addForce :P
        //_rb.velocity = transform.forward * _speed;
    }
}
