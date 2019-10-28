using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class PowerUpManager : MonoBehaviour
{


    //create and manage powerups

    private GameObject powerUp;
    //*
    //GameManager gameManager;

     //powerups allowed in this scene
    [SerializeField]
    private List<GameObject> boosters = new List<GameObject>();
    int ran;


    //For deciding When to spawn a powerup
    [SerializeField]
    private float minSpawnTimer = 5.0f, maxSpawnTimer = 12.0f;
    private float spawnTimer;
    private float timeSinceSpawn = 0;
   // private int choosenPanel;

    private bool SpawnsAllowed = false;
    System.Random random;


    
    // Start is called before the first frame update
    void Start()
    {
        if (boosters == null)
        {
            return;
        }
 

        random = new System.Random();

        RandomSpawnTimerAndPlace();
       // timerController.AddTime(5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnsAllowed && boosters != null && boosters.Count > 0)
        {
            timeSinceSpawn += Time.deltaTime;
            if (timeSinceSpawn > spawnTimer)
            {
                SpawnRandomPower();
            }
        }
    }

    private void SpawnRandomPower()
    {

        //TODO Allow 0,1 and 2 to spawn BOTH at the same time? 
        //pick a random powerup type
        ran = random.Next(0, boosters.Count);
        powerUp = boosters[ran];

        //Debug.Log("Tring to spawn in manager!");

        GameObject power = Instantiate(powerUp);  //TODO RANDOM POWERUP GameObject.FindGameObjectWithTag("BoosterPanels").transform
        //activiate the powerup
        power.SetActive(true);
        //all the powers should be able to acess PowerUpManager, for updates and death   
        PowerController pc = power.GetComponent<PowerController>();

        //make the powerup remmeber  this manager
        pc.powerUpManager = this;

        //give it a parent in the UI, this case the panels, stops the shaking...... power.transform
        pc.transform.parent = GameObject.FindGameObjectWithTag("BoosterPanels").transform;


        RandomSpawnTimerAndPlace();
        timeSinceSpawn = 0;
    }


    //Decides when  the next powerup is allowed to spawn
    private void RandomSpawnTimerAndPlace()
    {        
        spawnTimer = UnityEngine.Random.Range(minSpawnTimer, maxSpawnTimer);

       // Debug.Log("Picking random time " + spawnTimer  );  // and place  choosenPanel
    }


    public void AllowSpawns( bool allowSpawns)
    {
        //Debug.Log("Allow spawns " + allowSpawns);
        SpawnsAllowed = allowSpawns;
    }

    public void DestroyPowerup(GameObject power)
    {
       // Debug.Log("Destroy Power");
        Destroy(power);
    }

    public void UsePowerup(String power)
    {      
        switch (power)
        {
            case "xTime5":
                GameManager.Instance.AdjustGameTime(5);
                    break;
            case "xTime-5":
                GameManager.Instance.AdjustGameTime(-5);
                break;
            case "xJump5":
                GameManager.Instance.AdjustJumpSpeed(900);  //600 is normal
                break;
            case "xJump-5":
                GameManager.Instance.AdjustJumpSpeed(450);
                break;
            case "xSpeed5":
                GameManager.Instance.AdjustGameSpeed(1.5f);
                break;
            case "xSpeed-5":
                GameManager.Instance.AdjustGameSpeed(0.7f);
                break;
            case "xDoubleJump":
                GameManager.Instance.AllowDoubleJump();
                break;
            default:

                Debug.Log("No power was found");
                break;
        }
    }
  
}


#region unused code
//if (choosenPanel > 0)
//{
//    Debug.Log("Trying to spawn right!");
//    rightPowerControlPanel.SpawnPower();
//}
//else
//{
//    Debug.Log("Tring to spawn Left!");
//    leftPowerControlPanel.SpawnPower();
//}

//collider.gameObject.SetActive(false);
#endregion
