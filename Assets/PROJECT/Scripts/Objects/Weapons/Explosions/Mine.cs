using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Explosion
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.CompareTag("Vehicle"))
        {
            //eplode
            ExplosionActivation(collision.gameObject);

            //get rigibody component
            Rigidbody2D _objRigi = collision.GetComponent<Rigidbody2D>();
            //if the rigibody does exist
            if (_objRigi != null)
            {
                //Debug.Log("Addforce vehicle");
                //add force into it
                _objRigi.AddForce(Vector2.up * explosionForce * _objRigi.mass, ForceMode2D.Impulse);
            }

        }
    }

}
