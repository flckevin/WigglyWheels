using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : ObjectRoot
{
    [Header("General Info")]
    public GameObject crossBow; // crosbow mesh
    public float force; // force to shoot arrow
    public Transform shootPos; // position to shoot
    public Rigidbody bulllet; // bullet
    public int amunationAmount; // amount of amunition
    public float rotateSpeed; // rotation speed
    public bool followTarget = true;

    [Header("Firing Stats"), Space(10)]
    public float fireRate; // fire rate
    public bool infiniteAmmo; // identify whether ammo is infinite


    private float time; // time have waited
    private int _currentAmmo; // current amount of ammo

    private void Start()
    {
        //create loop with anmunition amount
        for (int i = 0; i < amunationAmount; i++)
        {
            //add bullet to pool
            PoolExtension.AddPool(bulllet, amunationAmount, bulllet.name);
        }

        //********************* SET ***************************

        //set ammo amount to be sa,e as amunation amount
        _currentAmmo = amunationAmount;
    }

    public override void ObjectInitiation(object param)
    {

    }

    public override void ObjectUpdate(object param)
    {
        if (_currentAmmo <= 0 && infiniteAmmo == false) return;

        if (followTarget == true)
        {
            // Quaternion lookPos = Quaternion.LookRotation(GameManager.Instance.character.position - crossBow.transform.position);
            // //crossBow.transform.LookAt(GameManager.Instance.vehicle.transform.position);
            // crossBow.transform.rotation = Quaternion.Slerp(transform.rotation, lookPos, rotateSpeed * Time.deltaTime);
            Vector3 _target = new Vector3(GameManager.Instance.character.position.x,
                                        GameManager.Instance.character.position.y,
                                        crossBow.transform.position.z);
            crossBow.transform.LookAt(_target);
        }



        //var lookPos = GameManager.Instance.vehicle.position - crossBow.transform.position;
        //lookPos.y = 0;
        //var rotation = Quaternion.LookRotation(lookPos);
        //crossBow.transform.rotation = Quaternion.Slerp(crossBow.transform.rotation, rotation, Time.deltaTime * 2);


        //increase time of waiting
        time += Time.deltaTime;
        //set new time
        float nextFire = fireRate;
        //if we wait enough time
        //fire turret
        if (time >= nextFire)
        {
            if (infiniteAmmo == false) { _currentAmmo--; }
            //get new arrow
            Rigidbody arrow = (Rigidbody)PoolExtension.GetPoolDict("Arrow");
            //set arrow position to be at crossbow position
            arrow.transform.position = shootPos.transform.position;
            //rotate arrow so it facing correct direction
            arrow.transform.rotation = Quaternion.Euler(GameManager.Instance.character.position.x - arrow.position.x + 80, crossBow.transform.eulerAngles.y, 0);
            //activate that new arrow
            arrow.gameObject.SetActive(true);
            //addforce to it
            arrow.velocity = crossBow.transform.forward * force;
            //set time back to 0 to prevent repeatedly firing turret
            time = 0;
        }

    }

    public override void ObjectDeactivate(object param)
    {
        //set time back to 0 to prevent repeatedly firing turret
        time = 0;
    }

}
