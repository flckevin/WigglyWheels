using System.Collections;
using System.Collections.Generic;
using QuocAnh.pattern;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Cinemachine;
using Kamgam.BikeAndCharacter25D;
public class GameManager : Singleton<GameManager>
{

    [Header("PLAYER INFO"), Space(10)]

    [Header("PLAYER INFO _ CHARACTER"), Space(5)]
    [HideInInspector] public CharacterBehaviour characterBehaviour;
    [HideInInspector] public Transform character;
    [HideInInspector] public Rigidbody centerOfCharacter;
    [HideInInspector] public CharacterBones characterBone;
    [HideInInspector] public Vector3 hitPos;

    [Header("PLAYER INFO _ VEHICLE"), Space(5)]
    [HideInInspector] public Transform vehicle;
    [HideInInspector] public VehicleScript vehicleScript;
    [HideInInspector] public Rigidbody2D vehicleRigidbody;


    [Header("======================================="), Space(20)]
    [Header("CINECAM"), Space(10)]
    public CinemachineBrain cineBrain;
    public CinemachineVirtualCamera cineMachineVirtualCam;


    [Header("======================================="), Space(20)]
    [Header("UI _ GENERAL"), Space(10)]
    public GamePauseMenu gameMenu;


    [Header("======================================="), Space(20)]
    [Header("CONTROLL BUTTONS _ UI"), Space(10)]
    public EventTrigger thrust;
    public EventTrigger reverse;
    public EventTrigger leanLeft;
    public EventTrigger leanRight;
    public EventTrigger specialAbility;

    [Header("======================================="), Space(20)]
    [Header("GAME DATA"), Space(10)]
    public int frameTarget = 60;



    private void Awake()
    {
        Application.targetFrameRate = frameTarget;
    }

}
