using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetToFollow; // transform of target for cam to follow
    public float smoothSpeed;
    public Vector3 camLookOffset;
    private void LateUpdate()
    {
        Vector3 desiredPos = new Vector3(targetToFollow.position.x, this.transform.position.y, this.transform.position.z);
        //Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = desiredPos;

    }

}
