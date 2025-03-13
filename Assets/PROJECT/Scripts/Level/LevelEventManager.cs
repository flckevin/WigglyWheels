using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEventManager : MonoBehaviour
{
    public float delayDeactivation = 5f;


    /// <summary>
    /// function to activate object
    /// </summary>
    /// <param name="_objectToActivate"> targetgame object type </param>
    public void ObjectActivation(GameObject _objectToActivate)
    {
        _objectToActivate.SetActive(true);
    }


    /// <summary>
    /// function to deactivate object
    /// </summary>
    /// <param name="_objectToActivate"> targetgame object type </param>
    public void ObjectDeactivation(GameObject _objectToActivate)
    {
        //deactivate object
        _objectToActivate.SetActive(false);
    }


    /// <summary>
    /// timed object deactivation
    /// </summary>
    /// <param name="_objectToDeactivate"> targetgame object type </param>
    public void ObjectDeactivationTimed(GameObject _objectToDeactivate)
    {
        //start couroutine of deactivate object
        StartCoroutine(ObjectActivationCou(_objectToDeactivate, false));
    }


    /// <summary>
    /// function to activate timed
    /// </summary>
    /// <param name="_objectToDeactivate"> targetgame object type </param>
    public void ObjectActivationTimed(GameObject _objectToDeactivate)
    {
        //start coroutine of activating object
        StartCoroutine(ObjectActivationCou(_objectToDeactivate, true));
    }


    /// <summary>
    /// function to activate rigibody 2d
    /// </summary>
    /// <param name="_rigi"> targetgame Rigidbody2D type </param>
    public void ActivateRigibody2D(Rigidbody2D _rigi)
    {
        //activate rigibody
        _rigi.isKinematic = false;
    }

    public void PlayDoTween(GameObject _anim)
    {
        DOTween.Play(_anim);
    }

    /// <summary>
    /// couroutine of activating object
    /// </summary>
    /// <param name="_objectDeactivate"> target gameobject type </param>
    /// <param name="activation"> boolean of decision whether to activate or deactivate </param>
    /// <returns></returns>
    IEnumerator ObjectActivationCou(GameObject _objectDeactivate, bool activation)
    {
        yield return new WaitForSeconds(delayDeactivation);
        _objectDeactivate.SetActive(activation);
    }
}
