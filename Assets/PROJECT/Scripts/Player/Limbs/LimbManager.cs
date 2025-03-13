using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LimbManager
{
    public static Dictionary<LimbType, float> limbInfo { get; set; } = new Dictionary<LimbType, float>(); // store all info of limb's health
    public static Dictionary<LimbType, float> _limbInfoDefaultValue = new Dictionary<LimbType, float>(); // store all info of limb's default health value

    // Start is called before the first frame update
    public static void CheckLimbInfo(LimbType _limbType, LimbContact _limb)
    {

        //if limb health less than 0
        if (limbInfo[_limbType] <= 0)
        {
            //break limb
            _limb.BreakLimb(false, true, true, GameManager.Instance.vehicleRigidbody.velocity.magnitude, ForceMode.Force);
            //set limb health back to default
            limbInfo[_limbType] = _limbInfoDefaultValue[_limbType];
        }
        else // there is still health left
        {
            //decrease limb health
            limbInfo[_limbType] -= _limb.selfDamage;
            //Debug.Log(_limbType.ToString() + " " + limbInfo[_limbType]);
            _limb.limbBhaviour.BloodEffect(true, true, _limb.limbBhaviour.transform.position);
        }
    }
}

public enum LimbType
{
    Head,
    Hand_Upper,
    Hand_Lower,
    Leg_Upper,
    Leg_Lower,

}
