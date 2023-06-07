using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    private Collider swordCollider;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<Collider>();
        // currently we only activate the collider when the player presses to attack
        // will eventually have to find a better way to do this most likely
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<HealthController>().TakeDamage(damage);
            Debug.Log("Enemy Health: " + other.GetComponentInParent<HealthController>().getCurrentHealth());
            // I will eventually find another and better way to do this thing below
            // this is just to prevent multiple hits in a single swing
        }
        swordCollider.enabled = false;
    }
}
