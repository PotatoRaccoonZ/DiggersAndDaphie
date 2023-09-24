using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager: MonoBehaviour {
    [SerializeField] private List<Transform> _spawnArea;
    [SerializeField] private List<GameObject> _enemiesList;

    private ArenaGate[] _arenaGates;

    private bool _respawnStarted = false;
    private float _maxRespawnCooldown = 2f;
    private float _respawnCooldown = 0f;

    // Start is called before the first frame update
    void Start() {
        _arenaGates = GetComponentsInChildren<ArenaGate>();
    }

    // Update is called once per frame
    void Update() {
        if ( _respawnStarted && _enemiesList.Count > 0 ) {
            // to make this simple for a start I will respawn 1 enemy every 2 secs? 
            // can eventually update it to an arena of waves and a final boss
            _respawnCooldown -= Time.deltaTime;
            if ( _respawnCooldown <= 0f ) {
                int randomEnemy = Random.Range( 0, _enemiesList.Count );
                Vector3 randomPosition = _getRandomPosition();
                Instantiate( _enemiesList[randomEnemy], randomPosition, Quaternion.identity );
                _respawnCooldown = _maxRespawnCooldown;
            }
        }

        // Stop respawning enemies and 
        if ( Input.GetKeyDown( KeyCode.Q ) ) {
            _respawnStarted = false;
        }
    }

    private Vector3 _getRandomPosition() {
        float minX = float.MaxValue, minZ = float.MaxValue, maxX = float.MinValue, maxZ = float.MinValue;
        foreach ( Transform spawnPoint in _spawnArea ) {
            minX = spawnPoint.position.x < minX ? spawnPoint.position.x : minX;
            minZ = spawnPoint.position.z < minZ ? spawnPoint.position.z : minZ;
            maxX = spawnPoint.position.x > maxX ? spawnPoint.position.x : maxX;
            maxZ = spawnPoint.position.z > maxZ ? spawnPoint.position.z : maxZ;
        }
        return new Vector3( Random.Range( minX, maxX ), 1f, Random.Range( minZ, maxZ ) );
    }

    private void OnTriggerStay( Collider other ) {
        if ( other.gameObject.CompareTag( "Player" ) ) {
            if ( Input.GetKey( KeyCode.E ) && !_respawnStarted ) {
                if ( _respawnStarted && !_arenaGates[0].IsRotating ) {
                    _respawnStarted = false;
                    foreach ( ArenaGate arenaGate in _arenaGates ) {
                        arenaGate.Open();
                    }
                } else if ( !_respawnStarted && !_arenaGates[0].IsRotating ) {
                    _respawnStarted = true;
                    foreach ( ArenaGate arenaGate in _arenaGates ) {
                        arenaGate.Close();
                    }
                }

            }
        }
    }
}
