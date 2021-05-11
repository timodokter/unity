using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    private GamemanagerX GamemanagerX;
    
    //variables of the camera
    public float speedH = 4;
    public float speedV = 4;

    private float rotationX;
    private float rotationY;
    
    //variables for the gun
    public Transform BulletSpawnPosition;
    
    public GameObject BulletPrefab;

    private float BulletVelocity = 5; 
    private float fireTimer;
    private float xRotationOfBullet, yRotationOfBullet, zRotationOfBullet, wRotationOfBullet;

    private bool shot;

    // Start is called before the first frame update
    void Start()
    {
        GamemanagerX = GameObject.Find("GameManager").GetComponent<GamemanagerX>();
        shot = false;
    }

    // Update is called once per frame
    void Update()
    {
        //camera movement
        rotationY += speedV * Input.GetAxis("Mouse X");
        rotationX -= speedH * Input.GetAxis("Mouse Y");

        rotationX = Mathf.Clamp(rotationX, -30f, 30f);
        
        transform.eulerAngles = new Vector3(0, rotationY, rotationX);

        if (shot)
        {
            fireTimer += Time.deltaTime;
        }
        if (fireTimer > 2f)
        {
            shot = false;
        }
        if (!shot)
        {
            ShootGun();
        }
        // Debug.Log(shot);
        // Debug.Log(Mathf.Round(fireTimer));
        // Debug.Log(xRotationOfBullet);
        // Debug.Log(yRotationOfBullet);
        // Debug.Log(zRotationOfBullet);
    }

    private void ShootGun()
    {
        if (GamemanagerX.isGameActive)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Instantiate(BulletPrefab, BulletSpawnPosition.position, BulletSpawnPosition.rotation);
                shot = true;
                fireTimer = 0;
                Debug.Log("left mouse button is pressed");
            }
        }
    }
}