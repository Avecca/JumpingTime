using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;  //for buttons

public class MovementController : MonoBehaviour
{



//TODO SINGLETON? används i game manager



    private Vector2 startTouchPos;
    private Vector2 endTouchPos;

    private Rigidbody2D rb;
    //private Vector3 movement;
    [SerializeField]
    float movementDir;

    [SerializeField]
    private bool doubleJumpActive = false;
    private int jumped;
    private bool jumpPossible = false;
    private float swipeNeededToJump = 0.15f; //20% avskärmen
    private float jumpDir;
    private bool facingRight = true;
    Vector3 localScale;  //vilket håll dino är

    //TODO hide from inspector
    [SerializeField]
    private float jumpSpeed = 600f;
    [SerializeField]
    private float moveSpeed = 6f;   //force?

    float origGravity;

    //TODO remove
    [SerializeField]
    bool AIR = false;

    private bool stopAllMovement = false;





    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        jumped = 0;
        origGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

        //TODO, unless game over
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
        {
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


            //TODO dubbelhopp? rb.velocity ändras för tillåtelse, sen insta ändras om efter hoppet till 0

            //if dubbelhopp DoubleJumpAllowed
            //if else

            Vector2 diff = startTouchPos - endTouchPos;
            diff = new Vector2(diff.x / Screen.width, diff.y / Screen.height);  //kolla större än 20% 0.1f

           // Debug.Log("% diff är " + diff.magnitude);
            if (endTouchPos.y > startTouchPos.y && ( onGroundCheck || doubleJumpActive ) && diff.magnitude > swipeNeededToJump)  // velocitynot already in air
            {
                jumpPossible = true;
               // Debug.Log("JUmp pissoble");

                
                Debug.Log(jumpPossible);
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
            //Debug.Log("JUMP!! " + rb.tag);
            //Debug.Log("JUMP!! " + (jumpDir * jumpSpeed));

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

        //walking on interactable objects
        if (!onGroundCheck)
        {
           // gameObject.layer = 9;  //Falling
        }

    }



    //TODO sätta på moving Object istället? tag.Equals("PLayer")?
    //collision.collider.transform.SetParent(transform);


        //IF Player hits a 2DCollision object
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag.Equals("movingObject"))
        {
            Debug.Log("enter moving objectx TRY");

            if (transform.position.y > collision.transform.position.y + 1)  // + 1
            {
                 this.transform.parent = collision.transform;
                // gameObject.layer = 8;  //moving object
                //jumpSpeed = collision.
                //rb.isKinematic = true;
               // rb.gravityScale = 0;
                //this.transform.SetParent(collision.transform);
                Debug.Log("enter moving object");
            }


        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("movingObject"))
        {

            //rb.isKinematic = false;
           // rb.gravityScale = origGravity;
            this.transform.parent = null;
            Debug.Log("exit moving object");
        }

    }

    public void ChangeJumpSpeed(float speed)
    {
        jumpSpeed = speed;
    }

    public void ChangeMovementSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public bool DoubleJumpActive
    {
        get { return doubleJumpActive; }
        set { doubleJumpActive = value; }
    }


    public void StopAllMovement(bool stop)
    {
        this.stopAllMovement = stop;
    }


    //  rb.AddRelativeForce(Vector3.forward * thrust);  //rockt motor



}

//<a href="https://www.vecteezy.com/free-vector/green-grass">Green Grass Vectors by Vecteezy</a>
//<a href = "https://www.vecteezy.com/free-vector/green-grass" > Green Grass Vectors by Vecteezy</a>







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
