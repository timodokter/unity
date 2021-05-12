using System.Collections;
using UnityEngine;
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

    public float currAmmo = 30;
    public float extraAmmo = 30;
    public float bulletsShot;
    private float BulletVelocity = 50f;
    private float fireTimer;
    private float reloadSpeed = 1f;

    private bool shot;
    private bool reloading;

    private Animator weaponAnimator;

    public AudioSource gunshotSound;

    // Start is called before the first frame update
    void Start()
    {
        GamemanagerX = GameObject.Find("GameManager").GetComponent<GamemanagerX>();
        shot = false;
        weaponAnimator = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Animator>();
        Debug.Log(BulletVelocity);
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
        if (fireTimer > 1f)
        {
            shot = false;
        }
        if (!shot)
        {
            ShootGun();
        }
        Debug.Log(shot);
        Debug.Log(Mathf.Round(fireTimer));
        
        //reload animation
        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            if (currAmmo == 30)
            {
                return;
            }
            if (extraAmmo >= 0)
            {
                StartCoroutine(Reload());
                return;
            }
        }
        if (currAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        GamemanagerX.UpdateAmmoCount();
    }

    private void ShootGun()
    {
        if (GamemanagerX.isGameActive)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                GameObject instantiatedBullet = Instantiate(BulletPrefab, BulletSpawnPosition.position, BulletSpawnPosition.rotation);

                instantiatedBullet.GetComponent<Rigidbody>().velocity =
                    transform.TransformDirection(new Vector3(-BulletVelocity, 0, 0));
                gunshotSound.Play();
                
                shot = true;
                fireTimer = 0;
                currAmmo--;
                bulletsShot++;
                Debug.Log("a bullet was shot");
            }
        }
    }

    IEnumerator Reload()
    {
        reloading = true;
        weaponAnimator.SetBool("reloading", true);
        
        yield return new WaitForSeconds(reloadSpeed);

        extraAmmo -= bulletsShot;
        bulletsShot = 0;
        currAmmo = 30;
        reloading = false;
        weaponAnimator.SetBool("reloading", false);
    }
}