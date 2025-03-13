using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooBehaviour : MonoBehaviour
{
    public float speedAdjustment; // value to drag the bitochbousahbfiuwhf

    private void OnTriggerEnter(Collider other)
    {
        //slow mf bike
        GameManager.Instance.vehicleScript.thrustPower -= speedAdjustment;
    }

    void OnTriggerStay(Collider other)
    {
        //slow the bitch down
        GameManager.Instance.vehicleScript.thrustPower -= speedAdjustment;
    }

    void OnTriggerExit(Collider other)
    {
        //push the bitch out
        GameManager.Instance.vehicleScript.thrustPower += speedAdjustment;
    }
}
