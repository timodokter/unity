using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float SpeedH = 4;
    public float SpeedV = 4;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationY += SpeedV * Input.GetAxis("Mouse X");
        rotationX -= SpeedH * Input.GetAxis("Mouse Y");

        rotationX = Mathf.Clamp(rotationX, -30f, 30f);
        
        transform.eulerAngles = new Vector3(0, rotationY, rotationX);
    }
}
