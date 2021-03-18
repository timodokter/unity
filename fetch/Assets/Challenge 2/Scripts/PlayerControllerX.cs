using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;

    public float dogSpawnInterval = 0.0f;
    
    // Update is called once per frame
    void Update()
    {

        dogSpawnInterval++;
        
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && dogSpawnInterval > 200f)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            dogSpawnInterval = 0.0f;
        }
    }
}
