using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    //Must exist in every scene, at the top
    //Scene specific data

    [SerializeField]
    private AudioClip backGroundSound;

    private int currentSceneIndex;
    AsyncOperation async;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //SoundManager.Instance.transform.position = Vector3.zero;
        //SoundManager.Instance();

        PlayLevelBackgroundSound();

        if (SceneManager.GetActiveScene().name == "Start")  //main menu, index == 0
        {
            PreLoadNextScene();
        }
        else
        {
            GameManager.Instance.NewLevelSceneStart();
        }
    }

    private void PlayLevelBackgroundSound()
    {
        if (backGroundSound != null)
        {
            //Debug.Log("Sending sound to SM");
            SoundManager.Instance.PlayBackground(backGroundSound);
        }
        
    }

    public void PreLoadNextScene()
    {

       // Debug.Log("PreLoading next scene!!!");
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //if next scene exists
        if (Application.CanStreamedLevelBeLoaded(currentSceneIndex + 1))
        {
            async = SceneManager.LoadSceneAsync(currentSceneIndex + 1);
            async.allowSceneActivation = false;
            //SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void StartNextScene()
    {
       // Debug.Log("STartNextLevel " + (currentSceneIndex + 1));
       //if preloaded exists
        if (async != null)
        {
            //Debug.Log("Async is present " + async);
            async.allowSceneActivation = true;
        }
        else
        {
            if (Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex + 1))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

}
