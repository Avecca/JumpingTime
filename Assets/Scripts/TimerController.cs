using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    TextMeshProUGUI text;

    [SerializeField]
    private float timeLeft = 30f;

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

    private int interval = 3;


    private void Start()
    {
        //text = GetComponentInChildren<Text>();
        text = GetComponent<TextMeshProUGUI>();

        

        Debug.Log(text);
    }

    private void Update()
    {
       // if (Time.frameCount % interval == 0)
       // {

           // Debug.Log("Update +  " + text);

            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 0;
            }
            
            text.text = Mathf.Round(timeLeft).ToString();
        //}
    }


    public void AddTime(float time)
    {
        TimeLeft = TimeLeft + time;
    }

    public void SubtractTime(float time)
    {
        TimeLeft = TimeLeft - time;
    }

}
