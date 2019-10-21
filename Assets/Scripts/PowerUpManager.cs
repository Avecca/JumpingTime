using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class PowerUpManager : MonoBehaviour
{

    //seperate entitys, connect through inspector
    // public TimerController timerController;

    //public TextMeshPro timeController;
    //public TextMeshProUGUI timeController;

   // public PowerController powerController;   DENNA SIST

    //[SerializeField]
    private GameObject powerUp;
    GameManager gameManager;

    [SerializeField]
    private List<GameObject> boosters = new List<GameObject>();
    int ran;


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

        // minSpawnTimer = 5.0f;
        // maxSpawnTimer = 12.0f;

        if (boosters == null)
        {
            return;
        }

        gameManager = GetComponent<GameManager>();

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
        ran = random.Next(0, boosters.Count);
        powerUp = boosters[ran];

        Debug.Log("Tring to spawn in manager!");
        // powerController.SpawnPower();

        GameObject power = Instantiate(powerUp);  //TODO RANDOM POWERUP GameObject.FindGameObjectWithTag("BoosterPanels").transform
        power.SetActive(true);
        //all the powers should be able to acess PowerUpManager, for updates and death
        //PowerController pc = power.GetComponentInChildren<PowerController>();
        PowerController pc = power.GetComponent<PowerController>();

        //PowerController pc = power.GetComponent<PowerController>();
        pc.powerUpManager = this;

        //give it a parent in the UI, this case the panels, stops the shaking...... power.transform
        pc.transform.parent = GameObject.FindGameObjectWithTag("BoosterPanels").transform;



        RandomSpawnTimerAndPlace();
        timeSinceSpawn = 0;
    }

    private void RandomSpawnTimerAndPlace()
    {

        
        spawnTimer = UnityEngine.Random.Range(minSpawnTimer, maxSpawnTimer);
        //choosenPanel = random.Next(0, 2);  //0 or 1
        //

        Debug.Log("Picking random time " + spawnTimer  );  // and place  choosenPanel
    }


    public void AllowSpawns( bool allowSpawns)
    {
        Debug.Log("Allow spawns " + allowSpawns);
        SpawnsAllowed = allowSpawns;
    }


    public void DestroyPowerup(GameObject power)
    {
        Debug.Log("Destroy Power");
        Destroy(power);
    }

    public void UsePowerup(String power)
    {

        switch (power)
        {
            case "xTime5":
                gameManager.AdjustGameTime(5);
                    break;
            case "xTime-5":
                gameManager.AdjustGameTime(-5);
                break;
            case "xJump5":
                gameManager.AdjustJumpSpeed(900);  //600 is normal
                break;
            case "xJump-5":
                gameManager.AdjustJumpSpeed(450);
                break;
            case "xSpeed5":
                gameManager.AdjustGameSpeed(1.5f);
                break;
            case "xSpeed-5":
                gameManager.AdjustGameSpeed(0.7f);
                break;
            case "xDoubleJump":
                gameManager.AllowDoubleJump();
                break;
            default:

                Debug.Log("No power was found");
                break;
        }


    }

    //collider.gameObject.SetActive(false);
}
