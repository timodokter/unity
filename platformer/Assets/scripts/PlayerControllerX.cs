using System;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{

    public Rigidbody playerRb;
    private float _rotationSpeed;
    private float _walkingSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        _rotationSpeed = 1.5f;
        _walkingSpeed = 5;

    }

    // Update is called once per frame
    void Update()
    {
        //camera movement
        if (Input.GetAxis("Mouse X") > 0)
        {
            transform.Rotate((Vector3.up) * _rotationSpeed);
        } else if (Input.GetAxis("Mouse X") < 0)
        {
            transform.Rotate((Vector3.up) * -_rotationSpeed);
        }
        
        //player movement
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * _walkingSpeed * Time.deltaTime);
        }        
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * _walkingSpeed * Time.deltaTime);
        }
        if (Input.GetKey (KeyCode.A)) {
            transform.Translate (Vector3.left * _walkingSpeed * Time.deltaTime); 
        }
        if(Input.GetKey (KeyCode.D)) {
            transform.Translate (Vector3. right * _walkingSpeed * Time.deltaTime);
        }
    }
}
