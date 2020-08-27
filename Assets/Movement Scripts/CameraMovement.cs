using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Transform locationOfPlayer;

    public float xLeftClamp;
    public float xRightClamp;
    public float yLeftClamp;
    public float yRightClamp;
    // Update is called once per frame
    void Update()
    {
        //Checks if bounds of camera are in still within the scence
        transform.position = new Vector3(Mathf.Clamp(locationOfPlayer.position.x, xLeftClamp, xRightClamp), 
           Mathf.Clamp(locationOfPlayer.position.y, yLeftClamp, yRightClamp), 
           transform.position.z);
    }
}
