using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownController : MonoBehaviour
{

    //TODO WHEN SINGLETON




    [SerializeField]
    private float timeTillStart = 5f;
    private float countDownFrom = 5f;

    public TextMeshProUGUI text;



    GameManager gameManager;

    private bool countingDown = false;

    //TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        //text = GetComponent<TextMeshProUGUI>();

        gameManager = GetComponent<GameManager>();
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
                //Debug.Log("MINDRE ÄN 0");
                countingDown = false;
                //gameObject.SetActive(false);

                text.gameObject.SetActive(false); // .enabled = false;
                //TODO WHEN SINGLETON
                gameManager.StartGameTimer();
                
            }
        }
    }
}
