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

    [SerializeField]
    private List<GameObject> powers = new List<GameObject>();
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

        if (powers == null)
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
        if (SpawnsAllowed)
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
        ran = random.Next(0, powers.Count);
        powerUp = powers[ran];

        Debug.Log("Tring to spawn in manager!");
        // powerController.SpawnPower();

        GameObject power = Instantiate(powerUp);  //TODO RANDOM POWERUP GameObject.FindGameObjectWithTag("BoosterPanels").transform
        power.SetActive(true);
        //all the powers should be able to acess PowerUpManager, for updates and death
        PowerController pc = power.GetComponentInChildren<PowerController>();
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
        Debug.Log("Destroy POwer");
        Destroy(power);
    }

    //collider.gameObject.SetActive(false);
}
