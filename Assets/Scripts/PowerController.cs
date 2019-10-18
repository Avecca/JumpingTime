using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerController : MonoBehaviour, IPointerDownHandler
{


    //public GameObject powerUp;  // +
   // public GameObject powerDown;  //-


        //TODO värdelöst om gravity
    [SerializeField]
    private float speed = 10f;

    //[SerializeField]
    // private Transform powerPos;

    //private int randomPower;

    private int spawnPoint;
    private System.Random random;

    [SerializeField]  //TODO behöver inte Btn pos
    private Transform leftSpawnPos, rightSpawnPos, leftBtn, rightBtn;


    private Vector3 pos1;  //current
    private Vector3 pos2;

    private Vector3 position;

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
        spawnPoint =  random.Next(0, 2);
        Debug.Log("Flyttar till hörn");

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
        }
        else
        {
            position = new Vector3(leftSpawnPos.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);

            //transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * speed);
        }

        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * speed);

        RectTransform rectT = leftBtn.GetComponent<RectTransform>();

        Vector3[] corners = new Vector3[4];
        rectT.GetWorldCorners(corners);

        float height = corners[2].y - corners[0].y;
        //float height = rectT.GetWorldCorners(); //rectT.rect.height / 2 ;
       // float height2 = corners2[2].y - corners2[0].y;

        Debug.Log(height + " HEIGHT * 2");

        if (transform.position.y < leftBtn.position.y + (height * 2)) //leftBtn.position.y
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
        Debug.Log("Pressed!!!!!!!");
    }
}
