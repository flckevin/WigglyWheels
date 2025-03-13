using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ImpaleableLimbs : MonoBehaviour
{
    [HideInInspector] public quaternion _impaledRotation; // rotation to store rigi object rotation
    [HideInInspector] public Vector3 _impaledPosition; // rotation to store rigi object position
    [HideInInspector] public Rigidbody _limbRigi; //rigibody of limb it self

    void Awake()
    {
        //enable the limb impaleable identifier
        GetComponent<LimbsBehaviour>()._impaleAble = true;
        //we have to disable this to save performance we only want to enable it
        if (this.enabled == true) { this.enabled = false; }
    }

    void FixedUpdate()
    {
        //move limb to be at impableable last position
        _limbRigi.MovePosition(_impaledPosition);
        //rotate limb to be at impableable last rotation
        _limbRigi.MoveRotation(_impaledRotation);
    }

}
