using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimationScript : MonoBehaviour
{
    
    public bool isAnimated;
    public bool isRotating = false;

    public Vector3 rotationAngle;
    public float rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimated)
        {
            if (isRotating)
            {
                transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
            }
        }
    }
}
