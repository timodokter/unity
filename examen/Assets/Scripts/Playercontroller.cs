using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float SpeedH = 2;
    public float SpeedV = 2;

    private float Horizontal = 0.0f;
    private float Vertical = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal += SpeedH * Input.GetAxis("Mouse X");
        Vertical -= SpeedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(Vertical, Horizontal, 0.0f);
    }
}
