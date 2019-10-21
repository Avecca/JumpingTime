using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;
    private string starType;


    private void Start()
    {
        starType = gameObject.tag;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.tag);

        if (col.gameObject.tag.Equals("Player"))
        {

            if (starType.Equals("WinningStar"))
            {
                Debug.Log("WINNING STAR!");
                gameManager.StarPassed();
                gameManager.LevelOverMenu("Winning Level!");
            }
            else if(starType.Equals("PassingStar"))
            {
                Debug.Log("STAR!");
                gameManager.StarPassed();
                //only trigger once
                gameObject.SetActive(false);

            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

}
