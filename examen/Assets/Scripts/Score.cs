using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private GamemanagerX GamemanagerX;

    public float pointsForHead; 
    public float pointsForBody; 
    // Start is called before the first frame update
    void Start()
    {
        GamemanagerX = GameObject.Find("GameManager").GetComponent<GamemanagerX>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "TargetBody")
        {
            GamemanagerX.UpdateScore(pointsForBody);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "TargetHead")
        {
            GamemanagerX.UpdateScore(pointsForHead);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
