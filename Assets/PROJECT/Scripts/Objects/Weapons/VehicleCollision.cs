using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCollision : MonoBehaviour
{
    public float vehicleHitForce;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Limb"))
        {
            //set default direction
            Vector3 _directToAddForce = new Vector3((this.transform.position.x - other.transform.position.x) * (GameManager.Instance.vehicleScript._direction * -1 * vehicleHitForce),
                                   this.transform.position.y * vehicleHitForce,
                                   this.gameObject.transform.position.z);

            GameManager.Instance.characterBehaviour.DisconnectVechicle(false);

            LimbsBehaviour _limb = other.GetComponent<LimbsBehaviour>();

            if (_limb != null)
            {

                _limb.BreakLimbs(false, true, vehicleHitForce, ForceMode.Force, _directToAddForce);
            }

            GameManager.Instance.centerOfCharacter.AddForce(_directToAddForce * (vehicleHitForce * GameManager.Instance.characterBehaviour.totalMass), ForceMode.Force);

        }


    }


}
