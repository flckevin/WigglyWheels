using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPusher : MonoBehaviour
{
    public float reloadDelay; // reload delay 
    public float forceToPush; // force to push player
    private bool ableToShoot; // identify whether able to shoot

    private void Start()
    {
        //set to true so it can shoot from beginning
        ableToShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //push player
        Push(GameManager.Instance.vehicleRigidbody);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //push player
        //Push(GameManager.Instance.vehicleRigidbody);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //push player
        //Push(GameManager.Instance.vehicleRigidbody);
    }

    /// <summary>
    /// function to shoot player
    /// </summary>
    /// <param name="_target"> target - player </param>
    void Push(Rigidbody2D _target)
    {
        //if player able to shoot
        if (ableToShoot == false) return;
        //push player
        _target.AddForce((Vector2.up /*+ _target.velocity.normalized*/) * forceToPush, ForceMode2D.Impulse);
        //set able to shoot is false to prevent from constantly shoot player
        ableToShoot = false;
        //start delay countdown
        StartCoroutine(reload());

    }

    /// <summary>
    /// function to delay psuh
    /// </summary>
    /// <returns></returns>
    IEnumerator reload()
    {
        //wait given second
        yield return new WaitForSeconds(reloadDelay);
        //set able to shoot to true
        ableToShoot = true;
    }
}
