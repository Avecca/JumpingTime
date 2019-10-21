using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionController : MonoBehaviour
{

    public GameObject levelMenu, optionMenu;

    private int currentSceneNr;
    private GameObject gObj;

        //TODO ACTIVATE SCENES

        

        

    //Level Over Choices
    public void CheckNextScene()
    {
        currentSceneNr = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("STartNextLevel " + (currentSceneNr + 1));
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
        Debug.Log("RESTART SCENE");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //timescale
    }


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

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }


    public void SoundToggle()
    {
        Debug.Log("SoundToggle");
    }


    public void DifficultySlider()
    {
        Debug.Log("Difficulty changed");
    }


}
