using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 10f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Heal(float health)
    {
        currentHealth += health;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        /**
         * TODO
         * - enemy on death behaviour -> currently destroying object (add animations in the future
         * - player on death behaviour
         */
        if (!gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        Debug.Log("DEAD");
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }
}
