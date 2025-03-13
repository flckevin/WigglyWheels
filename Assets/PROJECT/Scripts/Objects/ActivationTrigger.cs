using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationTrigger : MonoBehaviour
{
    public GameObject obj;
    public bool activation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vehicle" || collision.gameObject.tag == "Player") 
        {
            obj.SetActive(activation);
        }
    }
}
