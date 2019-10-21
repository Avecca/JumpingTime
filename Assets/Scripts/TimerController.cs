using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    TextMeshProUGUI text;

    [SerializeField]
    private float timeLeft = 90f;
    private bool gameRunning = false;


    public float TimeLeft
    {
        get
        {
            return timeLeft;
        }

        set
        {
            timeLeft = value;
        }
    }

    //private int interval = 3;


    private void Start()
    {
        //text = GetComponentInChildren<Text>();
        text = GetComponent<TextMeshProUGUI>();


        Time.timeScale = 1;
       // Debug.Log(text);


    }

    private void Update()
    {
        // if (Time.frameCount % interval == 0)
        // {

        // Debug.Log("Update +  " + text);

        if (gameRunning)
        {
            updateTimer();
        }
        //}
    }

    private void updateTimer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = 0;
        }

        text.text = Mathf.Round(timeLeft).ToString();
    }


    public void AdjustTime(float time)
    {
        TimeLeft = TimeLeft + time;
    }



    public void StartGame()
    {
        gameRunning = true;

        //TODO Ienumerator instead of updateTimer?
    }

    public void PausTime( bool pausTimer)
    {
        gameRunning = pausTimer;
    }

}
