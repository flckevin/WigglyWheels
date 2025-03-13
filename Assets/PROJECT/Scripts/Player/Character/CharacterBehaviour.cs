using System.Collections;
using System.Collections.Generic;
using Kamgam.BikeAndCharacter25D;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [Header("PLAYER GENERAL INFO _ REQUIRED TO FILL IN"), Space(20)]
    public bool alive = true; //identify whether player still alive
    public Transform targetToLook; // target to look at
    public List<LimbContact> dismembermedLimbs = new List<LimbContact>(); //limb contact for latest limb just got dismembered
    public int health = 4; // current limbs amount left

    [Space(10)]

    [Header("CHARACTER INFO"), Space(20)]

    [HideInInspector] public Joint2D[] connectedJoint; // all joints that connect to vehicles
    [HideInInspector] public Joint2D[] holderJoint; //limbs that hold whole body to vehicle
    [HideInInspector] public int _currentHolderJointIndex; // current holder joint left
    [HideInInspector] public float totalMass; // total mass of rigiobody
    [HideInInspector] public Rigidbody2D[] ragdolllimb;// all 2D ragdoll limbs to disconnect all joints that holding vehicle
    [HideInInspector] public Rigidbody[] ragdoll; // all 3D ragdoll limbs to disconnect from vehicle

    //private bool dead = false;//bool to prevent more than 1 time calling function

    private bool _disconnected = false;

    private void Awake()
    {
        //set target to follow
        GameManager.Instance.character = targetToLook;
        //assign player itself to game manager so others can also access
        GameManager.Instance.characterBehaviour = this;
        //assign player bone
        GameManager.Instance.characterBone = this.GetComponent<CharacterBones>();
        //GameManager.Instance.cineMachineVirtualCam.Follow = targetToLook;
        alive = true;
        //registering id so other that hard to access can be acess

        //event to decrease amount of limbs when limb dismember
        EventDispatcherExtension.RegisterListener(EventID.OnLimbBreak, (n) => DecreaseLimbAmount());
        //event to disconnect from vehicle
        EventDispatcherExtension.RegisterListener(EventID.OnDisconnect, (p) => DisconnectVechicle((bool)p));
        //event to disconnect from vehicle intantly
        EventDispatcherExtension.RegisterListener(EventID.OnInstantDisconnect, (p) => InstantDisconnect((bool)p));


    }



    // private void OnTriggerEnter2D(Collider2D collision)
    // {

    //     //if player collider with other game object
    //     if (collision.gameObject.tag == "Finishline")
    //     {
    //         Debug.Log(collision.transform.name);
    //         //activate game menu
    //         GameManager.Instance.pauseMenu.WinActivation();

    //     }

    // }


    /// <summary>
    /// function to force character to disconnect from vehicle
    ///  WARNING : ONLY USE THIS IF A TRAP IS GOING TO BE ISTATNLY KILLING THE PLAYER OTHERWISE USE DISCONNECT
    /// </summary>
    /// <param name="_forcePos"></param>
    public void InstantDisconnect(bool _addForce)
    {
        //Debug.Log(_forcePos);
        //unalive player
        alive = false;
        DisconnectVechicle(_addForce);
    }

    /// <summary>
    /// function to disconnect from vehicle but with safety check
    /// </summary>
    public void DisconnectVechicle(bool _addForce)
    {
        //if mickey is going to die
        if (alive == true || _disconnected == true) return;
        //loop all joints
        for (int i = 0; i < connectedJoint.Length; i++)
        {
            // Debug.Log(connectedJoint[i].name);
            //if connected joint does exist
            if (connectedJoint[i] != null)
            {
                //disconnect all of joints that connect to vehicle
                connectedJoint[i].enabled = false;
            }

        }

        //unparent character from vehicle
        this.transform.parent = null;

        //disable vehicle script
        GameManager.Instance.vehicleScript.enabled = false;

        //unparent character so it will act independently
        //GameManager.Instance.vehicleScript.characterRoot.parent = null;
        //disable character root so it wont affect 3D ragdoll
        GameManager.Instance.vehicleScript.characterRoot.gameObject.SetActive(false);
        //disable character bone - for some reason we have to assign this to game manager to be able to disable it
        //so that it wont cause null error
        GameManager.Instance.characterBone.enabled = false;

        //turn off ragdoll
        RagdollKinimaticOnOff(false);

        //======== WIP =======



        //if player did not hit anything
        if (_addForce == true)
        {
            Vector3 _finalPos;

            switch (GameManager.Instance.hitPos == Vector3.zero)
            {
                case true:
                    //add force to itslef only
                    _finalPos = new Vector3(this.transform.position.x * (GameManager.Instance.vehicleRigidbody.velocity.magnitude * (GameManager.Instance.vehicleScript._direction * -1) * totalMass),
                                                                            this.transform.position.y * (GameManager.Instance.vehicleRigidbody.velocity.magnitude * totalMass),
                                                                            this.transform.position.z);
                    break;
                case false:
                    //add force to itslef only
                    _finalPos = new Vector3(this.transform.position.x * ((GameManager.Instance.hitPos.x - this.transform.position.x) * totalMass),
                                                                            this.transform.position.y * (GameManager.Instance.vehicleRigidbody.velocity.magnitude * totalMass),
                                                                            this.transform.position.z);
                    break;
            }

            //Debug.Log("CHARACTER BEHAVIOUR: " + _forcePos);
            //add force to body
            GameManager.Instance.centerOfCharacter.AddForce(_finalPos, ForceMode.Force);
        }



        //======== WIP =======

        //set dead bool to true to prevent second time of calling
        //dead = true;

        //set time scale to be at slowmotion
        Time.timeScale = 0.3f;
        //Debug.Log("DIEEEEEEEEEEEEEEEE");
        _disconnected = true;
        //start death couroutine
        StartCoroutine(CharacterDeath());


    }

    /// <summary>
    /// function to enable and disable ragdoll
    /// </summary>
    /// <param name="_enabler"> identify whether or not to enable </param>
    public void RagdollKinimaticOnOff(bool _enabler)
    {
        //loop all limbs
        for (int i = 0; i < ragdoll.Length; i++)
        {
            //disable / enable ragdoll
            ragdoll[i].isKinematic = _enabler;
            //if player is enabling ragdoll
            if (_enabler == false)
            {
                //set ragdoll to be interlopate so it can be smooth on screen
                ragdoll[i].interpolation = RigidbodyInterpolation.Interpolate;
            }

        }


    }


    /// <summary>
    /// function to decrease amount of limb to identify whether 
    /// we have enough limb to leave the vehicle
    /// </summary>
    public void DecreaseLimbAmount()
    {
        //decrease limb
        health -= 1;
        //check if we have enough limb
        if (health <= 0)
        {
            //unalive player
            GameManager.Instance.characterBehaviour.alive = false;
            //disconnect vehicle
            DisconnectVechicle(true);
        }

        //if there is holder joint in array and one of holder joint still exist
        if (holderJoint.Length > 0 && _currentHolderJointIndex > -1)
        {
            //disable next holder joint
            holderJoint[_currentHolderJointIndex].enabled = false;
            //decrease holder joint idex
            _currentHolderJointIndex--;

        }
        //Debug.Log(_currentHolderJointIndex);
    }


    /// <summary>
    /// function to start death event
    /// </summary>
    /// <returns></returns>
    IEnumerator CharacterDeath()
    {
        //while time scale not ffr
        while (Time.timeScale > 0)
        {
            //decrease time scale time by time
            Time.timeScale -= 2 * Time.deltaTime;
            //if time scale reached to limit
            if (Time.timeScale <= 0.1f)
            {
                //freeze time
                Time.timeScale = 0;
                break;
            }
            yield return new WaitForSecondsRealtime(1f);
        }

        //call game meneu
        GameManager.Instance.gameMenu.looseMenu.SetActive(true);



    }
}
