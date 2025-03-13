using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseTesting : MonoBehaviour
{
    [Header("3D Bones_TEMPLATE FOR CHARACTER"), Space(20)]
    public Transform boneParentHolder;
    public Transform headBone;
    public Transform torsoBone;
    public Transform hipBone;


    [Header("3D Bones_R_TEMPLATE FOR CHARACTER"), Space(10)]

    public Transform upperArmBone_R;
    public Transform lowerArmBone_R;
    public Transform upperLegBone_R;
    public Transform lowerLegBone_R;


    [Header("3D Bones_L_TEMPLATE FOR CHARACTER"), Space(10)]
    public Transform upperArmBone_L;
    public Transform lowerArmBone_L;
    public Transform upperLegBone_L;
    public Transform lowerLegBone_L;

    [Space(10)]

    [Header("CHARACTER BONES")]
    public Transform HeadBone;
    public Transform TorsoBone;
    public Transform HipBone;
    [Header("RIGHT")]
    public Transform RightUpperArmBone;
    public Transform RightLowerArmBone;
    public Transform RightUpperLegBone;
    public Transform RightLowerLegBone;
    [Header("LEFT")]
    public Transform LeftUpperArmBone;
    public Transform LeftLowerArmBone;
    public Transform LeftUpperLegBone;
    public Transform LeftLowerLegBone;

    // Start is called before the first frame update
    void Start()
    {
        #region pose 3D character

        //=================== COMMON LIMB ===================
        BonePoser(HeadBone, headBone);
        BonePoser(TorsoBone, torsoBone);
        BonePoser(HipBone, hipBone);
        //==================== R LIMB =========================
        BonePoser(RightUpperArmBone, upperArmBone_R);
        BonePoser(RightLowerArmBone, lowerArmBone_R);
        BonePoser(RightUpperLegBone, upperLegBone_R);
        BonePoser(RightLowerLegBone, lowerLegBone_R);


        //==================== L LIMB ==============================
        BonePoser(LeftUpperArmBone, upperArmBone_L);
        BonePoser(LeftLowerArmBone, lowerArmBone_L);
        BonePoser(LeftUpperLegBone, upperLegBone_L);
        BonePoser(LeftLowerLegBone, lowerLegBone_L);

        #endregion
    }

    /// <summary>
    /// Helper function to pose our character
    /// </summary>
    /// <param name="root"> character bone </param>
    /// <param name="target"> template bone </param>
    private void BonePoser(Transform root, Transform target)
    {
        //set character bone to be the same as template bone in position and rotation
        root.localPosition = target.localPosition;
        root.localEulerAngles = target.localEulerAngles;
        root.localScale = target.localScale;
    }
}
