using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed = 4f;
    private Rigidbody rb;
    private Vector3 pos;
    
    public Collider EnemyBodyCollider;
    public Collider EnemydestroyCollider;
    public Collider PlayerCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        pos = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(pos);

        Vector3 newTarget = player.transform.position;
        newTarget.y = transform.position.y;
        transform.LookAt(newTarget);
    }
}
