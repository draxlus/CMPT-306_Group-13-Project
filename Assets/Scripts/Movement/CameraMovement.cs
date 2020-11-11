using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform targetObject;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //if the current camera position and the target position do not equal
        if(transform.position != targetObject.position)
        {
            //locate the position of the target
            Vector3 targetPosition = new Vector3(targetObject.position.x, targetObject.position.y, transform.position.z);
            //set current position of the camera to be a particular point between target position and camera position 
            //this point is decided by speed, the lower the number, the closer the camera gets to the object
            transform.position = Vector3.Lerp(transform.position, targetPosition, distance);
        }
        
    }
}
