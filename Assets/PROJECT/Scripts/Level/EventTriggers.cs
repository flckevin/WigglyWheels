using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventTriggers : MonoBehaviour
{
    public UnityEvent onTriggerEntertEvents; //Event for ontrigger enter
    public UnityEvent onTriggerExitEvents;   //Event for ontrigger exit
    public string tagKey = "Vehicle";

    #region 2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player enters trigger
        if (collision.CompareTag(tagKey))
        {
            //call event for ontrigger enter
            onTriggerEntertEvents.Invoke();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //player exit trigger
        if (collision.CompareTag(tagKey))
        {
            //call event for ontrigger exit
            onTriggerExitEvents.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //player enters trigger
        if (collision.gameObject.CompareTag(tagKey))
        {
            //call event for ontrigger enter
            onTriggerEntertEvents.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //player exit trigger
        if (collision.gameObject.CompareTag(tagKey))
        {
            //call event for ontrigger exit
            onTriggerExitEvents.Invoke();
        }
    }
    #endregion



    #region 3D

    private void OnTriggerEnter(Collider other)
    {
        //player enters trigger
        if (other.CompareTag(tagKey))
        {
            //call event for ontrigger enter
            onTriggerEntertEvents.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //player exit trigger
        if (other.CompareTag(tagKey))
        {
            //call event for ontrigger exit
            onTriggerExitEvents.Invoke();
        }
    }

    #endregion
}
