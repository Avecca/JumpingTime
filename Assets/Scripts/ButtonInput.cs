using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInput : MonoBehaviour
{

    //TODO används inte
    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {


                Vector3 pos = Camera.main.ScreenToViewportPoint(touch.position);

                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                PressedBtn(hit);
            }

        }
    }

    private void PressedBtn(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            switch (hit.collider.tag)
            {
                case "btnRestart":
                    Debug.Log("Restart tag selected");
                    gameManager.RestartScene();
                    break;
                case "btnNext":
                    Debug.Log("Next level tag selected");
                    gameManager.StartNextScene();
                    break;
                default:
                    Debug.Log("Tag doesnt exist");
                    break;
            }

        }
    }
}
