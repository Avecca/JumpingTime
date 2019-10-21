using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionController : MonoBehaviour
{

    public GameObject levelOverMenu, optionMenu;

    //private string currentSceneName;

        //TODO ACTIVATE SCENES

    //Level Over Choices
    public void StartNextScene()
    {

        Debug.Log("STartNextLevel");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void RestartScene()
    {
        Debug.Log("RESTART SCENE");
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //timescale
    }


    //OptionChoices, Can do in Inspector
    public void ShowOptionMenu()
    {
        Debug.Log("Show menu!");

        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            levelOverMenu.SetActive(false);
        }

        //levelOverMenu.SetActive(false);
        optionMenu.SetActive(true);

        //TODO
        //QUIT
        //STARTFROM GAmeMenu
        //No sound
    }

    public void CloseOptionWindow()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            levelOverMenu.SetActive(true);
        }

        optionMenu.SetActive(false);

    }

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
