using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private GamemanagerX GamemanagerX;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        GamemanagerX = GameObject.Find("GameManager").GetComponent<GamemanagerX>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            GamemanagerX.UpdateScore(pointValue);
            Destroy(gameObject);
            GamemanagerX.TargetCount -= 1;
        }
    }
}