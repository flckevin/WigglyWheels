using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismemberOnTrigger : MonoBehaviour
{
    public bool onlyCharacter = true; // identify whether trigger only for character
    public float forceToAdd = 2; // force to add to dismembermed limbs

    private void OnTriggerEnter(Collider collision)
    {
        //if limb touch trigger
        if (collision.CompareTag("Limb"))
        {
            //get limb behaviour
            LimbsBehaviour _limb = collision.GetComponent<LimbsBehaviour>();
            //if limb behaviour does exist
            if (_limb != null)
            {
                //break the limb
                collision.GetComponent<LimbsBehaviour>().BreakLimbs(false,
                                                                    true,
                                                                    forceToAdd,
                                                                    ForceMode.Force,
                                                                    new Vector3((collision.transform.position.x - this.transform.position.x) * (GameManager.Instance.vehicleScript._direction * -1) * forceToAdd,
                                                                                (this.transform.position.y - collision.transform.position.y) * forceToAdd,
                                                                                collision.gameObject.transform.position.z));
            }

        }

        //if vehicle touch trigger and trigger is also for vehicle
        if (collision.CompareTag("Vehicle") && onlyCharacter == false)
        {
            //add force to the vehicle
            GameManager.Instance.vehicleRigidbody.AddForce(Vector2.up * 500, ForceMode2D.Impulse);

        }
    }

    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     if (collision.tag == "Limb")
    //     {
    //         LimbsBehaviour _limb = collision.GetComponent<LimbsBehaviour>();
    //         if (_limb != null)
    //         {
    //             collision.GetComponent<LimbsBehaviour>().BreakLimbs();
    //         }

    //     }

    //     if (collision.tag == "Vehicle" && onlyCharacter == false)
    //     {
    //         GameManager.Instance.vehicleRigidbody.AddForce(Vector2.up * 30, ForceMode2D.Impulse);

    //     }
    // }

}
