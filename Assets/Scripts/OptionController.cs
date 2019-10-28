using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionController : MonoBehaviour
{

    //Game options including sound, restart, quit

    public GameObject levelMenu, optionMenu;
    [SerializeField]
    private AudioClip click;
    private int currentSceneNr;
    private GameObject gObj;
  

    //Level Over Choices, called when a level/scene is done playing
    public void CheckNextScene()
    {
        currentSceneNr = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("STartNextLevel " + (currentSceneNr + 1));
        //if next level doesnt exist
        if (!Application.CanStreamedLevelBeLoaded(currentSceneNr + 1))
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Game Ended";
            gObj = GameObject.Find("OptionButtons/Next");
            if (gObj != null)
            {
                gObj.SetActive(false);
            }
        } //If Exists is handled in Gamemanager

    }

    public void RestartScene()
    {
       // Debug.Log("RESTART SCENE");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //timescale - done in sceneManagement
    }


    public void onClick()
    {
        SoundManager.Instance.PlayEffect(click);
    }


    public void Slider()
    {
        //TODO GameManager.Instance
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }


    public void SoundToggle()
    {
        SoundManager.Instance.TurnSoundToggle();
    }


    public void DifficultySlider()
    {
        Debug.Log("Difficulty changed");
    }

}

#region unused code
//OptionChoices, Can do in Inspector
//public void ShowOptionMenu()
//{
//    Debug.Log("Show menu!");

//    if (SceneManager.GetActiveScene().buildIndex > 0)
//    {
//        levelMenu.SetActive(false);
//    }

//    //levelOverMenu.SetActive(false);
//    optionMenu.SetActive(true);

//    //TODO
//    //QUIT
//    //STARTFROM GAmeMenu
//    //No sound
//}

//public void CloseOptionWindow()
//{
//    if (SceneManager.GetActiveScene().buildIndex > 0)
//    {
//        levelMenu.SetActive(true);
//    }

//    optionMenu.SetActive(false);

//}
#endregion
