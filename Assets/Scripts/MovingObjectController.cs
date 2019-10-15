using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController : MonoBehaviour
{
    [SerializeField]
    private float dirX, moveSpeed = 2f;
    //[SerializeField]
   // private float stopPoint = 2f;
   // private bool moveRight = true;


    private Vector3 pos1;  //current
    private Vector3 pos2;
    private Vector3 nextPos;

    [SerializeField]
    private Transform childTransform;
    [SerializeField]
    private Transform transform2;


    private void Start()
    {
        pos1 = childTransform.localPosition;
        pos2 = transform2.localPosition;
        nextPos = pos2;   
    }


    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {


        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
        {
            ChangeNextPosition();
        }

        //if (transform.position.x > stopPoint)
        //{
        //    moveRight = false;
        //}
        //else if (transform.position.x < stopPoint)
        //{
        //    moveRight = true;
        //}


        //if (moveRight)
        //{
        //    transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        //}
        //else
        //{
        //    transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        //}
    }

    private void ChangeNextPosition()
    {
        nextPos = nextPos != pos1 ? pos1 : pos2;
    }
}


//int interval = 3;

//Update()
//{
//    if (Time.frameCount % innterval == 0)
//    {
//        gör ngt var 3e frame
//    }
//}

//    delay = new WaitForSeconds(3);

//--- co rutin

//    yield delay
//     är bättre än att skapa många new waitfor
