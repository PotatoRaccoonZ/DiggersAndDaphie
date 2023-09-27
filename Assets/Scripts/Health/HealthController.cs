using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController: MonoBehaviour {
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 10f;
    private HealthBar healthBar;

    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void Heal( float health ) {
        currentHealth += health;
        healthBar.UpdateHealthBar( currentHealth, maxHealth );

        if ( currentHealth > maxHealth ) {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage( float damage ) {
        currentHealth -= damage;
        healthBar.UpdateHealthBar( currentHealth, maxHealth );

        if ( currentHealth <= 0 ) {
            Die();
        }
    }

    public void Die() {
        /**
         * TODO
         * - enemy on death behaviour -> currently destroying object (add animations in the future
         * - player on death behaviour
         */
        if ( CompareTag( "Enemy" ) ) {
            EnemyController enemyController = GetComponent<EnemyController>();
            enemyController.Die();
            Destroy( gameObject );
        }
        Debug.Log( "DEAD" );
    }

    public float getCurrentHealth() {
        return currentHealth;
    }
}
