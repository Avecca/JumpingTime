using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{


    // private GameManager gameManager;
    [SerializeField]
    private string starType;

    private void Start()
    {
        starType = gameObject.tag;
    }

    //collect a star
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.tag);

        if (col.gameObject.tag.Equals("Player"))
        {
            if (starType.Equals("WinningStar"))
            {
                //Debug.Log("WINNING STAR!");
                GameManager.Instance.StarPassed();
                GameManager.Instance.LevelOverMenu("Winning Level!");
            }
            else if(starType.Equals("PassingStar"))
            {
                //Debug.Log("STAR!");
                GameManager.Instance.StarPassed();
                //only trigger once
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // gameObject.SetActive(false);
    }

}
