using Kamgam.BikeAndCharacter25D.Helpers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Obi;

/**********************************************
 * THIS IS A FUCKING MESSY SYSTEM SO READ ME BEFORE MODIFY
 * 
 * =========================== BRIEF ABOUT LIMB CONCEPT ===========================
 * FOR RIGIBODY 2D
 * We going to use it for:
 *
 * _ detecting incoming event which object going to be hit our limbs
 * after that we sent signal to break limbs in 3D
 *
 * _ connecting our player to the bike
 * 
 * FOR RIGIBODY 3D
 * we are going to use it for 
 * _ Breaking our limbs
 * _ Receving decals
 * _ Sticking weapon ammunitions such as arrows or harpoon to our limbs
 * 
 * mixing between them would be better idea since it will create a good ragdoll feeling when usuing puppet theory
 * =========================== BRIEF ABOUT LIMB CONCEPT ===========================
 *
 * =========================== ABOUT ========================
 * the limb behaviour script is a script where it contains behaviour of a limb it self
 * from dismemberment to receiving contact from 3D enviroment such as traps
 * it also receive signals from limb contact script
 * =========================== ABOUT ========================
 *********************************************/

public class LimbsBehaviour : MonoBehaviour
{
    //======================= LIMBS INFO =======================================
    [Space(10)]
    [Header("LIMBS INFO"), Space(5)]

    public float limbForceReq = 3; // force to break current limb
    //public Joint2D connectAbleLimb; // joints to connect other object to itself
    public bool instantDeath = false; // identify whether limb break will instantly die

    // public int limbDamageAmount = 1; // the damage that will cause to host if it break

    public GameObject[] extraAccessory; // glass or armour
    //======================= LIMBS INFO =======================================


    //======================= LIMBS INFO =======================================\
    //[HideInInspector] public GameObject limbConnectorRoot; // root gameobject of nerve connection
    [HideInInspector] public Rigidbody limbRigi; // rigibody of limb itself
    [HideInInspector] public Rigidbody limbToSpawn; // limb to spawn
    [HideInInspector] public GameObject limb;//3D object of limb
    //[HideInInspector] public Transform posToSpawnUltilities_Bones;//position to spawn limbs and other stuff
    [HideInInspector] public LimbsBehaviour connectedLimb; // connected limb
    [HideInInspector] public ParticleSystem bloodFlowEffectPrefab; // bloodflow particle system
    [HideInInspector] public HingeJoint2D mainJoint; // main joint that represent the limb itslef
    [HideInInspector] public LimbContact limbContact; // link with limbcontact so we can disable it when break our limbs
    //======================= LIMBS INFO =======================================


    //======================= NERVE INFO MAYBE IN FEAUTURE =======================================

    //======================= NERVE INFO MAYBE IN FEAUTURE =======================================

    private int _currentLimb = 0; // current limb that exist
    public bool _impaleAble = false;

    private void Awake()
    {
        //if the script enable at start then disable it
        if (this.enabled == true) { this.enabled = false; return; }
    }

    //private Rigidbody2D connected2DRig; // connected rigibody
    // Start is called before the first frame update
    void Start()
    {
        //*********************************** GET *******************************************
        //connected2DRig = this.gameObject.GetComponent<Rigidbody2D>();
        limbRigi = this.gameObject.GetComponent<Rigidbody>();
        //get impaleable limbs
        ImpaleableLimbs _impaleableLimb = this.gameObject.GetComponent<ImpaleableLimbs>();
        //*********************************** GET *******************************************

        //*********************************** SET *******************************************
        //register event dispatcher with it parameter
        //EventDispatcherExtension.RegisterListener(EventID.OnLimbHitThreeDimen, (p) => BreakLimbEventDis((object[])p));

        //storing ragdoll total mass
        GameManager.Instance.characterBehaviour.totalMass += limbRigi.mass;

        #region LIMBS

        //set gameobject tag to be limb
        this.gameObject.tag = "Limb";
        //spawn our limbs
        Rigidbody _spawnedLimbs = new Rigidbody();
        //Vector3 _limbEuler = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        // //if there is only 1 type of limb (there could be 2 types of limbs -> ex: whole limb and sliced limbs)
        // if (limbToSpawn.Length < 2)
        // {

        //     //spawn limb on that one
        //     _spawnedLimbs = Instantiate(limbToSpawn[0],
        //                                     this.transform.position,
        //                                    this.transform.rotation);
        //     //deactivate that spawned limb
        //     _spawnedLimbs.gameObject.SetActive(false);
        //     //set limb to spawn to be spawned limb
        //     limbToSpawn[0] = _spawnedLimbs;

        //     //========== DEBUG TEST =============
        //     //_spawnedLimbs.transform.parent = this.transform;

        //     //================================
        // }
        // else
        // {
        //     //loop all limbs need to be spawn
        //     for (int i = 0; i < limbToSpawn.Length; i++)
        //     {
        //         //spawn limb on that one
        //         _spawnedLimbs = Instantiate(limbToSpawn[i],
        //                                         this.transform.position,
        //                                         this.transform.rotation);
        //         //deactivate that spawned limb
        //         _spawnedLimbs.gameObject.SetActive(false);
        //         limbToSpawn[i] = _spawnedLimbs;
        //     }


        // }

        //spawn limb on that one
        _spawnedLimbs = Instantiate(limbToSpawn,
                                        this.transform.position,
                                       this.transform.rotation);
        //deactivate that spawned limb
        _spawnedLimbs.gameObject.SetActive(false);
        //set limb to spawn to be spawned limb
        limbToSpawn = _spawnedLimbs;

        #endregion


        #region IMPALEABLE LIMB

        //check if impaleable limb does exist
        if (_impaleableLimb != null)
        {
            //if it is
            //then set this limb is impaleable
            _impaleAble = true;
        }

        #endregion
        //spawn particle system at bone position
        //ParticleSystem _particleBlood = Instantiate(bloodFlowEffectPrefab,
        //                                            posToSpawnUltilities_Bones.transform.position,
        //                                            posToSpawnUltilities_Bones.transform.rotation);
        // set parent of particle system to be at bone posituion
        //_particleBlood.transform.parent = posToSpawnUltilities_Bones.parent;
        //set bloodflow effect to be spawned particle system
        //bloodFlowEffectPrefab = _particleBlood;

        //*********************************** SET *******************************************
    }

    // => FOR COLLISION SUCH AS HITTING OBJECTS
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.relativeVelocity.magnitude);
        //Debug.Log(collision.transform.name);
        //Debug.Log(collision.transform.name);
        // Debug.Log(this.gameObject.name);
        //if limbs collided other object with half of requiermenet to break a limb speed
        if (GameManager.Instance.vehicleScript.onGround == false && collision.relativeVelocity.magnitude >= limbForceReq - Mathf.Abs(GameManager.Instance.vehicleRigidbody.velocity.y) / 2)
        {
            //break limb but with wiggle limb haning on it
            BreakLimbs(false, true, collision.relativeVelocity.magnitude * 2, ForceMode.Force);

        }
        //if it only larger than limb sensitivity
        else if (collision.relativeVelocity.magnitude >= limbForceReq)
        {
            //break limb but with wiggle limb haning on it
            BreakLimbs(false, true, collision.relativeVelocity.magnitude * 2, ForceMode.Force);
        }


        // if (collision.gameObject.CompareTag("StickAble"))
        // {
        //     //set rigibody to be static so it will stay still DO NOT SET TO KINIMATIC SINCE IT WILL GOES MAD
        //     this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        //     //play blood effect
        //     BloodEffect(true, true);
        // }
    }


    // => FOR TRIGGER SUCH AS CAR HITTING
    private void OnTriggerEnter(Collider collision)
    {
        // Debug.Log(collision.tag);
        //if player into dismemberment tag
        if (collision.CompareTag("Dismember"))
        {
            //break current limb
            BreakLimbs(false, true, 5);
        }

        //if limb touched a impableObject
        if (_impaleAble == true && collision.gameObject.CompareTag("Impaleable"))
        {
            //Debug.Log(this.transform.name);
            //disconnect from vehicle
            GameManager.Instance.characterBehaviour.DisconnectVechicle(true);
            //set limb rigibody to be kinimatic
            limbRigi.isKinematic = true;
            //get impaleable limbs
            ImpaleableLimbs _impaleableLimb = this.gameObject.GetComponent<ImpaleableLimbs>();
            //set impale last position for storage
            _impaleableLimb._impaledPosition = limbRigi.position;
            //set last impale rotation for storage
            _impaleableLimb._impaledRotation = limbRigi.rotation;
            //set limb rigibody
            _impaleableLimb._limbRigi = limbRigi;
            //enable impableable limb script
            _impaleableLimb.enabled = true;
            //play blood effect
            BloodEffect(true, true, this.transform.position);
            collision.enabled = false;

        }

    }


    /// <summary>
    /// function to break limb - assigned by event dispatcher
    /// </summary>
    /// <param name="_param">first param -> limb id | second param -> hit force | third param -> direction to hit</param>
    // public void BreakLimbEventDis(object[] _param)
    // {
    //     //break all parameter and get requierment values
    //     //limb id
    //     int _id = (int)_param[0];
    //     //hit force to apply
    //     float force = (float)_param[1];
    //     //force mode
    //     ForceMode _forceMode = (ForceMode)_param[2];


    //     //if current limb does exist , limb id is not 0 which is not interactable and limb ID are the same
    //     if (_currentLimb >= 0 && _id != 0 && _id == limbID)
    //     {
    //         //break the limb
    //         BreakLimbs(false, true, force, _forceMode);
    //     }

    // }


    /// <summary>
    /// function to break limbs
    /// </summary>
    public void BreakLimbs(bool _wiggleLimb = false, bool _spawnLimb = true, float _force = 15, ForceMode _forceMode = ForceMode.Force, Vector3? _directionToAddForce = null)
    {

        //********************* GET COMMON COMPONENT *****************

        //disable collision
        Collider _limbCol = this.gameObject.GetComponent<Collider>();

        //********************* GET COMMON COMPONENT *****************


        //if (_directionAddForce == default(Vector2)) { _directionAddForce = this.transform.position; }
        //Debug.Log(_directionAddForce);
        //for connected limb
        if (_spawnLimb == false)
        {
            //deactivate limbMesh
            limb.SetActive(false);
            //deactivate this gameobject instantly
            this.enabled = false;
            //play blood effect
            BloodEffect(true, true);

            //if collider does exist then disbale it
            if (_limbCol != null) _limbCol.enabled = false;
            //do not execute others
            return;
        }

        if (limbToSpawn == null) { this.BreakLimbs(false, false); return; };
        //Debug.Log(this.gameObject.name + " || " + _currentLimb);
        #region Limbs

        //if limbs teaches to limit
        //if (limbToSpawn.Length < 1) return;


        //if the connected limb have been dismembered
        // if (limbToSpawn.Length > 1 && connectedLimb.enabled == false)
        // {
        //     //increase current limb to change limb to activate
        //     _currentLimb = 1;
        // }

        //deactivate limbMesh
        limb.SetActive(false);
        //set correct position for limb
        limbToSpawn.transform.position = this.gameObject.transform.position;
        //set rotation for limb
        limbToSpawn.transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
        //activate limb to spawn
        limbToSpawn.gameObject.SetActive(true);
        //play blood effect
        BloodEffect(true, true, limbToSpawn.transform.position);

        //if there is no custom direction
        if (_directionToAddForce == null)
        {
            //set to default direction FOR LIMB TO SPAWN
            _directionToAddForce = new Vector3((limbToSpawn.transform.position.x - this.transform.position.x) * GameManager.Instance.vehicleScript._direction * _force,
                                     (limbToSpawn.transform.position.y - this.transform.position.y) * _force,
                                     this.gameObject.transform.position.z);

        }
        //Debug.Log(_force);
        //addforce to dismembermed limbs so that looks good
        limbToSpawn.AddForce((Vector3)_directionToAddForce * _force, _forceMode);
        //Debug.Log(_directionToAddForce + " " + _forceMode);

        // if (_wiggleLimb == true)
        // {

        //     //Debug.Log(_dismemebermedLimbJoint.name);

        //     if (limbConnector != null)
        //     {

        //         limbConnector.target = limbToSpawn[_currentLimb].transform;
        //         limbConnector.gameObject.GetComponent<MeshRenderer>().enabled = true;
        //     }


        // }

        #endregion



        //===== CONNECTED LIMB STUFF ====
        //break connected limb
        //if there is connected limbs
        if (connectedLimb != null)
        {
            //break connected limb
            connectedLimb.BreakLimbs(false, true);

        }
        //===== CONNECTED LIMB STUFF ====

        //call on limb break event so that our character behaviour can check whether 
        //our character has enough limbs to ride our vehicle
        EventDispatcherExtension.FireEvent(EventID.OnLimbBreak);

        //if current limb when it hits it is instant death
        if (instantDeath == true)
        {
            //call event on die intant
            EventDispatcher.Instance.FireEvent(EventID.OnDisconnect);
        }
        //======================== JOINTS ============================

        //if there is extra accessory
        if (extraAccessory.Length > 0)
        {
            //loop all extra accessory
            for (int i = 0; i < extraAccessory.Length; i++)
            {
                //deactivate all of them
                extraAccessory[i].SetActive(false);
            }
        }

        //reduce ragdoll total mass since it already lost a limb
        GameManager.Instance.characterBehaviour.totalMass -= limbRigi.mass;
        //disable limbcontact it wont make contact with traps twice and spawn limbs twice
        limbContact.gameObject.SetActive(false);
        //if collider does exist then disbale it
        if (_limbCol != null) _limbCol.enabled = false;
        //deactivate this object
        this.enabled = false;

    }



    public void BloodEffect(bool _flowBlood, bool _decal, Vector3? _pos = null)
    {

        #region Particle system

        //if there is no position to spawn
        if (_pos == null)
        {
            //position will be set as limb position as default
            _pos = this.gameObject.transform.position;
        }

        //particle system
        ParticleSystem blood = (ParticleSystem)PoolExtension.GetPool(PoolM.Instance.bloodSplashEffect, ref PoolM.Instance.bloodSplashID);
        //Debug.Log(PoolM.Instance.BloodSpashID++);



        //set blood position
        blood.transform.position = (Vector3)_pos;
        //play blood
        blood.Play();

        #endregion

        #region Decal

        //if decal set
        if (_decal == true)
        {
            //get blood decal
            GameObject bloodDecal = PoolExtension.GetPool(PoolM.Instance.bloodDecal, ref PoolM.Instance.bloodDecalID) as GameObject;
            //activate blood decal
            bloodDecal.SetActive(true);
            //set blood decal position
            bloodDecal.transform.position = this.transform.position;
            //set blood decal rotation
            //bloodDecal.transform.rotation = bloodFlowEffectPrefab.transform.rotation;
            //parent blood decal to limb just got dismemeberment
            bloodDecal.transform.parent = GameManager.Instance.centerOfCharacter.transform.parent;
        }
        //Debug.Log(PoolM.Instance.bloodDecalID);
        #endregion

        //if blood flow set
        if (_flowBlood == true)
        {
            //======================== BLOOD FLOW =============================
            //if blood flow effect does exist
            if (bloodFlowEffectPrefab != null)
            {
                //play blood flow effect
                bloodFlowEffectPrefab.Play();
            }
            //======================== BLOOD FLOW =============================
        }
    }
}
