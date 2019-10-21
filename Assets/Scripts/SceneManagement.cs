using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    //Must exist in every scene, at the top


    private int currentSceneIndex;
    AsyncOperation async;
    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 0)
        {
            PreLoadNextScene();
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


}
