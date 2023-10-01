using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController: MonoBehaviour {

    [SerializeField] private float _speed = 3;
    [SerializeField] private bool _frozen = false; // used for dummy enemies at the moment

    private HealthController _healthController;
    private Transform _target;
    private Animator _animator;

    // Start is called before the first frame update
    void Start() {
        _target = GameObject.Find( "Player" ).transform;
        _healthController = GetComponent<HealthController>();
        _animator = transform.Find( "Body" ).GetComponent<Animator>();
    }
    void Awake() {
        if ( !_target ) {
            _target = GameObject.Find( "Player" ).transform;
        }
        if ( !_healthController ) {
            _healthController = GetComponent<HealthController>();
        }
    }

    // Update is called once per frame
    void Update() {
        if ( _frozen ) {
            return;
        }

        if ( _target ) {
            transform.position = Vector3.MoveTowards( transform.position, _target.position, _speed * Time.deltaTime );
            transform.LookAt( new Vector3( _target.position.x, transform.position.y, _target.position.z ) );
            _animator.SetBool( "Walk Forward", true );
            //_moveDir = (_target.position - transform.position).normalized;
        }
    }

    public void TakeDamage( float damage ) {
        _healthController.TakeDamage( damage );
        _animator.SetTrigger( "Take Damage" );
    }

    public void Die() {
        _animator.SetBool( "Walk Forward", false );
        _animator.SetTrigger( "Die" );
    }
}
