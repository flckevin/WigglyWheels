#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPCControll : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        InputVehicle();

    }

    void InputVehicle()
    {

        if (Input.GetKey(KeyCode.D))
        {
            GameManager.Instance.vehicleScript._ableToMove = true;
            GameManager.Instance.vehicleScript._direction = -1;
        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            GameManager.Instance.vehicleScript._ableToMove = true;
            GameManager.Instance.vehicleScript._direction = 1;
        }
        else
        {
            GameManager.Instance.vehicleScript._ableToMove = false;
        }

        if (Input.GetKey(KeyCode.K))
        {
            GameManager.Instance.vehicleScript._ableToRotate = true;
            GameManager.Instance.vehicleScript._rotateDirection = -1;
        }

        if (Input.GetKey(KeyCode.J))
        {
            GameManager.Instance.vehicleScript._ableToRotate = true;
            GameManager.Instance.vehicleScript._rotateDirection = 1;
        }
        else
        {
            GameManager.Instance.vehicleScript._ableToRotate = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.vehicleScript.SpecialAbility();
        }
    }
}

#endif