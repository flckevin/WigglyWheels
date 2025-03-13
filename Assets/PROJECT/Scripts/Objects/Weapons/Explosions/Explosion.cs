using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionForce; // force of explosion affect to object
    public float explosionRadius; //explostion radius
    public ParticleSystem explosionParticle; //explosion particle
    public int interactableMaskID = 6; // mask ID so it can reduce object when detecting

    private void Start()
    {
        //spawn explosion 
        PoolExtension.AddPool(explosionParticle, 5, "Explosion");
    }

    public virtual void ExplosionActivation(object param)
    {
        //GameObject obj = param as GameObject;

        //get particle system from pool
        ParticleSystem explosion = (ParticleSystem)PoolExtension.GetPoolDict("Explosion");
        //set position of explosion to be at the explosion object
        explosion.transform.position = this.transform.position;
        //play particle
        explosion.Play();

        //get all objects that overlap our circle
        Collider2D[] _2DCol = Physics2D.OverlapCircleAll(this.transform.position, explosionRadius);

        //loop all objects in range
        for (int i = 0; i < _2DCol.Length - 1; i++)
        {

            //if the object has tag "Limb"
            if (_2DCol[i].gameObject.tag == "Limb")
            {
                //get limb behaviour class
                LimbContact _limbC = _2DCol[i].GetComponent<LimbContact>();
                //if limb behaviour does exist
                if (_limbC != null)
                {
                    Vector3 _driect = new Vector3((_2DCol[i].transform.position.x - this.transform.position.x) * (GameManager.Instance.vehicleScript._direction * -1) * explosionForce,
                                                      _2DCol[i].transform.position.y * explosionForce,
                                                        this.gameObject.transform.position.z);
                    //de attach the limb THE REASON WE SET DISCONNECT TO FALSE IS BECAUSE WE WANT TO DISCONNECT LIMB EACH BY EACH FIRST THEN ADD FORCE TO BODY MANUALLY LATER
                    _limbC.BreakLimb(false, true, false, explosionForce, ForceMode.Force, _driect);
                    //Debug.Log("HERE " + _driect);


                    //we break here so we can find the correct limb with limb contact so we can always
                    //break our limbs when hit a mine
                    break;
                }



            }

        }

        //if player not alive then execute these 
        if (GameManager.Instance.characterBehaviour.alive == false)
        {
            //correct position to add force    // POSITION                                                                      // DIRECTION                                           //FORCE
            Vector3 _directRigi = new Vector3((GameManager.Instance.centerOfCharacter.position.x - this.transform.position.x) * (GameManager.Instance.vehicleScript._direction * -1) * (explosionForce * (GameManager.Instance.characterBehaviour.totalMass * 20)),
                                            //POSITION                                           //FORCE
                                            GameManager.Instance.centerOfCharacter.position.y * (explosionForce * (GameManager.Instance.characterBehaviour.totalMass * 20)),
                                            //POSITION
                                            this.gameObject.transform.position.z);
            //add force to limb

            //WHY TF DOES IT NULLL WTFFFFFFFF
            //EventDispatcherExtension.FireEvent(EventID.OnDisconnect, _directRigi);

            //GameManager.Instance.characterBehaviour.DisconnectVechicle(_directRigi);


            //Debug.Log("EXPLOSION: " + _driectRigi);

            //add force to body
            GameManager.Instance.centerOfCharacter.AddForce(_directRigi, ForceMode.Force);
        }



        //deactivate game object
        this.gameObject.SetActive(false);

    }

    public virtual void Explosion3D()
    {
        //GameObject obj = param as GameObject;

        //get particle system from pool
        ParticleSystem explosion = (ParticleSystem)PoolExtension.GetPoolDict("Explosion");
        //set position of explosion to be at the explosion object
        explosion.transform.position = this.transform.position;
        //play particle
        explosion.Play();

        //get all objects that overlap our circle
        Collider[] _Col = Physics.OverlapSphere(this.transform.position, explosionRadius);

        //loop all objects in range
        for (int i = 0; i < _Col.Length - 1; i++)
        {

            //if the object has tag "Limb"
            if (_Col[i].gameObject.tag == "Limb")
            {
                //get limb behaviour class
                LimbContact _limbC = _Col[i].GetComponent<LimbContact>();
                //if limb behaviour does exist
                if (_limbC != null)
                {
                    Vector3 _driect = new Vector3((_Col[i].transform.position.x - this.transform.position.x) * (GameManager.Instance.vehicleScript._direction * -1) * explosionForce,
                                                      _Col[i].transform.position.y * explosionForce,
                                                        this.gameObject.transform.position.z);
                    //de attach the limb THE REASON WE SET DISCONNECT TO FALSE IS BECAUSE WE WANT TO DISCONNECT LIMB EACH BY EACH FIRST THEN ADD FORCE TO BODY MANUALLY LATER
                    _limbC.BreakLimb(false, true, false, explosionForce, ForceMode.Force, _driect);
                    //Debug.Log("HERE " + _driect);


                    //we break here so we can find the correct limb with limb contact so we can always
                    //break our limbs when hit a mine
                    break;
                }



            }

        }

        //setting correct position to add force
        Vector3 _driectRigi = new Vector3((GameManager.Instance.centerOfCharacter.position.x - this.transform.position.x) * (GameManager.Instance.vehicleScript._direction * -1) * explosionForce,
                                             GameManager.Instance.centerOfCharacter.position.y * explosionForce,
                                                        this.gameObject.transform.position.z);

        //add force to limb
        GameManager.Instance.centerOfCharacter.AddForce(_driectRigi * (explosionForce * (GameManager.Instance.characterBehaviour.totalMass * 2)), ForceMode.Force);

        //deactivate game object
        this.gameObject.SetActive(false);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, explosionRadius);
    }
#endif

}
