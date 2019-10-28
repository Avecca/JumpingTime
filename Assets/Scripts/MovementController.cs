using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;  //for buttons

public class MovementController : MonoBehaviour
{

    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private Rigidbody2D rb;
    [SerializeField]
    float movementDir;
    //[SerializeField]
    //private GameManager gameManager; 

    [SerializeField]
    private bool doubleJumpActive = false;
    private int jumped;
    private bool jumpPossible = false;
    private float swipeNeededToJump = 0.15f; //15% of the screen
    private float jumpDir;
    private bool facingRight = true;
    Vector3 localScale;  //vDino facing direction

    //Able to change in inspector
    [SerializeField]
    private float jumpSpeed = 620f;
    [SerializeField]
    private float moveSpeed = 6f;

    private bool stopAllMovement = false;
    //see in inspector if jumping
    public bool AIR;

    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        jumped = 0;
    }

    void Update()
    {
        if (!stopAllMovement)
        {
            WalkCheck();
            JumpCheck();
        }
        // Debug.Log(dirX);
    }

    //physics update, things using rigidbody should go here
    private void FixedUpdate()  
    {
        //TODOunless game over
        if (!stopAllMovement)
        {
            TryToWalk();
            TryToJump();
            AIR = onGroundCheck;
        }
    }

    private void LateUpdate()
    {
        if (!stopAllMovement)
        { //face the dino in the correct dierction
            CheckLookingDir();
        }
    }

    private void CheckLookingDir()
    {
        if (movementDir > 0)
        {
            facingRight = true;
        }
        else if (movementDir < 0)
        {
            facingRight = false;
        }

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }

    private void JumpCheck()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
            jumpDir = CrossPlatformInputManager.VirtualAxisReference("Horizontal").GetValue;
           // Debug.Log("JUmp try started with jump dir " + jumpDir);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;

           // Debug.Log("JUmp try ended");
            Vector2 diff = startTouchPos - endTouchPos;
            diff = new Vector2(diff.x / Screen.width, diff.y / Screen.height); 

           // Debug.Log("% diff är " + diff.magnitude);
            if (endTouchPos.y > startTouchPos.y && ( onGroundCheck || doubleJumpActive ) && diff.magnitude > swipeNeededToJump)  // velocitynot already in air
            {
                jumpPossible = true;
            }
        }
    }

    private void WalkCheck()
    {
        //movementDir = CrossPlatformInputManager.GetAxis("Horizontal");
        movementDir = CrossPlatformInputManager.VirtualAxisReference("Horizontal").GetValue;
    }

    //not falling
    public bool onGroundCheck
    {  
        get
        {
            return Math.Abs(rb.velocity.y) <= 0;
        }
    }

    private void TryToJump()
    {
        if (jumpPossible)
        {
            //Debug.Log("JUMPED = " + jumped);

            if (onGroundCheck)
            {
               // Debug.Log("1a hoppet");
                rb.AddForce(Vector2.up * jumpSpeed);

                if (doubleJumpActive)
                {
                    jumped++;
                }
            }

            if (!onGroundCheck && jumped > 0)
            {
               //extra umph bcs it's hard to land 2 quickly in a row
                rb.AddForce(Vector2.up * (jumpSpeed * 1.5f));
                jumped = 0;
            }

            jumpPossible = false;
        }
    }
    private void TryToWalk()
    {
       // Debug.Log("Try to walk");
        rb.velocity = new Vector2(movementDir * moveSpeed, rb.velocity.y);
    }

    //collision.collider.transform.SetParent(transform);

    //IF Player hits a 2DCollision object, ie moving platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag.Equals("WinningStar")), happens StarController

        if (collision.gameObject.tag.Equals("movingObject"))
        {
            if (transform.position.y > collision.transform.position.y + 1)  // + 1
            {   //Make the moving Object the Players parent, so the player follows along with the moving object
                 this.transform.parent = collision.transform;
                //this.transform.SetParent(collision.transform);
               // Debug.Log("enter moving object");
            }
        }
    }

    //Player exists a 2Dcollison Object
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("movingObject"))
        {
            this.transform.parent = null;
            //Debug.Log("exit moving object");
        }
    }

    public float GetJumpSpeed()
    {
        return jumpSpeed;
    }

    public void ChangeJumpSpeed(float speed)
    {
        jumpSpeed = speed;
    }

    public void ChangeMovementSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void DoubleJumpActive(bool active)
    {
        doubleJumpActive = active;
    }

    public bool GetDoubleJumpActive()
    {
        return doubleJumpActive;
    }

    public void StopAllMovement(bool stop)
    {
        this.stopAllMovement = stop;
    }

}

#region unused code



//<a href="https://www.vecteezy.com/free-vector/green-grass">Green Grass Vectors by Vecteezy</a>
//<a href = "https://www.vecteezy.com/free-vector/green-grass" > Green Grass Vectors by Vecteezy</a>

//  rb.AddRelativeForce(Vector3.forward * thrust);  //rockt motor

////TODO titta rad 122, velocity
// if (!doubleJumpActive || jumped > 1)
// {
//    // jumpPossible = false;
//    rb.AddForce(Vector2.up * jumpSpeed);
//    jumped = 0;
//    Debug.Log("Ända eller efter 2a hoppet");
//}
//else if(doubleJumpActive && jumped > 0 ) //inte landat
//{
//    if (!onGroundCheck)  //Math.Abs(rb.velocity.y) > 0
//    {
//        rb.AddForce(Vector2.up * (jumpSpeed * 2));
//        Debug.Log("2a hoppetDubbel speed");
//    }
//    else
//    {
//        rb.AddForce(Vector2.up * jumpSpeed);
//        Debug.Log("2a hoppet");
//        jumped = 0;
//    }
//}
//else
//{
//    Debug.Log("1a hoppet");
//    rb.AddForce(Vector2.up * jumpSpeed);
//}

// //todo sätt i de olika if satserna
//jumped++;

//rb.AddForce(Vector2.up * jumpSpeed);  //, ForceMode2D.Impulse
//rb.AddForce(new Vector2(0f, 8.0f), ForceMode2D.Impulse);  //x , y  movementspeed, jumpspeed
//rb.AddForce(new Vector2((10f), jumpSpeed));
//rb.AddForce(Vector2.right * (jumpDir * moveSpeed));

// rb.AddRelativeForce((jumpDir * jumpSpeed), 0.0f, jumpSpeed, ForceMode.Force);
//rb.AddRelativeForce(Vector2.left * jumpSpeed);

//speedbost
//rb.AddForce(transform.forward * jumpSpeed, ForceMode2D.Impulse);
//rb.AddForce(Vector2.right * jumpSpeed);

#endregion
