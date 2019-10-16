using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    //Under the same "item" GameManager, init i start då
    CountdownController countdownController;

    //seperate entitys, connect through inspector
    public GameObject hurry, gameOver,countdown;  //todo remove countdown

    public TimerController timerController;
    //todo to stop movement stopMovement in StopMovement(true);
    public MovementController movementController;  
    //public CountdownController countdownController;

    private int showHurryMax = 8;
    private int showHurryMin = 5;
   // private int startCountDown = 5; in timercontroller


    private string currentSceneName;


    //public TextMeshProUGUI timeTextController;

    // Start is called before the first frame update
    void Start()
    {
        //TODO sätt olika timers per bana
        currentSceneName = SceneManager.GetActiveScene().name;
        countdownController = GetComponent<CountdownController>();
        StartSequence();
    }

    private void StartSequence()
    {

        movementController.StopAllMovement(true);
        countdownController.StartCountDown();
    }

    private void Update()
    {
        ControlGameTimer();
    }



    private void ControlGameTimer()
    {
        if (timerController.TimeLeft <= showHurryMax && timerController.TimeLeft >= showHurryMin)
        {
            hurry.gameObject.SetActive(true);
        }
        else
        {
            hurry.gameObject.SetActive(false);
        }


        if ( timerController.TimeLeft <= 0)
        {
            Time.timeScale = 0;
            gameOver.gameObject.SetActive(true);
            //TODO restart o continue button activa beroende på poäng

            //Stop char from moving

            
        }
    }


    public void StartGameTimer()
    {
        Debug.Log("Trying to start game timer");
        movementController.StopAllMovement(false);
        timerController.StartGame();

    }

    public void RestartScene()
    {
        Debug.Log("RESTART SCENE");
        //SceneManager.LoadScene(currentSceneName);
    }


    public void StartNextScene()
    {

        Debug.Log("STartNextGame");
        //SceneManager.LoadScene("SCENENENENENEN");
    }
}
