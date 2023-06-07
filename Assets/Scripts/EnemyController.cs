using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private HealthController healthController;

    // Start is called before the first frame update
    void Start()
    {
        healthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        healthController.TakeDamage(damage);
    }
}
