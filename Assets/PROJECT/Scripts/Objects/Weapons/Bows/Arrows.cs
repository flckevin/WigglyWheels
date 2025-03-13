using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    //public Rigidbody bowRigi; // rigibody of arrow

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(collision.gameObject.name);
        if (other.tag == "Limb")
        {
            //disable box collision so it won't conflict with other
            this.GetComponent<BoxCollider>().enabled = false;
            //Debug.Log(other.gameObject.name);
            this.GetComponent<Rigidbody>().isKinematic = true;
            //parent to target
            this.transform.parent = other.transform;
            //set x position to be 0 so it can be at middle of the limb so it looks better in visual
            this.gameObject.transform.localPosition = new Vector3(0, this.transform.localPosition.y, this.transform.localPosition.z);
            //particle system
            ParticleSystem blood = (ParticleSystem)PoolExtension.GetPool(PoolM.Instance.bloodSplashEffect, ref PoolM.Instance.bloodSplashID);
            //Debug.Log(PoolM.Instance.BloodSpashID++);
            //set blood position
            blood.transform.position = other.transform.position;
            //play blood
            blood.Play();
            //get blood decal
            GameObject bloodDecal = PoolExtension.GetPool(PoolM.Instance.bloodDecal, ref PoolM.Instance.bloodDecalID) as GameObject;
            //activate blood decal
            bloodDecal.SetActive(true);
            //set blood decal position
            bloodDecal.transform.position = other.transform.position;
            //set blood decal rotation
            //bloodDecal.transform.rotation = bloodFlowEffectPrefab.transform.rotation;
            //parent blood decal to limb just got dismemeberment
            bloodDecal.transform.parent = other.transform.parent;

        }
        else // no target detected
        {
            //deactivate it
            this.gameObject.SetActive(false);
        }
    }
}
