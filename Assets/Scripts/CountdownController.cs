using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownController : MonoBehaviour
{

    //Countdown before the game scene is allowd to "start"

    [SerializeField]
    private float timeTillStart = 5f;
    private float countDownFrom = 5f;

    public TextMeshProUGUI text;
  
    private bool countingDown = false;
    // GameManager gameManager;
    void Start()
    {
        // gameManager = GetComponent<GameManager>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeTillStart <= 0)
        {
            text.text = "GO!";
        }else
        {
            text.text = timeTillStart.ToString();
        }     
    }

    //called from gamemanager
    public void StartCountDown()
    {
        //gameObject.SetActive(true);
        timeTillStart = countDownFrom;
        countingDown = true;
        StartCoroutine("CountdownStarted");
    }


    IEnumerator CountdownStarted()
    {
        while (countingDown)
        {

            yield return new WaitForSeconds(1);
            timeTillStart--;

            if (timeTillStart < 0)
            {
                Debug.Log("MINDRE ÄN 0");
                countingDown = false;
                //gameObject.SetActive(false);
                //only text is inactive
                text.gameObject.SetActive(false); 
                //gameManager.StartGameTimer();
                //start rest of the game
                GameManager.Instance.StartGameTimer();
                
            }
        }
    }
}
