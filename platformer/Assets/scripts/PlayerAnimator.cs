using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //animation forward
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isMovingForward", true);
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isMovingForward", false);
        }
        
        //animation backward
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isMovingBackward", true);
        } 
        else if (!Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isMovingBackward", false);
        }

        //animation right
        if (Input.GetKey(KeyCode.D))
        {
            
            animator.SetBool("isMovingRight", true);
        } 
        else if (!Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isMovingRight", false);
        }
        
        //animation left
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isMovingLeft", true);
        } 
        else if (!Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isMovingLeft", false);
        }
    }
}
