using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if player collider with other game object
        if (collision.CompareTag("Player") || collision.CompareTag("Vehicle"))
        {
            Debug.Log(collision.transform.name);
            //activate game menu
            GameManager.Instance.gameMenu.WinActivation();

        }

    }
}
