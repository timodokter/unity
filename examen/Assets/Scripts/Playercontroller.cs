using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // private GamemanagerX GamemanagerX;
    
    //variables of the camera
    public float SpeedH = 4;
    public float SpeedV = 4;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        // GamemanagerX = GameObject.Find("GameManager").GetComponent<GamemanagerX>();
    }

    // Update is called once per frame
    void Update()
    {
        //camera movement
        rotationY += SpeedV * Input.GetAxis("Mouse X");
        rotationX -= SpeedH * Input.GetAxis("Mouse Y");

        rotationX = Mathf.Clamp(rotationX, -30f, 30f);
        
        transform.eulerAngles = new Vector3(0, rotationY, rotationX);
        shootGun();
    }

    private void shootGun()
    {
        // while (GamemanagerX.isGameActive)
        // {
            if (Input.GetKey(KeyCode.Mouse0))
            {

                Debug.Log("left mouse button is pressed");
            }
        // }
    }
}