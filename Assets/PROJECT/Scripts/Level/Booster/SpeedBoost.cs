using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float forceToAdd; // force to push
    public DirectionToPush direction; // direction to push -> | horizontal *** --- vertical
    private Vector2 _directionToPush; //vector 2 to store direction to push
    // Start is called before the first frame update
    void Start()
    {
        //check which direction to push
        switch (direction)
        {
            //if it is right
            case DirectionToPush.vertical_X:
                //push right
                _directionToPush = new Vector2(1f, 0);
                break;

            //if it up
            case DirectionToPush.horizontal_Y:
                //push up
                _directionToPush = new Vector2(0, 1f);
                break;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Vehicle")
        {
            // Debug.Log("SHIT");
            ApplyForce();
            //pushing player with given direction
            // GameManager.Instance.vehicleRigidbody.AddForce(_directionToPush * force, ForceMode2D.Impulse);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Vehicle")
        {
            //float force = extraValue();
            //pushing player with given direction
            //GameManager.Instance.vehicleRigidbody.AddForce(_directionToPush * force, ForceMode2D.Impulse);
            ApplyForce();

            // GameManager.Instance.vehicleRigidbody.velocity += Vector2.Scale(worldForceVec, new Vector2(1f, 0f));
            //pushing player with given direction
            //GameManager.Instance.vehicleRigidbody.AddForce(_directionToPush * force, ForceMode2D.Impulse);

        }

    }

    private void ApplyForce()
    {
        //float force = extraValue();
        //Getting relative force vector
        Vector3 relativeForceVec = new Vector2(GameManager.Instance.vehicleRigidbody.transform.position.x,
                                                    0f) * forceToAdd * Time.fixedDeltaTime;
        //get world force vector
        Vector3 worldForceVec = transform.TransformDirection(relativeForceVec);
        //add force to vehicle
        GameManager.Instance.vehicleRigidbody.AddForce(Vector3.Scale(worldForceVec, _directionToPush), ForceMode2D.Impulse);
        //if vehicle not heavy type then stop
        // if (GameManager.Instance.heavyVehicle == false) return;
        // //if it is
        // //add more force
        // GameManager.Instance.vehicleRigidbody.velocity += Vector2.Scale(worldForceVec, _directionToPush);
    }

    // /// <summary>
    // /// fucntion to return a suitable value for booster
    // /// </summary>
    // /// <returns></returns>
    // private float extraValue()
    // {
    //     //if direction is left or right
    //     if (direction == DirectionToPush.horizontal_Y)
    //     {
    //         //Debug.Log("left n right");
    //         //return force to add multiple with time.fixed delta time so it wont push to crazy
    //         return forceToAdd * Time.fixedDeltaTime;
    //     }
    //     else // add force up
    //     {
    //         //add directly
    //         return forceToAdd * Time.fixedDeltaTime;
    //     }




    // }

}

/// <summary>
/// all direction to push 
/// </summary>
public enum DirectionToPush
{
    horizontal_Y,
    vertical_X,

}
