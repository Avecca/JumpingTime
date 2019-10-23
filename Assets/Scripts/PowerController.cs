using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerController : MonoBehaviour, IPointerDownHandler
{


    //Add to PowerUpManagers boosters list to add the  booster to a scene

    //public GameObject powerUp;  // +
   // public GameObject powerDown;  //-


        //TODO värdelöst om gravity
    [SerializeField]
    private float speed = 10f;

    //[SerializeField]
    // private Transform powerPos;

    //private int randomPower;


    //Decide SpawnPoint
    [SerializeField]  //TODO behöver inte Btn pos
    private Transform leftSpawnPos, rightSpawnPos, leftMoveBtn, rightMoveBtn;
    [SerializeField]
    AudioClip clickSound;
    private int spawnPoint;
    private System.Random random;
    private int nrSpawnPoints = 2;
    //current position, to keep it falling inside the panel
    private Vector3 position;

    //type of booster/power, if clicked
    String powerType;


    //Sizes to decide destroy point of booster
    private RectTransform rectT;
    private Vector3[] corners;
    private float recHeight;
    private float boostHeight;

    //private float heightDiff = 1.6f;  //1.6 is the height of the buttons and than some
    //private Vector3 pos1;  //current
    //private Vector3 pos2;

    

    [SerializeField]  //the powerups referense to the manager
    public PowerUpManager powerUpManager;

    private void Start()
    {
        //TODO random left right


        // position =
        StartPower();

        //Rigidbody2D r = gameObject.GetComponentInChildren<Rigidbody2D>();
        //Debug.Log("Gravity " + r.gravityScale);
       // r.gravityScale = 0.1f;

    }



    private void Update()
    {
        MovePower();

        ///TODO IF hit
        ///IF Bottom
    }

    private void StartPower()
    {
        random = new System.Random();
        spawnPoint =  random.Next(0, nrSpawnPoints);  //2 spawnpoints
        Debug.Log("Flyttar till hörn");

        //Point of no touching between booster and direction buttons, boostheight/2 + recHeight
        //half the size ogf the booster
        boostHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        powerType = gameObject.tag;

        if (spawnPoint > 0)
        {
            transform.position = rightSpawnPos.transform.position;  //parent
        }
        else
        {
            transform.position = leftSpawnPos.transform.position;

        }
        

        //transform.Translate(Vector3.down * speed * Time.deltaTime, Space.Self);  //Self.World
    }


    //TODO TEMP REMOVE
    bool moving = true;
    private void MovePower()
    {
        // transform.localPosition = Vector3.MoveTowards(transform.localPosition, leftBtn.localPosition, speed * Time.deltaTime);

        //transform .position = new Vector3(leftSpawnPos.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);

        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftSpawnPos.position.x, leftSpawnPos.transform.x), transform.position.y * speed * Time.deltaTime, transform.position.z);

        //if (moving)
        //{

        //if (position.y > leftSpawnPos.position.y)
        //{

        if (spawnPoint > 0)
        {
            position = new Vector3(rightSpawnPos.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            rectT = rightMoveBtn.GetComponent<RectTransform>();
        }
        else
        {
            position = new Vector3(leftSpawnPos.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            rectT = leftMoveBtn.GetComponent<RectTransform>();

            //transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * speed);
        }

        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * speed);


        corners = new Vector3[4];
        rectT.GetWorldCorners(corners);

        recHeight = (corners[2].y - corners[0].y);


        //float height = rectT.GetWorldCorners(); //rectT.rect.height / 2 ;
        // float height2 = corners2[2].y - corners2[0].y;

        //Debug.Log(height + " HEIGHT * 2");


       // Debug.Log(recHeight + " hmm " + boostHeight);
        if (transform.position.y < (leftMoveBtn.position.y + (recHeight + boostHeight)) || transform.position.y < (rightMoveBtn.position.y + (recHeight + boostHeight))) //leftBtn.position.y
            DestroyPower();

        //    }
        //    else
        //    {

        //        if (powerUpManager != null)
        //        {
        //            Debug.Log("DESTOYINGS!");
        //            moving = false;
        //            DestroyPower();
        //        }

        //    }
        //}

    }





    //TODO TA BORT
    public void SpawnPower()
    {



        Debug.Log("Spawning!");
        //spawnposition = powerPos.position


       // position = leftSpawnPos.transform.position;//leftSpawnPos.localPosition;

      // GameObject power =  Instantiate(powerUp, position , Quaternion.identity);  //leftSpawnPos.transform.position


       // power.transform.position = Vector3.MoveTowards(leftSpawnPos.localPosition, leftBtn.localPosition, speed * Time.deltaTime);

        //TODO in inspector instead
        //power.transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);


       // power.transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World );

        //TODO kill gameObject
    }



    private void DestroyPower()
    {  //TODO inte förstöra parhent
        //GameObject parent = transform.parent.gameObject;
        powerUpManager.DestroyPowerup(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (powerType != null)
        {

            //TODO SOUND
            Debug.Log("Pressed!!!!!!! " + powerType);
            SoundManager.Instance.PlayEffect(clickSound);
            powerUpManager.UsePowerup(powerType);
            DestroyPower();

        }
        
    }
}
