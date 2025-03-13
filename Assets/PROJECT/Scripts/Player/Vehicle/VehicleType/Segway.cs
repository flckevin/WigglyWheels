using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Segway : VehicleScript
{

    [Header("SEGWAY INFO"), Space(20)]
    public float jumpForce; // jump force for vehicle


    public override void Start()
    {

        //mark as vehicle has special ability
        hasSpecialAbility = true;
        //mark as it cannot rotate
        ableToRotate = false;


        base.Start();


        if (SceneManager.GetActiveScene().buildIndex == 0) return;
        //activate special ability button
        GameManager.Instance.specialAbility.gameObject.SetActive(true);

    }

    public override void SpecialAbility()
    {
        //there's nothing overlap vehicle
        if (onGround == false) return;
        //segway jump
        _vehicleRigi.AddForce(Vector2.up * jumpForce);
    }
}
