using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //If playwe press W key
        if (Input.GetKey("w"))
        {
            //then set the IsWalking boolean to be true
            
            animator.SetBool("IsWalking", true);

        }


    }
}
