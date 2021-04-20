using System;
using System.ComponentModel.Design;
using System.Timers;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControllerX : MonoBehaviour
{
    private GamemanagerX GamemanagerX;

    public Rigidbody playerRb;
    private float _rotationSpeed;
    private float _walkingSpeed;

    public LayerMask groundLayers;
    public float jumpForce = 7;
    public GameObject pBody;
    private GameObject player;

    public Vector3 movespeed;

    private GameObject enemy;
    private Vector3 knockbackDirection;
    private bool gothit;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        GamemanagerX = GameObject.Find("GameManager").GetComponent<GamemanagerX>();
        playerRb = GetComponent<Rigidbody>();
        _rotationSpeed = 1.5f;
        _walkingSpeed = 5;
        enemy = GameObject.FindGameObjectWithTag("enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        gothit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gothit)
        {
            timer += Time.deltaTime;
        }
        if (timer > 0.5f)
        {
            gothit = false;
        }
        
        //camera movement
        if (Input.GetAxis("Mouse X") > 0)
        {
            transform.Rotate((Vector3.up) * _rotationSpeed);
        }
        else if (Input.GetAxis("Mouse X") < 0)
        {
            transform.Rotate((Vector3.up) * -_rotationSpeed);
        }

        //player movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 horizontalMoveSpeed = horizontal * transform.right * Time.deltaTime;
        Vector3 verticalMoveSpeed = vertical * transform.forward * Time.deltaTime;
        Vector3 totalMoveSpeed = horizontalMoveSpeed + verticalMoveSpeed;
        totalMoveSpeed = Vector3.Normalize(totalMoveSpeed);
        totalMoveSpeed *= _walkingSpeed;
        if (!gothit)
        {
            playerRb.velocity = new Vector3(totalMoveSpeed.x, playerRb.velocity.y, totalMoveSpeed.z);
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(pBody.transform.position, Vector3.down, out hit, 0.8f, groundLayers);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("is colliding " + other.collider.tag);
        if (other.collider.tag == "enemyCollider")
        {
            Destroy(other.gameObject);
        }
        if (other.collider.tag == "enemy")
        {
            GamemanagerX.UpdateLives(1);
            knockbackDirection = playerRb.transform.position - other.transform.position;
            knockbackDirection = knockbackDirection.normalized;
            knockbackDirection = new Vector3(knockbackDirection.x, playerRb.velocity.y, knockbackDirection.z);
            playerRb.velocity = knockbackDirection * 15f;
            gothit = true;
            timer = 0;
        }
    }
}