using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager: MonoBehaviour
{


    public Transform player;
    private float cameraDistance = 100.0f;

    private float cameraAbove = 02f;

    private void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    }


    private void FixedUpdate()
    {

       // cameraAbove = ((Screen.height /4));

        //Kanske inte tillåta x att flyttas
        transform.position = new Vector3( player.position.x,( player.position.y + cameraAbove), transform.position.z);
    }
}
