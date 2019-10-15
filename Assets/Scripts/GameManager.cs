using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    //Under the same "item" GameManager, init i start då 

    //seperate entitys, connect through inspector
    public GameObject hurry, gameOver;

    public TimerController timerController;
    public MovementController movementController;

    private int showHurryMax = 8;
    private int showHurryMin = 5;


    private string currentSceneName;


    //public TextMeshProUGUI timeTextController;

    // Start is called before the first frame update
    void Start()
    {
        //TODO sätt olika timers per bana
        currentSceneName = SceneManager.GetActiveScene().name;
    }


    private void Update()
    {
        ControlTimer();
    }



    private void ControlTimer()
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
