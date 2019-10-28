using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController : MonoBehaviour
{


    //For moving a playform between 2 points
    [SerializeField]
    private float dirX, moveSpeed = 3f;

    private Vector3 pos1;  //current
    private Vector3 pos2;
    private Vector3 nextPos;

    //physical representation of the start and endpoints
    [SerializeField]
    private Transform startTransform;
    [SerializeField]
    private Transform endTransform;


    private void Start()
    {
        pos1 = startTransform.localPosition;  //relativt to parent, static in the world view
        pos2 = endTransform.localPosition;
        nextPos = pos2;   
    }


    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        startTransform.localPosition = Vector3.MoveTowards(startTransform.localPosition, nextPos, moveSpeed * Time.deltaTime);

        //If platform reaches the end point, change direction by chenging nextPos
        if (Vector3.Distance(startTransform.localPosition, nextPos) <= 0.1)
        {
            ChangeNextPosition();
        }
    }

    private void ChangeNextPosition()
    {
        nextPos = nextPos != pos1 ? pos1 : pos2;
    }
}


#region unused code
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
#endregion
