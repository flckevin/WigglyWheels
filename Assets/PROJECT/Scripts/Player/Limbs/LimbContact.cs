using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/**********************************************
 * ABOUT LIMB CONTACT - 2D

 * Limb contact script is a script where it will receive 
 * hit collision from enviroment -> then it will send signal
 * to Limb behaviour (which hold by 3D limb) to decrease health or break limbs

 *********************************************/
public class LimbContact : MonoBehaviour
{
    public LimbsBehaviour limbBhaviour;//limb behaviour so we can call all main function from there
    public int limbID; // id of limb
    //public LimbContact requiredLimb; //limb that required to be able to hold onto vehicle
    //public bool disconnectOnBreak; // bool to check whether it going to disconnect on next break
    [HideInInspector] public LimbType limbType; // type of limb
    [HideInInspector] public float hingeJointLimitation; // maxmimum of joint limitation
    [HideInInspector] public float selfDamage; // damage cause ot itself
    [HideInInspector] public float maximumHeight;
    [HideInInspector] public LimbContact connectedLimb; // connected limb
    public Rigidbody2D rigi;

    // DELETE THIS AFTER DEBUG FINISHED

    private void Start()
    {

#if UNITY_EDITOR
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
        {
            //add our limb to debug limb storage so we can dismember them with our command
            DebugController.Instance.debug_LimbStorage.Add(this);
        }
#endif
        GameManager.Instance.hitPos = Vector3.zero;
        rigi = GetComponent<Rigidbody2D>();
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.hitPos = collision.transform.position;
        //Debug.Log(Mathf.Abs(GameManager.Instance.vehicleRigidbody.velocity.y));
        if (maximumHeight <= Mathf.Abs(GameManager.Instance.vehicleRigidbody.velocity.y))
        {
            // Debug.Log("BROKEN");
            //break limb but with wiggle limb haning on it
            limbBhaviour.BreakLimbs(false, true, collision.relativeVelocity.magnitude * 2, ForceMode.Force);
        }
        else if (collision.relativeVelocity.magnitude >= limbBhaviour.limbForceReq)
        {
            //call contact function
            ContactCall(collision);
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //if joint over limitation of rotaion, break it
        if (limbBhaviour.mainJoint.jointAngle < hingeJointLimitation * -1 || limbBhaviour.mainJoint.jointAngle > hingeJointLimitation)
        {
            //break limb but with wiggle limb haning on it
            limbBhaviour.BreakLimbs(false, true, collision.relativeVelocity.magnitude * 2, ForceMode.Force);
        }
    }

    /// <summary>
    /// function to call contacts on limb
    /// </summary>
    /// <param name="collision"> object collided </param>
    private void ContactCall(Collision2D collision)
    {


        //Debug.Log(collision.transform.name);
        // Debug.Log(this.gameObject.name);
        //if limbs collided other object with half of requiermenet to break a limb speed
        if (GameManager.Instance.vehicleScript.onGround == false && collision.relativeVelocity.magnitude >= limbBhaviour.limbForceReq - Mathf.Abs(GameManager.Instance.vehicleRigidbody.velocity.y) / 2)
        {
            //break limb but with wiggle limb haning on it
            //limbBhaviour.BreakLimbs(false, true, collision.relativeVelocity.magnitude * 2, ForceMode.Force);
            //play blood effect
            //limbBhaviour.BloodEffect(true, true, this.transform.position);
            //if player can disconnect vehicle after break then disconnect
            //if (disconnectOnBreak == true) { EventDispatcherExtension.FireEvent(EventID.OnDeath); }
            //set dismembermed to true so that it will know that state of limb has been cut off

            //deactivate object preventing of calling more than one time
            //this.gameObject.SetActive(false);

            LimbManager.CheckLimbInfo(limbType, this);
        }
        //if it only larger than limb sensitivity
        else if (collision.relativeVelocity.magnitude >= limbBhaviour.limbForceReq)
        {

            //break limb but with wiggle limb haning on it
            //limbBhaviour.BreakLimbs(false, true, collision.relativeVelocity.magnitude * 2, ForceMode.Force);
            //play blood effect
            //limbBhaviour.BloodEffect(true, true, this.transform.position);
            //if player can disconnect vehicle after break then disconnect
            //if (disconnectOnBreak == true) { EventDispatcherExtension.FireEvent(EventID.OnDeath); }
            //set dismembermed to true so that it will know that state of limb has been cut off

            //deactivate object preventing of calling more than one time
            //this.gameObject.SetActive(false);
            GameManager.Instance.hitPos = collision.transform.position;
            LimbManager.CheckLimbInfo(limbType, this);

        }
    }


    /// <summary>
    /// function to break limbs directly
    /// - we would usually use this function for traps
    /// </summary>
    /// <param name="_wiggleLimb"> [not using it] </param>
    /// <param name="_spawnLimb"> whether to spawn limb </param>
    /// <param name="_forceToLimb"> force to apply to dismemebermeted limbs </param>
    /// <param name="_limbForceMode"> force mode </param>
    public void BreakLimb(bool _wiggleLimb = false, bool _spawnLimb = true, bool _autoVehicleDisconnect = true, float _forceToLimb = 6, ForceMode _limbForceMode = ForceMode.Force, Vector3? _direction = null)
    {
        //Debug.Log(_force);

        //declare vector 3 to set direction for limbs to add force
        Vector3 _directToAddForce;

        //if there is no custom direction
        if (_direction == null)
        {
            //set default direction
            _directToAddForce = new Vector3((this.transform.position.x - GameManager.Instance.hitPos.x) * (GameManager.Instance.vehicleScript._direction * _forceToLimb),
                                            (this.transform.position.y - GameManager.Instance.hitPos.y) * _forceToLimb,
                                            this.gameObject.transform.position.z);
            // Debug.Log("LIMB NULL");
        }
        else // there is given custom direction
        {
            //set to be that custom direction
            _directToAddForce = (Vector3)_direction;
            //Debug.Log(_directToAddForce);
        }

        //Debug.Log(_force);
        //add dismembermed limb into character behaviour so it can remember which limb just got dismemebered
        //GameManager.Instance.characterBehaviour.dismembermedLimbs.Add(this);
        //break limb
        limbBhaviour.BreakLimbs(_wiggleLimb, _spawnLimb, _forceToLimb, _limbForceMode, _directToAddForce);

        //if there is another limb connecting to this limb and that limb is active on scene
        if (connectedLimb != null && connectedLimb.gameObject.activeSelf == true)
        {
            //break it
            connectedLimb.limbBhaviour.BreakLimbs(_wiggleLimb, _spawnLimb, _forceToLimb, _limbForceMode, _directToAddForce);
        }

        //check vehicle disconnect event
        OnLimbCheckVehicleDis(_autoVehicleDisconnect);

    }

    /// <summary>
    /// function to check if the required limb has been cut off
    /// </summary>
    public void OnLimbCheckVehicleDis(bool _autoDisconnect = true)
    {

        //if dismemebered limbs not empyty
        if (GameManager.Instance.characterBehaviour.dismembermedLimbs.Count > 0)
        {

            //get the recent limbs just dismembermed
            int _id = GameManager.Instance.characterBehaviour.dismembermedLimbs.Count - 1;
            //if the dismembermed limbs are the same type of the limb as itself and the limb just dismembered is not the same as our connected limb or else we going to disconnect our vehicle 
            //                                                                      ex : upperarm_R and lowerarm_R disconnected it will disconnect our vehicle eventhough it on same side 
            if (GameManager.Instance.characterBehaviour.dismembermedLimbs[_id].limbID == this.limbID)
            {
                //if current dismembermed limb does not have connected limb or connected limb does not exist
                if (GameManager.Instance.characterBehaviour.dismembermedLimbs[_id] != connectedLimb || connectedLimb == null)
                {

                    //** WARNING : THE REASON WE DONT USE INSTANT DICONNECT IS BECAUSE OF SOME TRAP WOULD HAVE DIFFERENT 
                    //              FORCE AND ANGLE TO ADD FORCE TO THE CHARACTER BODY SO THEY WOULD ADD FORCE MANUALLY TO IT

                    //unalive player
                    GameManager.Instance.characterBehaviour.alive = false;

                    if (_autoDisconnect == true)
                    {
                        //Debug.Log("LIMB CONTACT CALLED");
                        //Debug.Log("AUTO DISCONNECT");
                        //die
                        EventDispatcherExtension.FireEvent(EventID.OnDisconnect);
                    }

                }

            }
            else // is not the same type
            {
                //add this limb into it
                GameManager.Instance.characterBehaviour.dismembermedLimbs.Add(this);
            }
        }
        else // if dismembermed limb is empty
        {

            //add into it
            GameManager.Instance.characterBehaviour.dismembermedLimbs.Add(this);
        }

        //deactivate this object
        this.gameObject.SetActive(false);
    }
}
