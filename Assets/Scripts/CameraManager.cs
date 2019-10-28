using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager: MonoBehaviour
{

    //Camera follows the player
    public Transform player;
    private float cameraDistance = 100.0f;

    //player isnt exactly center of the view but a few steps below
    private float cameraAbove = 02f;

    private void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    }

    private void FixedUpdate()
    {
       // cameraAbove = ((Screen.height /4));
        transform.position = new Vector3( player.position.x,( player.position.y + cameraAbove), transform.position.z);
    }
}
