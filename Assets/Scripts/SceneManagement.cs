using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    //Must exist in every scene, at the top
    //TODO Scene specific data
    //TODOLevel manager som har koll på alla stats för närvarande nivån, powers, timer osv?
    //spawna Player?
    //change music
    //se till att gamemanager häner med?

    [SerializeField]
    private AudioClip backGroundSound;

    private int currentSceneIndex;
    AsyncOperation async;
    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //SoundManager.Instance.transform.position = Vector3.zero;
        //SoundManager.Instance();

        PlayStartSound();

        if (SceneManager.GetActiveScene().name == "Start")  //main menu, index == 0
        {
            PreLoadNextScene();

        }
        else
        {
            //TODO WHEN SINGLETON GAMEMANAGER
            GameManager.Instance.NewLevelSceneStart();
        }

       

        //TODO this scenes changes! music, new stats in game manager osv, andra boosters
    }

    private void PlayStartSound()
    {

        
        if (backGroundSound != null)
        {
            //Debug.Log("Sending sound to SM");
            SoundManager.Instance.PlayBackground(backGroundSound);
        }
        
    }

    public void PreLoadNextScene()
    {

        Debug.Log("PreLoading next scene!!!");
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (Application.CanStreamedLevelBeLoaded(currentSceneIndex + 1))
        {
            async = SceneManager.LoadSceneAsync(currentSceneIndex + 1);
            async.allowSceneActivation = false;
            //SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void StartNextScene()
    {
        Debug.Log("STartNextLevel " + (currentSceneIndex + 1));
        if (async != null)
        {
            Debug.Log("Async is present " + async);
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


    //SCene 2, 


}
