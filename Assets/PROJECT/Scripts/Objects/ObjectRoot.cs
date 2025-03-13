using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectRoot : MonoBehaviour
{

    public abstract void ObjectInitiation(object param);
    public abstract void ObjectUpdate(object param);
    public abstract void ObjectDeactivate(object param);


    //============================ TRIGGER 2D ===========================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectInitiation(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ObjectUpdate(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ObjectDeactivate(collision);
    }
    //============================ TRIGGER 2D ===========================


    //============================ COLLISION 2D ===========================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ObjectInitiation(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ObjectDeactivate(collision);
    }
    //============================ COLLISION 2D ===========================


    //============================ TRIGGER 3D ===========================
    private void OnTriggerEnter(Collider other)
    {
        ObjectInitiation(other);
    }



}
