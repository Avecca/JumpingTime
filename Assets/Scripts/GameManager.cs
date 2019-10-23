using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    //private static GameManager _instance;

    ////singleton
    //public static GameManager Instance
    //{
    //    get
    //    {

    //        if (_instance == null)
    //        {
    //            _instance = new GameManager();
    //        }
    //        return _instance;
    //    }
    //}





    //Under the same "item" GameManager, init i start då
    CountdownController countdownController;
    PowerUpManager powerUpManager;

    //seperate entitys, connect through inspector
    public GameObject hurry, levelOverMenu, optionMenu, countdown;  //todo remove countdown

    public TimerController timerController;
    //todo to stop movement stopMovement in StopMovement(true);
    public MovementController playerMovementController;  
    //public CountdownController countdownController;

    private int showHurryMax = 8;
    private int showHurryMin = 5;
    // private int startCountDown = 5; in timercontroller
    public int powerLastingTimer = 15;
    private float jumpSpeedOrigional;
    private bool gameIsRunning = true;

    //todo remove public
    [SerializeField]
    private int starsCaught;

    Color32 yellow = new Color32(224, 212, 35, 255);


    // private int currentSceneIndex;
    // AsyncOperation async;

    //Handle the Scene changes
    private GameObject currentSceneManagement;
    private SceneManagement currentSceneManagementScript;
    private GameObject nextButton;


    //public TextMeshProUGUI timeTextController;

    // Start is called before the first frame update
    void Start()
    {

        StartSequence();

        //Singleton
        //SoundManager.Instance.PlayBackground();

    }




    private void Update()
    {
        ControlGameTimer();
    }

    //TODOLevel manager som har koll på alla stats för närvarande nivån, powers, timer osv?
    //  == Scenemanagement

    private void StartSequence()
    {

        //TODO sätt olika timers per bana
        //currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        countdownController = GetComponent<CountdownController>();
        powerUpManager = GetComponent<PowerUpManager>();
        jumpSpeedOrigional = playerMovementController.GetJumpSpeed();
        starsCaught = 0;


        //TODO IF STartScene
        //Countdown till start
        playerMovementController.StopAllMovement(true);
        countdownController.StartCountDown();

        //TODO if startscene preload first

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
                hurry.gameObject.SetActive(false);
            }


            if (timerController.TimeLeft <= 0)
            {

                Debug.Log("Times up!");

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



        //TODO PreLOAD next Scene

        
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
        Debug.Log("Trying to start game timer");
        playerMovementController.StopAllMovement(false);
        timerController.StartGame();
        powerUpManager.AllowSpawns(true);
    }


    public void AdjustGameTime(float time)
    {
        Debug.Log("AdjutGameTime " + time);
        //todo  if(gameRunning)
        timerController.AdjustTime(time);
    }
    

    public void AdjustJumpSpeed(float speed)
    {

        Debug.Log("JumpSpeed " + speed);
       StartCoroutine(ChangeJumpSpeed(speed));
    }

    IEnumerator ChangeJumpSpeed(float speed)
    {

        playerMovementController.ChangeJumpSpeed(speed);

        yield return new WaitForSeconds(powerLastingTimer);

        //Taken at Start
        playerMovementController.ChangeJumpSpeed(jumpSpeedOrigional);


    }

    public void AdjustGameSpeed(float speed)
    {
        Debug.Log("GameSpeed " + speed);

        StartCoroutine(ChangeGameSpeed(speed));

    }

    IEnumerator ChangeGameSpeed(float speed)
    {
        Time.timeScale = speed;
        yield return new WaitForSeconds(powerLastingTimer);
        Time.timeScale = 1.0f;
    }

    public void AllowDoubleJump()
    {
        Debug.Log("Allow DoubleJump");
        StartCoroutine(DoubleJump());
    }


    IEnumerator DoubleJump()
    {
        playerMovementController.DoubleJumpActive(true);

        yield return new WaitForSeconds(powerLastingTimer);
        playerMovementController.DoubleJumpActive(false);
    }

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
