using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public class SpawnTower : MonoBehaviour
{

    [SerializeField] private GameObject tower_preview_prefab;

    private GameObject instantiatePreviewTower;







    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            // criar instancia e meter o codigo todo para lá
            if(instantiatePreviewTower == null){
            instantiatePreviewTower = Instantiate(tower_preview_prefab);
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Destroy(instantiatePreviewTower);
        }
    }
}
