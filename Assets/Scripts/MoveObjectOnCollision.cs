using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectOnCollision : MonoBehaviour
{

    //Script for making an object start moving when a player lands on top

    //add platform effector 2d
    [SerializeField]
    private Vector3 velocity;  //sätt x1 y0 z0

    private bool moving;

    private void Start()
    {
        velocity.Set(1, 0, 0);
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            transform.position += (velocity * Time.deltaTime);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            //if the playform is lower in y than the player
            if (transform.position.y < collision.transform.position.y +1)  
            {
                collision.collider.transform.SetParent(transform);
                moving = true;
            }
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.collider.transform.SetParent(null);
            moving = false;

        }

    }

}
