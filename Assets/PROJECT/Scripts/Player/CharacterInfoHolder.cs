using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
public class CharacterInfoHolder : MonoBehaviour
{

    [Header("LIMB BONES"), Space(20)]
    public GameObject head_B;
    public GameObject hips_G;

    [Space(10)]
    [Header("Right Limbs"), Space(20)]
    public GameObject upperArm_R_B;
    public GameObject lowerArm_R_B;
    public GameObject upperLeg_R_B;
    public GameObject lowerLeg_R_B;
    public Transform hand_R_B;

    [Header("Left Limbs"), Space(5)]
    public GameObject upperArm_L_B;
    public GameObject lowerArm_L_B;
    public GameObject upperLeg_L_B;
    public GameObject lowerLeg_L_B;
    public Transform hand_L_B;


    [Space(10)]
    [Header("Limbs behaviour"), Space(20)]
    public LimbsBehaviour headLimbBehaviour;
    public LimbsBehaviour upperArmBehaviour;
    public LimbsBehaviour lowerArmBehaviour;
    public LimbsBehaviour upperLegBehaviour;
    public LimbsBehaviour lowerLegBehaviour;

    [Header("Limbs behaviour_L"), Space(10)]

    public LimbsBehaviour upperArmBehaviour_L;
    public LimbsBehaviour lowerArmBehaviour_L;
    public LimbsBehaviour upperLegBehaviour_L;
    public LimbsBehaviour lowerLegBehaviour_L;


    [Space(10)]
    [Header("RAGDOLL RIGIBODY"), Space(20)]
    public Rigidbody[] rigibodyLimb;


    // [Space(10)]
    // [Header("Limbs Rigibody - RG"), Space(20)]
    // public Rigidbody headRigi_RG;

    // public Rigidbody upperArmRigi_RG_R;
    // public Rigidbody lowerArmRigi_RG_R;
    // public Rigidbody upperLegRigi_RG_R;
    // public Rigidbody lowerLegRigi_RG_R;

    // public Rigidbody upperArmRigi_RG_L;
    // public Rigidbody lowerArmRigi_RG_L;
    // public Rigidbody upperLegRigi_RG_L;
    // public Rigidbody lowerLegRigi_RG_L;


    [Space(10)]
    [Header("LIMB MESHES"), Space(20)]
    public GameObject head_M;

    [Header("Right Limbs"), Space(5)]
    public GameObject upperArm_R_M;
    public GameObject lowerArm_R_M;
    public GameObject upperLeg_R_M;
    public GameObject lowerLeg_R_M;

    [Header("Left Limbs"), Space(5)]
    public GameObject upperArm_L_M;
    public GameObject lowerArm_L_M;
    public GameObject upperLeg_L_M;
    public GameObject lowerLeg_L_M;

    [Space(10)]
    [Header("LIMB FORCE TO BREAK"), Space(20)]
    public float head_Force;
    public float upperArm_Force;
    public float lowerArm_Force;
    public float upperLeg_Force;
    public float lowerLeg_Force;

    [Space(10)]
    [Header("LIMB TO SPAWN"), Space(20)]

    public Rigidbody head_S;

    [Header("Right Limbs"), Space(5)]
    public Rigidbody upperArm_R_S;
    public Rigidbody lowerArm_R_S;
    public Rigidbody upperLeg_R_S;
    public Rigidbody lowerLeg_R_S;

    [Header("Left Limbs"), Space(5)]
    public Rigidbody upperArm_L_S;
    public Rigidbody lowerArm_L_S;
    public Rigidbody upperLeg_L_S;
    public Rigidbody lowerLeg_L_S;

    [Header("Whole limb"), Space(5)]

    public Rigidbody upperArm_R_S_Whole;
    public Rigidbody upperLeg_R_S_Whole;
    public Rigidbody upperArm_L_S_Whole;
    public Rigidbody upperLeg_L_S_Whole;


    // [Space(10)]
    // [Header("LIMB ID"), Space(20)]
    // public GameObject head_LID;

    // public GameObject upperArm_LID_L;
    // public GameObject lowerArm_LID_L;
    // public GameObject upperleg_LID_L;
    // public GameObject lowerLeg_LID_L;

    // public GameObject upperArm_LID_R;
    // public GameObject lowerArm_LID_R;
    // public GameObject upperleg_LID_R;
    // public GameObject lowerLeg_LID_R;


    [Space(10)]
    [Header("LIMB ANIMATIONS"), Space(20)]

    public CharacterAnimationFollower head_A;

    [Header("Right Limbs"), Space(5)]
    public CharacterAnimationFollower upperArm_R_A;
    public CharacterAnimationFollower lowerArm_R_A;
    public CharacterAnimationFollower upperLeg_R_A;
    public CharacterAnimationFollower lowerLeg_R_A;
    public CharacterAnimationFollower foot_R_A;

    [Header("Left Limbs"), Space(5)]
    public CharacterAnimationFollower upperArm_L_A;
    public CharacterAnimationFollower lowerArm_L_A;
    public CharacterAnimationFollower upperLeg_L_A;
    public CharacterAnimationFollower lowerLeg_L_A;
    public CharacterAnimationFollower foot_L_A;


    [Space(10)]
    [Header("COLLIDER SIZE - CS"), Space(20)]
    public float head_CS;

    public Vector2 upperArm_CS;
    public Vector2 lowerArm_CS;
    public Vector2 torso_CS;
    public Vector2 hips_CS;
    public Vector2 upperLeg_CS;
    public Vector2 lowerLeg_CS;


    [Space(10)]
    [Header("COLLIDER OFFSET - CO"), Space(20)]
    public Vector2 head_CO;

    public Vector2 upperArm_CO;
    public Vector2 lowerArm_CO;
    public Vector2 torso_CO;
    public Vector2 hips_CO;
    public Vector2 upperLeg_CO;
    public Vector2 lowerLeg_CO;


    [Space(10)]
    [Header("Extra Accessory - EA"), Space(20)]
    public GameObject[] head_EA;

    public GameObject[] upperArm_R_EA;
    public GameObject[] lowerArm_R_EA;

    public GameObject[] upperLeg_R_EA;
    public GameObject[] lowerLeg_R_EA;

    public GameObject[] upperArm_L_EA;
    public GameObject[] lowerArm_L_EA;

    public GameObject[] upperLeg_L_EA;
    public GameObject[] lowerLeg_L_EA;


    [Space(10)]
    [Header("Maximum height"), Space(20)]
    public float maximumHeight;

    [Space(10)]
    [Header("Particle Effects"), Space(20)]
    public ParticleSystem bloodFlowEffect_particle;

    [Space(10)]
    [Header("CENTER HOLDER OF BODY"), Space(20)]
    public Rigidbody centerOfBody;

    [Space(10)]
    [Header("Other"), Space(20)]
    public CharacterBehaviour characterBehaviour;

}
