using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    private GamemanagerX GamemanagerX;

    public Transform playerTransform;

    //variables of the camera
    public float speedH = 4;
    public float speedV = 4;

    private float rotationZ;
    private float rotationY;

    //variables for the gun
    public Transform BulletSpawnPosition;

    public GameObject BulletPrefab;

    public float currAmmo = 30;
    public float extraAmmo = 30;
    public float totalBulletsShot;
    public float bulletsShot;
    private float BulletVelocity = 10f;
    private float fireTimer;
    private float reloadSpeed = 1f;
    
    [Range(0,2)]
    public float MaxRecoilXAxis;
    [Range(0,2)]
    public float MaxRecoilYAxis;
    
    private Vector2 recoilRemaining;
    
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
        weaponAnimator.SetFloat("ReloadSpeed", reloadSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (GamemanagerX.isGameActive)
        {
            //camera movement
            rotationY += speedV * Input.GetAxis("Mouse X");
            rotationZ -= speedH * Input.GetAxis("Mouse Y");

            rotationZ = Mathf.Clamp(rotationZ, -30f, 30f);

            playerTransform.transform.eulerAngles = new Vector3(0, rotationY, rotationZ);

            if (shot)
            {
                fireTimer += Time.deltaTime;
            }

            if (fireTimer > 0.2f)
            {
                shot = false;
            }

            if (!shot)
            {
                ShootGun();
            }

            //reload animation
            if (Input.GetKeyDown(KeyCode.R) && !reloading)
            {
                if (currAmmo == 30)
                {
                    return;
                }

                if (extraAmmo > 0)
                {
                    StartCoroutine(Reload());
                    return;
                }
            }

            if (extraAmmo <= 0)
            {
                extraAmmo = 0;
            }

            //this function is in a different file and is used to show the ammo count in your gun and the extra bullets you have left
            GamemanagerX.UpdateAmmoCount();

            //Recoil is placed to make it smooth
            // Recoil on the x axis
            playerTransform.transform.Rotate(Vector3.forward, recoilRemaining.x);
            //recoil on the Y axis
            playerTransform.transform.Rotate(Vector3.up, recoilRemaining.y, Space.Self);
        }
    }

    //function for shooting the gun
    private void ShootGun()
    {
        if (GamemanagerX.isGameActive)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !reloading && currAmmo > 0)
            {
                GameObject instantiatedBullet = Instantiate(BulletPrefab, BulletSpawnPosition.position, BulletSpawnPosition.rotation);

                instantiatedBullet.GetComponent<Rigidbody>().velocity =
                    transform.TransformDirection(new Vector3(-BulletVelocity, 0, 0));
                gunshotSound.Play();

                shot = true;
                fireTimer = 0;
                currAmmo--;
                bulletsShot++;
                totalBulletsShot++;

                //-------------------------------  recoil  -------------------------------\\
                recoilRemaining.x = Random.Range(-MaxRecoilXAxis, MaxRecoilXAxis);
                recoilRemaining.y = Random.Range(0, -MaxRecoilYAxis);
                
                //-------------------------------  recoil  -------------------------------\\
            }
        }
    }

    //function for reloading the gun
    IEnumerator Reload()
    {
        reloading = true;
        weaponAnimator.SetBool("reloading", true);
        
        yield return new WaitForSeconds(reloadSpeed);

        if (currAmmo + extraAmmo >= 30)
        {
            currAmmo = 30;
        }
        else
        {
            currAmmo += extraAmmo;
        }
        extraAmmo -= bulletsShot;
        bulletsShot = 0;
        
        weaponAnimator.SetBool("reloading", false);
        reloading = false;
    }
}