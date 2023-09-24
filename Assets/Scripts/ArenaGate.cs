using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaGate: MonoBehaviour {

    public float rotationDuration = 2.0f;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private float rotationStartTime;
    private bool isRotating = false;
    [SerializeField] private bool isLeft = false;
    private bool canOpenGate = true;

    public bool CanOpenGate { get; set; }
    public bool IsRotating { get; set; }

    // Start is called before the first frame update
    void Start() {
        initialRotation = transform.rotation;
        targetRotation = initialRotation * Quaternion.Euler( 0, 0, isLeft ? 90 : -90 );
    }

    private void StartRotation( bool open = true ) {
        if ( !isRotating ) {
            StartCoroutine( RotateOverTime( open ) );
        }
    }

    IEnumerator RotateOverTime( bool open ) {
        isRotating = true;
        rotationStartTime = Time.time;

        Quaternion startRotation = open ? initialRotation : targetRotation;
        Quaternion endRotation = open ? targetRotation : initialRotation;
        while ( Time.time - rotationStartTime < rotationDuration ) {
            float progress = (Time.time - rotationStartTime) / rotationDuration;
            transform.rotation = Quaternion.Lerp( startRotation, endRotation, progress );
            yield return null;
        }

        transform.rotation = endRotation; // Ensure it ends at the exact target rotation.
        isRotating = false;
    }

    public void Open() {
        StartRotation();
    }

    public void Close() {
        StartRotation( false );
    }

    private void OnTriggerStay( Collider other ) {
        if ( other.gameObject.CompareTag( "Player" ) ) {
            if ( Input.GetKey( KeyCode.E ) && canOpenGate ) {
                Open();
                canOpenGate = false;
            }
        }
    }
}
