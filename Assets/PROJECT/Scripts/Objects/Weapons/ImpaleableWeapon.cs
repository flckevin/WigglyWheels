using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ImpaleableWeapon : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //if limb touch collider
        if (other.CompareTag("Limb"))
        {
            //get limbs behaviour
            LimbsBehaviour _limbBhaviour = other.GetComponent<LimbsBehaviour>();
            //if limb behaviour does not exist
            if (_limbBhaviour == null) return;
            //get fixed joint
            FixedJoint _joint = this.gameObject.GetComponent<FixedJoint>();
            //disconnect player from vehicle
            GameManager.Instance.characterBehaviour.DisconnectVechicle(false);
            //set joint connected body
            _joint.connectedBody = _limbBhaviour.limbRigi;
            //play blood effect
            _limbBhaviour.BloodEffect(true, true, other.transform.position);
            //disable script to prevent from assigning different limb
            this.enabled = false;

        }
    }
}
