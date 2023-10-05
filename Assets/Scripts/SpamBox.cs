using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamBox : MonoBehaviour
{
    public GameObject boxPrefab;
    public Transform spawnPoint;
    public float spawnDistance = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    // Check if the "1" key is pressed
    if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Calculate the position in front of the character
            Vector3 spawnPosition = spawnPoint.position + spawnPoint.forward * spawnDistance;

            // Spawn the box prefab at the calculated position
            Instantiate(boxPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

