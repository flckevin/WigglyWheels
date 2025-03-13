using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class VehicleScript : MonoBehaviour
{
    [Header("VEHICLE INFO"), Space(20)]
    public WheelJoint2D[] wheels; //all vehicle wheels
    public float enginePower;// engine power aka maximum value that wheel speed can reach to
    public float thrustPower;//amount to increase speed;
    public float thrustPowerOnAir;//amount to thrust while on air
    public int thrustPowerMultiplier = 1;//thrust force multiply
    public float brakeConstrainVal; // break constrain value
    //[Range(1, 0)] public float brakeConstrainMeter = 0.3f;//value to tell vehicle when to apply brake constrain
    public Rigidbody2D _vehicleRigi; //vehicle rigibody
    public float rotateForce;//force to rotate vehicle
    public GameObject vehicleBody; // vehicle body
    public Transform groundCheckPos; // postiion to check ground
    public LayerMask mask; // mask to check
    public bool onGround; // identify whether player vehicle on land
    [HideInInspector] public JointMotor2D _motor2D; // need to declare another jointmotor2d to set speed for wheel joint

    [HideInInspector] public int _direction = 1; //identify which direction vehicle going to be heading at || left or right -1 -> left 1 -> right ||
    private int currentDirection = 1; //store our current vehicle direction 
    [HideInInspector] public int _rotateDirection = 1;//rotate direction


    [Space(10)]
    [Header("ANIMATIONS_ONLY IF IT HAS IT"), Space(20)]
    public Animator blendAnims; // animator type of blend
    public string animName; // name of animation to play
    public float maxAnimationSpeed; // maximum of animation speed that able to reach


    [Space(10)]
    [Header("CONTROL"), Space(20)]
    public bool ableToRotate = true; // identify whether vehicle able to rotate
    public bool hasSpecialAbility = false;// identify whether vehicle able to use their ability


    [Space(10)]
    [Header("CHARACTER INFO"), Space(20)]
    public Transform characterRoot;

    //========================================== PRIVATE / PROTECTED ================================================


    //---------- VEHICLE VALUES -------------
    protected int _vehicleLastValue; // store last value so we can detect vehicle speed changed to apply break faster
    //---------- VEHICLE VALUES -------------

    //---------- VEHICLE ABILITY -------------
    [HideInInspector] public bool _ableToMove;//bool to idenetify whether to move the vehicle
    [HideInInspector] public bool _ableToRotate;// identify whether vehicle able to rotate
    //---------- VEHICLE ABILITY -------------



    //========================================== PRIVATE / PROTECTED ================================================

    private void Awake()
    {
        //assaigning vehicle script
        GameManager.Instance.vehicleScript = this;
        //assigning vehicle rigibody
        GameManager.Instance.vehicleRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        //assigning vehicle transform
        GameManager.Instance.vehicle = vehicleBody.transform;

        //store current vehicle into game manager
        //GameManager.Instance.currentVechicle = this;

    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        //****************** SPECIAL CASES **************************

        //if the bike in main menu, deactivate controller script
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //deactivate script
            this.enabled = false;
            return;
        }

        //****************** SPECIAL CASES **************************


        //************************ GET *************************************

        _vehicleRigi = GetComponent<Rigidbody2D>();

        //************************ GET *************************************


        //************************** SET *************************************
        //add event to event dispatcher
        //EventDispatcherExtension.RegisterListener(EventID.OnVehicleHitThreeDimen, (p) => OnThreeDimenObjHit((object[])p));

        //set vehicle mass in center
        _vehicleRigi.centerOfMass = new Vector3(0, -0.5f, 0.3f);

        //need to get maximum motor torque or vehicle cannot add force
        //can be any wheels in array
        _motor2D.maxMotorTorque = wheels[0].motor.maxMotorTorque;

        //making sure thrust multiplier always 1
        if (thrustPowerMultiplier <= 0) { thrustPowerMultiplier = 1; }

        //************************** SET *************************************


        #region button assigning

        //================== VEHICLE THRUST ====================

        //assign able to move vehicle
        EventTriggerAssigner(GameManager.Instance.thrust,
                            () => { OnMoveVehicle(true); },
                            EventTriggerType.PointerDown);
        //assign vehicle direction changer
        EventTriggerAssigner(GameManager.Instance.thrust,
                            () => { VehicleDirectionChanger(-1); },
                            EventTriggerType.PointerDown);
        //assign function to stop vehicle
        EventTriggerAssigner(GameManager.Instance.thrust,
                            () => { OnMoveVehicle(false); },
                            EventTriggerType.PointerUp);
        //================== VEHICLE THRUST ====================

        //================== VEHICLE REVERSE ====================

        //assign able to move vehicle
        EventTriggerAssigner(GameManager.Instance.reverse,
                            () => { OnMoveVehicle(true); },
                            EventTriggerType.PointerDown);
        //assign vehicle direction changer
        EventTriggerAssigner(GameManager.Instance.reverse,
                            () => { VehicleDirectionChanger(1); },
                            EventTriggerType.PointerDown);
        //assign function to stop vehicle
        EventTriggerAssigner(GameManager.Instance.reverse,
                            () => { OnMoveVehicle(false); },
                            EventTriggerType.PointerUp);
        //================== VEHICLE REVERSE ====================




        //================== LEAN ====================

        //vehicle can lean
        if (ableToRotate == true)
        {
            //================== VEHICLE LEAN LEFT ====================

            //assign able to move vehicle
            EventTriggerAssigner(GameManager.Instance.leanLeft,
                                () => { OnRotateVehicle(true); },
                                EventTriggerType.PointerDown);
            //assign vehicle direction changer
            EventTriggerAssigner(GameManager.Instance.leanLeft,
                                () => { RotateDirectionChanger(-1); },
                                EventTriggerType.PointerDown);
            //assign function to stop vehicle
            EventTriggerAssigner(GameManager.Instance.leanLeft,
                                () => { OnRotateVehicle(false); },
                                EventTriggerType.PointerUp);

            //================== VEHICLE LEAN LEFT ====================

            //================== VEHICLE LEAN RIGHT ====================

            //assign able to move vehicle
            EventTriggerAssigner(GameManager.Instance.leanRight,
                                () => { OnRotateVehicle(true); },
                                EventTriggerType.PointerDown);
            //assign vehicle direction changer
            EventTriggerAssigner(GameManager.Instance.leanRight,
                                () => { RotateDirectionChanger(1); },
                                EventTriggerType.PointerDown);
            //assign function to stop vehicle
            EventTriggerAssigner(GameManager.Instance.leanRight,
                                () => { OnRotateVehicle(false); },
                                EventTriggerType.PointerUp);
            //================== VEHICLE LEAN RIGHT ====================
        }
        else // vehicle cannot lean
        {
            //deactivate lean right and left
            GameManager.Instance.leanLeft.gameObject.SetActive(false);
            GameManager.Instance.leanRight.gameObject.SetActive(false);
        }

        //================== LEAN ====================



        //================== SPEICAL ABILITY ====================

        //if vehicle has special ability
        if (hasSpecialAbility == true)
        {
            //assign special ability function to vehicle
            //GameManager.Instance.specialAbility.onClick.AddListener(() => { SpecialAbility(); });

            EventTriggerAssigner(GameManager.Instance.specialAbility,
                                () => { SpecialAbility(); },
                                EventTriggerType.PointerDown);
        }
        else // vehicle does not have special ability
        {
            //remove special ability button
            GameManager.Instance.specialAbility.gameObject.SetActive(false);
        }

        //================== SPEICAL ABILITY ====================
        #endregion

    }

    protected virtual void Update()
    {
        DistanceChecker();
    }


    protected virtual void FixedUpdate()
    {
        VehicleMovement();
    }

    /// <summary>
    /// function to check distance
    /// </summary>
    private void DistanceChecker()
    {
        //if ground checker does not exist then dont execute code to cause error
        if (groundCheckPos == null) return;
        //check if anything overlap the vehicle
        onGround = Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, mask);

    }

    /// <summary>
    /// Vehicle movement logic
    /// </summary>
    private void VehicleMovement()
    {

        //if vehicle able to rotate
        if (_ableToRotate == true)
        {
            //rotate vehicle
            _vehicleRigi.MoveRotation(_vehicleRigi.rotation + (_rotateDirection * rotateForce * Time.fixedDeltaTime));

        }

        //_vehicleRigi.rotation += _rotateDirection;
        //if car able to move
        if (_ableToMove == true)
        {
            //play animation of vehicle
            AnimationPlay();

            //if the vehicle is on air
            if (onGround == false)
            {
                //Debug.Log("MOVING ON AIR, " + _direction);
                //add force to vehicle while it on air
                _vehicleRigi.AddForce(new Vector2((-thrustPowerOnAir * _direction) * Time.fixedDeltaTime, 0), ForceMode2D.Force);
            }

            //if current direction that vehicle at is not same as new direction
            if (currentDirection != _direction)
            {
                //brake the vehicle
                _motor2D.motorSpeed = 0;
                //set new direction
                currentDirection = _direction;
                //apply force to vehicle
                ApplyForce();
            }


            //if motor reach to maximum value then stop increasement
            if (Mathf.Abs(_motor2D.motorSpeed) >= enginePower)
            {
                _motor2D.motorSpeed = enginePower * _direction;
            }
            else
            {
                //not reach enough
                //keep increase
                _motor2D.motorSpeed += thrustPower * thrustPowerMultiplier * _direction * Time.fixedDeltaTime;
            }


            //Debug.Log(_motor2D.motorSpeed);
            //apply force to wheels
            ApplyForce();

        }
        else // not able to move
        {

            //check whether car speed has stop
            if (_motor2D.motorSpeed == 0) return;
            //Debug.Log("DECREASING | THRUST POWER: " + thrustPower);
            if (_motor2D.motorSpeed > 0)
            {

                //keep decrease until it reaches to 0
                _motor2D.motorSpeed -= thrustPower / 2 * Time.fixedDeltaTime;
                if (_motor2D.motorSpeed < 1) { _motor2D.motorSpeed = 0; }
            }
            else
            {

                //keep decrease until it reaches to 0
                _motor2D.motorSpeed += thrustPower / 2 * Time.fixedDeltaTime;
                if (_motor2D.motorSpeed > -1) { _motor2D.motorSpeed = 0; }
            }

            //play animation
            AnimationPlay();

            //if blend animation does exist
            if (blendAnims != null)
            {
                //set animation speed back to default
                blendAnims.speed = 1;
            }
            //Debug.Log(thrustPower);

            //apply force to wheels
            ApplyForce();

        }

    }

    #region button functions

    /// <summary>
    /// function to move vehicle through UI button on screen
    /// </summary>
    /// <param name="_move"> identifier whether to move </param>
    public void OnMoveVehicle(bool _move)
    {
        //set vehicle able to move
        _ableToMove = _move;

    }

    /// <summary>
    /// function to change vehicle direction
    /// </summary>
    public void VehicleDirectionChanger(int _newDirect)
    {
        //set new direction
        _direction = _newDirect;

    }


    /// <summary>
    /// function to rotate vehicle
    /// </summary>
    /// <param name="_rotate"> identify whether to rotate </param>
    public void OnRotateVehicle(bool _rotate)
    {
        //Debug.Log(_rotate);
        //rotate
        _ableToRotate = _rotate;

    }

    /// <summary>
    /// functiont to change rotate direction
    /// </summary>
    /// <param name="_newDirect"> direction to rotate </param>
    public void RotateDirectionChanger(int _newDirect)
    {
        //change rotate direction
        _rotateDirection = _newDirect;

    }

    //public void AddForceAtAngle(float _desiredAngle) 
    //{ 

    //    float x = Mathf.Cos(_desiredAngle * Mathf.PI/180)*force;
    //    float y = Mathf.Sin(_desiredAngle * Mathf.PI / 180) * force;

    //    _vehicleRigi.AddForce(new Vector2(y, x));
    //}


    /// <summary>
    /// function to assign function to event trigger
    /// </summary>
    /// <param name="_eventTrig"> our target that has event trigger component </param>
    /// <param name="_func"> function to assign </param>
    /// <param name="_type"> type of event trigger </param>
    private void EventTriggerAssigner(EventTrigger _eventTrig, Action _func, EventTriggerType _type)
    {
        //create entry for event trigger
        EventTrigger.Entry _event = new EventTrigger.Entry()
        {
            //getting correct type
            eventID = _type
        };

        //assigning function to event trigger
        _event.callback.AddListener((f) => { _func(); });
        //adding every info into our event trigger
        _eventTrig.triggers.Add(_event);

    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        //if vehicle reached to finish line and player still alive
        if (collision.gameObject.tag == "Finishline")
        {
            //if (GameManager.Instance.characterBehaviour.alive == false) return;
            //activate win menu
            GameManager.Instance.gameMenu.WinActivation();
        }

        if (collision.gameObject.tag == "Accident")
        {
            Debug.Log("CRASH");
            _vehicleRigi.AddForce(Vector2.up * 3000);

        }
    }


    // public void OnThreeDimenObjHit(object[] _param)
    // {
    //     //get all required component
    //     float _force = (float)_param[0];
    //     //force
    //     Vector2 _direct = (Vector2)_param[1];

    //     _vehicleRigi.AddForce(_direct * (_force * (_vehicleRigi.mass * 2)));
    // }

    /// <summary>
    /// function to apply thrust force to wheels
    /// </summary>
    private void ApplyForce()
    {
        //set all wheel speed
        wheels[0].motor = _motor2D;
    }


    /// <summary>
    /// function to decide whether to apply brake constrain
    /// </summary>
    /// <param name="_val"> speed value </param>
    /// <returns></returns>
    // private float breakConstrain(int _val)
    // {
    //     //value to check whether we need to apply break constrain
    //     float _breakVal = Mathf.Abs(_val / enginePower);

    //     //if we have not reached to the value that we wanted
    //     if (_breakVal < brakeConstrainMeter)
    //     {
    //         //apply break constrain
    //         return brakeConstrainVal * _direction;
    //     }
    //     else
    //     {
    //         return 1;
    //     }


    // }

    public virtual void AnimationPlay() { }

    public virtual void SpecialAbility() { }
}
