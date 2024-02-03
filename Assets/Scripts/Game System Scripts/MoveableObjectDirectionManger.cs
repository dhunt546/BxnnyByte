using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjectDirectionManger : MonoBehaviour
{
    public Rigidbody2D rb;

    //public Animator animator;

    Vector2 movement;

    bool north;
    bool south;
    bool east;
    bool west;

    void Update()
    {
        ObjectMovementController();      
    }

    void FixedUpdate()
    {
        RigidBodyMovement();
    }

    void ObjectMovementController()
    {     

        if (movement.y > 0)
        {
            north = true;
            south = false;
            east = false;
            west = false;
        }
        if (movement.y < 0)
        {
            north = false;
            south = true;
            east = false;
            west = false;
        }
        if (movement.x > 0)
        {
            north = false;
            south = false;
            east = true;
            west = false;
        }
        if (movement.x < 0)
        {
            north = false;
            south = false;
            east = false;
            west = true;
        }
    }

    //void AnimatorMovementController()
    //{
    //    //animator.SetFloat("horizontal", movement.x);
    //    //animator.SetFloat("vertical", movement.y);
    //    //animator.SetFloat("speed", movement.sqrMagnitude);
    //
    //    animator.SetBool("north", north);
    //    animator.SetBool("south", south);
    //    animator.SetBool("east", east);
    //    animator.SetBool("west", west);
    //}

    void RigidBodyMovement()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }
}


