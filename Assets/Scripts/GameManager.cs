using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    //singleton
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameObject>("GameManager")).GetComponent<GameManager>(); //new SoundManager();
                Debug.Log("GameManager instantiated");
            }
            return _instance;
        }
    }


    //Objects and scripts the GameManager utilizes- finds all of the in script since becoming singleton
    //Under the same "item" GameManager, init i start då
    [SerializeField]
    CountdownController countdownController;
    [SerializeField]
    PowerUpManager powerUpManager;
    //seperate entitys, connect through inspector
    public GameObject hurry, levelOverMenu, optionMenu, countdown; 
    public TimerController timerController;
    //todo to stop movement stopMovement in StopMovement(true);
    public MovementController playerMovementController;
    //public CountdownController countdownController;

    [SerializeField] //enable level complete through inspector
    private int starsCaught;
    private int showHurryMax = 8;
    private int showHurryMin = 5;
    //How long the powerups last
    public int powerLastingTimer = 10;
    //incase of changes
    private float jumpSpeedOrigional;
    private bool gameIsRunning = true;
    Color32 yellow = new Color32(224, 212, 35, 255);

    // private int currentSceneIndex;
    // AsyncOperation async;

    //Handle the Scene changes
    private GameObject currentSceneManagement;
    private SceneManagement currentSceneManagementScript;
    private GameObject nextButton;


    private void Awake()
    {
        if (_instance != null && _instance != this) 
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        ControlGameTimer();
    }

    //TODOLevel manager som har koll på alla stats för närvarande nivån, powers, timer osv?
    //  == Scenemanagement

    //TODO doing this backwards - scripts should fetch from gamemanager not the other way around
    private void StartSequence()
    {
        Debug.Log("STARTSEQUENCE");
        gameIsRunning = false;

        //TODO sätt olika timers per bana
        //currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //Find all the objects/scripts that gamenanager uses
        //scripts
        playerMovementController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        timerController = GameObject.Find("Canvas/Timer").GetComponent<TimerController>();
        countdownController = GameObject.Find("SceneManagement").GetComponent<CountdownController>(); //GetComponent<CountdownController>();
        powerUpManager = GameObject.Find("SceneManagement").GetComponent<PowerUpManager>(); //GetComponent<PowerUpManager>();
        //objects
        countdown = GameObject.Find("Canvas/Countdown");
        hurry = GameObject.Find("Canvas/Hurry");
        levelOverMenu = GameObject.Find("Canvas/LevelOverMenu");
        optionMenu = GameObject.Find("Canvas/OptionsMenu");

        hideNonStartObjects();

        jumpSpeedOrigional = playerMovementController.GetJumpSpeed();
        starsCaught = 0;

        //Countdown till start
        playerMovementController.StopAllMovement(true);
        countdownController.StartCountDown();

    }

    private void hideNonStartObjects()
    {
        hurry.SetActive(false);
        levelOverMenu.SetActive(false);
        optionMenu.SetActive(false);
    }

    public void NewLevelSceneStart()
    {
        StartSequence();
    }

    private void ControlGameTimer()
    {
        if (gameIsRunning)
        {

            if (timerController.TimeLeft <= showHurryMax && timerController.TimeLeft >= showHurryMin)
            {
                hurry.gameObject.SetActive(true);
            }
            else
            {
                if (hurry.gameObject.activeSelf)
                {
                    hurry.gameObject.SetActive(false);
                }
            }

            if (timerController.TimeLeft <= 0)
            {
               // Debug.Log("Times up!");
                if (starsCaught > 0)
                {
                    LevelOverMenu("Level Success");
                }
                else
                {
                    LevelOverMenu("Level Over");
                }
            }
        }
    }

    public void StarPassed()
    {
        starsCaught++;
    }

    public void LevelOverMenu(string msg)
    {
        //Stop char/everything from moving
        Time.timeScale = 0;
        
        levelOverMenu.gameObject.SetActive(true);
        levelOverMenu.GetComponentInChildren<TextMeshProUGUI>().text = msg;

        if (starsCaught > 0)
        {
            colorStars();
        }
        else  //not be able to play next level
        {
            nextButton = GameObject.Find("OptionButtons/Next");
            if (nextButton != null)
            {
                nextButton.SetActive(false);
            }
        }

        //stop spawning powerups
        powerUpManager.AllowSpawns(false);
        //TODO restart o continue button activa beroende på poäng

        //Only happen once
        gameIsRunning = false;

        PreLoadNextScene();
    }

    private void PreLoadNextScene()
    {
        //currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //if (Application.CanStreamedLevelBeLoaded(currentSceneIndex + 1))
        //{
        //    async = SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        //    async.allowSceneActivation = false;
        //    //SceneManager.LoadScene(currentSceneIndex + 1);
        //}

        currentSceneManagement = GameObject.Find("SceneManagement");

        if (currentSceneManagement != null)
        {
            currentSceneManagementScript = currentSceneManagement.GetComponent<SceneManagement>();

            currentSceneManagementScript.PreLoadNextScene();
        }
    }

    private void colorStars()
    {
        GameObject g1 = GameObject.Find("ProgressImage");

        List<Image> list = new List<Image>();
        foreach (var item in g1.GetComponentsInChildren<Image>())
        {
            list.Add(item);
        }

       // Debug.Log(list.Count);

        for (int i = 0; i < list.Count; i++) 
        {
            if (starsCaught > i)
            {
                list[i].color = yellow;
            }    
        }
    }

    public void StartGameTimer()
    {
        //TODO gameIsrnning = true
        gameIsRunning = true;
        //Debug.Log("Trying to start game timer");
        playerMovementController.StopAllMovement(false);
        timerController.StartGame();
        powerUpManager.AllowSpawns(true);
    }


    public void AdjustGameTime(float time)
    {
        // Debug.Log("AdjutGameTime " + time);

        if (gameIsRunning)
        {
            timerController.AdjustTime(time);
        }
    }
    
    public void AdjustJumpSpeed(float speed)
    {
        // Debug.Log("JumpSpeed " + speed);
        if (gameIsRunning)
        {
            StartCoroutine(ChangeJumpSpeed(speed));
        }
    }

    IEnumerator ChangeJumpSpeed(float speed)
    {
        if (gameIsRunning)
        {
            playerMovementController.ChangeJumpSpeed(speed);

            yield return new WaitForSeconds(powerLastingTimer);
            //Taken at Start
            playerMovementController.ChangeJumpSpeed(jumpSpeedOrigional);
        }
    }

    public void AdjustGameSpeed(float speed)
    {
        // Debug.Log("GameSpeed " + speed);
        if (gameIsRunning)
        {
            StartCoroutine(ChangeGameSpeed(speed));
        }
    }

    IEnumerator ChangeGameSpeed(float speed)
    {
        Time.timeScale = speed;
        //only last 10 secs -3
        yield return new WaitForSeconds(powerLastingTimer - 3);
        Time.timeScale = 1.0f;
    }

    public void AllowDoubleJump()
    {
       // Debug.Log("Allow DoubleJump");
        //allow doublejump if not already active
        if (!playerMovementController.GetDoubleJumpActive() && gameIsRunning)
        {
            StartCoroutine(DoubleJump());
        } 
    }

    IEnumerator DoubleJump()
    {
        playerMovementController.DoubleJumpActive(true);

        yield return new WaitForSeconds(powerLastingTimer);
        playerMovementController.DoubleJumpActive(false);
    }

    //TODO not called yet, only doable while gameiSrunning
    public void AdjustDifficulty(float time)
    {
        //TODO  !gameisrunning
        if (!gameIsRunning)
        {
            timerController.SetStartTimeAccordingToDifficulty(time);
        }
    }


    #region extra code not used

  

    // Start is called before the first frame update
    //void Start()
    //{
    //    //TODO *
    //    // StartSequence();

    //    //Singleton
    //    //SoundManager.Instance.PlayBackground();

    //}

    //public void StartNextScene()
    //{

    //    //if (async != null)
    //    //{
    //    //    Debug.Log("Async is present " + async);
    //    //    async.allowSceneActivation = true;
    //    //}
    //    //else
    //    //{
    //    //    if (Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex + 1))
    //    //    {
    //    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    //    }
    //    //}
    //}


    //TODO, Happens in OptionsController
    //public void RestartScene()
    //{
    //    Debug.Log("RESTART SCENE");
    //    //SceneManager.LoadScene(currentSceneName);
    //    //timescale
    //}


    //public void StartNextScene()
    //{

    //    Debug.Log("STartNextGame");
    //    //SceneManager.LoadScene("SCENENENENENEN");
    //}

    //public void ShowMenu()
    //{
    //    Debug.Log("Show menu!");

    //    levelOverMenu.SetActive(false);
    //    optionMenu.SetActive(true);

    //    //TODO
    //    //QUIT
    //    //STARTFROM GAmeMenu
    //    //No sound
    //}
}



/*
 *
 * difficulty, sliding booster
 * speed on boosters
 * less time
 * Time.timeScale = 2f;
 * yield return waitforsecons(5f);
 * Time.Timescale  1f;
 * 
 */

#endregion
