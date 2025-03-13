using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDismembermentOnTrigger : MonoBehaviour
{
    public bool onlyCharacter = true; // identify whether trigger only for character
    public Transform objToCompare; // object to compare
    public float maxDistance; // max distance

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Vector3.Distance(GameManager.Instance.character.transform.position, objToCompare.position) > maxDistance) return;

        //if limb touch trigger
        if (collision.CompareTag("Limb"))
        {
            //get limb behaviour
            LimbsBehaviour _limb = collision.GetComponent<LimbsBehaviour>();
            //if limb behaviour does exist
            if (_limb != null)
            {
                //break the limb
                collision.GetComponent<LimbsBehaviour>().BreakLimbs();
            }

        }

        //if vehicle touch trigger and trigger is also for vehicle
        if (collision.CompareTag("Vehicle") && onlyCharacter == false)
        {
            //add force to the vehicle
            GameManager.Instance.vehicleRigidbody.AddForce(Vector2.up * 500, ForceMode2D.Impulse);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(Vector3.Distance(GameManager.Instance.character.transform.position, objToCompare.position));
        if (Vector3.Distance(GameManager.Instance.character.transform.position, objToCompare.position) > maxDistance) return;

        //if collision has limb tag
        if (collision.CompareTag("Limb"))
        {
            //get limb behaviour
            LimbsBehaviour _limb = collision.GetComponent<LimbsBehaviour>();
            //if limb does exist
            if (_limb != null)
            {
                //call break limb
                _limb.BreakLimbs();
            }

        }

        //if collision is vehicle
        if (collision.CompareTag("Vehicle") && onlyCharacter == false)
        {
            //add force to the vehicle
            GameManager.Instance.vehicleRigidbody.AddForce(Vector2.up * 30, ForceMode2D.Impulse);

        }
    }
}
