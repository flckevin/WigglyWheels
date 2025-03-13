using JetBrains.Annotations;
using Kamgam.BikeAndCharacter25D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * OBJECT HOLD: every root of vehicles
 * PURPOSE: assign characters bones to every vehicles in other word
 is a bridge between the vehicle and the character itself
 
========================== ANNOTATION ===================================
- R - RIGHT
- L - LEFT
- A - Animation
- CS - Connector Slot
- HJ - Hingejoint
- RG2D - Rigibody 2D
- RG - Rigibody 
- RL - Rotation Limit
- BS - Break Sensitivity
- H - Health (Limb)
- DMG - Damage amount that cause to itself
- CT - Contact (Limb)
- LID - limb ID
- LC - Limb collider
- EA - Extra accessory 
========================== ANNOTATION ===================================
 */

public class VehicleSetup : MonoBehaviour
{
    [Header("CHARACTER_INFO"), Space(20)]

    [Header("2D Bones")]
    public Transform head;
    public Transform hips;
    public Transform torso;

    [Header("2D Bones_RIGHT"), Space(10)]
    public Transform upperArm;
    public Transform lowerArm;
    public Transform upperLeg;
    public Transform lowerLeg;

    [Header("2D Bones_LEFT"), Space(10)]
    public Transform upperArm_L;
    public Transform lowerArm_L;
    public Transform upperLeg_L;
    public Transform lowerLeg_L;

    [Space(10)]


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
    public Transform hand_R;

    [Header("3D Bones_L_TEMPLATE FOR CHARACTER"), Space(10)]
    public Transform upperArmBone_L;
    public Transform lowerArmBone_L;
    public Transform upperLegBone_L;
    public Transform lowerLegBone_L;
    public Transform hand_L;

    [Space(10)]


    [Header("3D Bones_ANIMATION_NO ANIMATION NO FILL IN - A"), Space(20)]

    public Transform headBone_A;
    public Transform torsoBone_A;


    [Header("3D Bones_R_ANIMATION_NO ANIMATION NO FILL IN - A"), Space(10)]
    public Transform upperArmBone_R_A;
    public Transform lowerArmBone_R_A;
    public Transform upperLegBone_R_A;
    public Transform lowerLegBone_R_A;
    public Transform foot_L_A;


    [Header("3D Bones_L_ANIMATION_NO ANIMATION NO FILL IN - A"), Space(10)]
    public Transform upperArmBone_L_A;
    public Transform lowerArmBone_L_A;
    public Transform upperLegBone_L_A;
    public Transform lowerLegBone_L_A;
    public Transform foot_R_A;


    [Space(10)]
    [Header("2D Limbs Hinge Joint MAIN JOINT - HJ"), Space(20)]
    public HingeJoint2D head_HJ;
    public HingeJoint2D upperArm_HJ_R;
    public HingeJoint2D lowerArm_HJ_R;
    public HingeJoint2D upperLeg_HJ_R;
    public HingeJoint2D lowerLeg_HJ_R;
    public HingeJoint2D upperArm_HJ_L;
    public HingeJoint2D lowerArm_HJ_L;
    public HingeJoint2D upperLeg_HJ_L;
    public HingeJoint2D lowerLeg_HJ_L;


    [Space(10)]
    [Header("2D Limbs Rigibody - RG2D"), Space(20)]
    public Rigidbody2D headRigi_RG2D;

    public Rigidbody2D upperArmRigi_RG2D_R;
    public Rigidbody2D lowerArmRigi_RG2D_R;
    public Rigidbody2D upperLegRigi_RG2D_R;
    public Rigidbody2D lowerLegRigi_RG2D_R;

    public Rigidbody2D upperArmRigi_RG2D_L;
    public Rigidbody2D lowerArmRigi_RG2D_L;
    public Rigidbody2D upperLegRigi_RG2D_L;
    public Rigidbody2D lowerLegRigi_RG2D_L;



    [Space(10)]
    [Header("2D Limbs Hinge Joint Maximum Limitation - RL"), Space(20)]

    [Range(-360, 360)]
    public float head_RL;
    [Range(-360, 360)]
    public float upperArm_RL;
    [Range(-360, 360)]
    public float lowerARm_RL;
    [Range(-360, 360)]
    public float upperLeg_RL;
    [Range(-360, 360)]
    public float lowerLeg_RL;



    [Space(10)]
    [Header("General Limbs Break Sensitivity - BS"), Space(20)]
    public float head_BS = 3;
    public float upperArm_BS = 3;
    public float lowerArm_BS = 3;
    public float upperLeg_BS = 3;
    public float lowerLeg_BS = 3;


    [Space(10)]
    [Header("General Limbs Health - H"), Space(20)]
    public float headHealth_H;
    public float upperArmHealth_H;
    public float lowerArmHealth_H;
    public float upperlegHealth_H;
    public float lowerLegHealth_H;

    [Space(10)]
    [Header("Self Damage - SDMG"), Space(20)]
    public float headDamage_SDMG;
    public float upperArmDamage_SDMG;
    public float lowerArmDamage_SDMG;
    public float upperlegDamage_SDMG;
    public float lowerLegDamage_SDMG;

    [Space(10)]
    [Header("2D Limbs Contact - CT"), Space(20)]
    public LimbContact head_CT;
    public LimbContact upperArm_CT_R;
    public LimbContact lowerArm_CT_R;
    public LimbContact upperLeg_CT_R;
    public LimbContact lowerLeg_CT_R;
    public LimbContact upperArm_CT_L;
    public LimbContact lowerArm_CT_L;
    public LimbContact upperLeg_CT_L;
    public LimbContact lowerLeg_CT_L;

    [Space(10)]
    [Header("2D Limbs Collider - LC"), Space(20)]
    public CircleCollider2D headCol_LC;

    [Space(5)]
    public BoxCollider2D torso_LC;
    public BoxCollider2D hips_LC;

    [Space(5)]
    public BoxCollider2D upperArmCol_LC_R;
    public BoxCollider2D lowerArmCol_LC_R;
    public BoxCollider2D upperLegCol_LC_R;
    public BoxCollider2D lowerLegCol_LC_R;

    [Space(5)]
    public BoxCollider2D upperArmCol_LC_L;
    public BoxCollider2D lowerArmCol_LC_L;
    public BoxCollider2D upperLegCol_LC_L;
    public BoxCollider2D lowerLegCol_LC_L;


    [Space(10)]
    [Header("General Limbs ID - LID"), Space(20)]
    public int arm_LID_R = 0;
    public int arm_LID_L = 0;
    public int leg_LID_R = 1;
    public int leg_LID_L = 1;

    [Space(10)]
    [Header("2D Vehicle INFO"), Space(20)]

    public Joint2D[] jointsConnectToBike;
    public Transform parentTo;

    [Space(10)]
    [Header("2D HOLDER JOINTS"), Space(20)]
    public Joint2D[] holderJoins;



    public void AssignBones(CharacterBones _characterBone, CharacterInfoHolder _characterInfo)
    {

        #region  OffRigibody
        _characterInfo.characterBehaviour.ragdoll = _characterInfo.rigibodyLimb;

        _characterInfo.characterBehaviour.RagdollKinimaticOnOff(true);
        #endregion


        #region pose 3D character

        //=================== COMMON LIMB ===================
        BonePoser(_characterBone.HeadBone, headBone);
        BonePoser(_characterBone.TorsoBone, torsoBone);
        BonePoser(_characterBone.HipBone, hipBone);
        //==================== R LIMB =========================
        BonePoser(_characterBone.RightUpperArmBone, upperArmBone_R);
        BonePoser(_characterBone.RightLowerArmBone, lowerArmBone_R);
        BonePoser(_characterBone.RightUpperLegBone, upperLegBone_R);
        BonePoser(_characterBone.RightLowerLegBone, lowerLegBone_R);
        BonePoser(_characterInfo.hand_R_B, hand_R);

        //==================== L LIMB ==============================
        BonePoser(_characterBone.LeftUpperArmBone, upperArmBone_L);
        BonePoser(_characterBone.LeftLowerArmBone, lowerArmBone_L);
        BonePoser(_characterBone.LeftUpperLegBone, upperLegBone_L);
        BonePoser(_characterBone.LeftLowerLegBone, lowerLegBone_L);
        BonePoser(_characterInfo.hand_L_B, hand_L);

        #endregion


        #region Destroy unesscary animation component for limb
        AnimationChecker(_characterInfo.head_A, headBone_A, () => _characterBone.headTrack = false);

        //===================== R LIMBS =============================
        AnimationChecker(_characterInfo.upperArm_R_A, upperArmBone_R_A, () => _characterBone.upperArmTrack = false);
        AnimationChecker(_characterInfo.lowerArm_R_A, lowerArmBone_R_A, () => _characterBone.lowerArmTrack = false);
        AnimationChecker(_characterInfo.upperLeg_R_A, upperLegBone_R_A, () => _characterBone.upperLegTrack = false);
        AnimationChecker(_characterInfo.lowerLeg_R_A, lowerLegBone_R_A, () => _characterBone.lowerLegTrack = false);
        AnimationChecker(_characterInfo.foot_R_A, foot_R_A, null);

        //===================== L LIMBS =============================
        AnimationChecker(_characterInfo.upperArm_L_A, upperArmBone_L_A, () => _characterBone.upperArmTrack = false);
        AnimationChecker(_characterInfo.lowerArm_L_A, lowerArmBone_L_A, () => _characterBone.lowerArmTrack = false);
        AnimationChecker(_characterInfo.upperLeg_L_A, upperLegBone_L_A, () => _characterBone.upperLegTrack = false);
        AnimationChecker(_characterInfo.lowerLeg_L_A, lowerLegBone_L_A, () => _characterBone.lowerLegTrack = false);
        AnimationChecker(_characterInfo.foot_L_A, foot_L_A, null);

        #endregion


        #region parent character to bike position

        //parent character to vehicle character torso 2d bone
        //so the character will follow 2d bone and create jiggling effects

        /// set character position to be at bone parent holder position
        _characterBone.transform.position = boneParentHolder.position;
        // set character position to have the same rotation as bone parent holder
        //so it will face correct direction
        _characterBone.transform.rotation = boneParentHolder.rotation;

        // _characterBone.transform.position += offSet;

        //if parent does exist
        if (parentTo != null)
        {
            // assign character bone to parent
            _characterBone.transform.parent = parentTo.transform;
        }
        else // parent does not exist
        {
            //parent character to hips as default
            _characterBone.transform.parent = hips;
        }

        #endregion


        #region assign character 2D bones
        _characterBone.Head = head;
        _characterBone.Torso = torso;
        _characterBone.hips = hips;

        _characterBone.LowerArm_R = lowerArm;
        _characterBone.UpperArm_R = upperArm;
        _characterBone.LowerLeg_R = lowerLeg;
        _characterBone.UpperLeg_R = upperLeg;

        _characterBone.LowerArm_L = lowerArm_L;
        _characterBone.UpperArm_L = upperArm_L;
        _characterBone.LowerLeg_L = lowerLeg_L;
        _characterBone.UpperLeg_L = upperLeg_L;
        #endregion


        #region assign limb to spawn

        _characterInfo.headLimbBehaviour.limbToSpawn = _characterInfo.head_S;

        //====================== RIGHT ===============================
        _characterInfo.upperArmBehaviour.limbToSpawn = _characterInfo.upperArm_R_S;

        _characterInfo.lowerArmBehaviour.limbToSpawn = _characterInfo.lowerArm_R_S;

        _characterInfo.upperLegBehaviour.limbToSpawn = _characterInfo.upperLeg_R_S;

        _characterInfo.lowerLegBehaviour.limbToSpawn = _characterInfo.lowerLeg_R_S;

        //====================== RIGHT ===============================

        //======================== LEFT ==============================

        _characterInfo.upperArmBehaviour_L.limbToSpawn = _characterInfo.upperArm_L_S;

        _characterInfo.lowerArmBehaviour_L.limbToSpawn = _characterInfo.lowerArm_L_S;

        _characterInfo.upperLegBehaviour_L.limbToSpawn = _characterInfo.upperLeg_L_S;

        _characterInfo.lowerLegBehaviour_L.limbToSpawn = _characterInfo.lowerLeg_L_S;

        //======================== LEFT ==============================
        #endregion


        #region assign position to spawn ultilities - REMOVED

        // //====================== RIGHT ===============================

        // _characterInfo.headLimbBehaviour.posToSpawnUltilities_Bones = _characterInfo.head_B.transform;

        // _characterInfo.upperArmBehaviour.posToSpawnUltilities_Bones = _characterInfo.upperArm_R_B.transform;

        // _characterInfo.lowerArmBehaviour.posToSpawnUltilities_Bones = _characterInfo.lowerArm_R_B.transform;

        // _characterInfo.upperLegBehaviour.posToSpawnUltilities_Bones = _characterInfo.upperLeg_R_B.transform;

        // _characterInfo.lowerLegBehaviour.posToSpawnUltilities_Bones = _characterInfo.lowerLeg_R_B.transform;

        // //====================== RIGHT ===============================

        // //======================== LEFT ==============================

        // _characterInfo.upperArmBehaviour_L.posToSpawnUltilities_Bones = _characterInfo.upperArm_L_B.transform;

        // _characterInfo.lowerArmBehaviour_L.posToSpawnUltilities_Bones = _characterInfo.lowerArm_L_B.transform;

        // _characterInfo.upperLegBehaviour_L.posToSpawnUltilities_Bones = _characterInfo.upperLeg_L_B.transform;

        // _characterInfo.lowerLegBehaviour_L.posToSpawnUltilities_Bones = _characterInfo.lowerLeg_L_B.transform;

        // //======================== LEFT ==============================

        #endregion


        #region assign limb meshes

        //====================== RIGHT ===============================

        _characterInfo.headLimbBehaviour.limb = _characterInfo.head_M;

        _characterInfo.upperArmBehaviour.limb = _characterInfo.upperArm_R_M;

        _characterInfo.lowerArmBehaviour.limb = _characterInfo.lowerArm_R_M;

        _characterInfo.upperLegBehaviour.limb = _characterInfo.upperLeg_R_M;

        _characterInfo.lowerLegBehaviour.limb = _characterInfo.lowerLeg_R_M;

        //====================== RIGHT ===============================

        //======================== LEFT ==============================

        _characterInfo.upperArmBehaviour_L.limb = _characterInfo.upperArm_L_M;

        _characterInfo.lowerArmBehaviour_L.limb = _characterInfo.lowerArm_L_M;

        _characterInfo.upperLegBehaviour_L.limb = _characterInfo.upperLeg_L_M;

        _characterInfo.lowerLegBehaviour_L.limb = _characterInfo.lowerLeg_L_M;

        //======================== LEFT ==============================

        #endregion


        #region Assign connected limbs

        //********************* 3D *************************

        //====================== RIGHT ===============================

        _characterInfo.upperArmBehaviour.connectedLimb = _characterInfo.lowerArmBehaviour;
        _characterInfo.upperLegBehaviour.connectedLimb = _characterInfo.lowerLegBehaviour;

        //====================== RIGHT ===============================


        //======================== LEFT ==============================

        _characterInfo.upperArmBehaviour_L.connectedLimb = _characterInfo.lowerArmBehaviour_L;
        _characterInfo.upperLegBehaviour_L.connectedLimb = _characterInfo.lowerLegBehaviour_L;

        //======================== LEFT ==============================

        //********************* 3D *************************


        //********************* 2D *************************

        upperArm_CT_R.connectedLimb = lowerArm_CT_R;
        upperArm_CT_L.connectedLimb = lowerArm_CT_L;

        //====================== RIGHT ===============================


        //======================== LEFT ==============================

        upperLeg_CT_R.connectedLimb = lowerLeg_CT_R;
        upperLeg_CT_L.connectedLimb = lowerLeg_CT_L;

        //********************* 2D *************************

        #endregion


        #region Assign nerve connection parent - REMOVED

        // headLimbBehaviour.limbConnectorRoot = _characterInfo.head_NC_G;

        // lowerArmBehaviour.limbConnectorRoot = _characterInfo.upperArm_NC_G;
        // lowerLegBehaviour.limbConnectorRoot = _characterInfo.upperLeg_NC_G;

        // lowerArmBehaviour_L.limbConnectorRoot = _characterInfo.upperArm_L_NC_G;
        // lowerLegBehaviour_L.limbConnectorRoot = _characterInfo.upperLeg_L_NC_G;

        #endregion


        #region Assign info to character behaviour

        _characterInfo.characterBehaviour.connectedJoint = jointsConnectToBike;

        #endregion


        #region Assign limb break force info

        _characterInfo.headLimbBehaviour.limbForceReq = _characterInfo.head_Force;

        _characterInfo.upperArmBehaviour.limbForceReq = _characterInfo.upperArm_Force;
        _characterInfo.lowerArmBehaviour.limbForceReq = _characterInfo.lowerArm_Force;

        _characterInfo.upperArmBehaviour_L.limbForceReq = _characterInfo.upperArm_Force;
        _characterInfo.lowerArmBehaviour.limbForceReq = _characterInfo.lowerArm_Force;

        _characterInfo.upperLegBehaviour.limbForceReq = _characterInfo.upperLeg_Force;
        _characterInfo.lowerLegBehaviour.limbForceReq = _characterInfo.lowerLeg_Force;

        _characterInfo.upperLegBehaviour_L.limbForceReq = _characterInfo.upperLeg_Force;
        _characterInfo.lowerLegBehaviour_L.limbForceReq = _characterInfo.lowerLeg_Force;

        #endregion


        #region SPAWN BLOOD FLOW EFFECTS _ BLOOD FLOW TO BE SPECIFIC

        //** NOTE ** -> CONSIDER MAKING THIS PART AS PRE SPAWN BY MAKING A EDITOR WINDOW TO SPAWN ALL OF THESE EFFECT AND PARENT THEM SO WE CAN SKIP THIS PART TO LIGHTEN THE LOADING TIME

        //============================================================== LEFT ===========================================================================

        _characterInfo.headLimbBehaviour.bloodFlowEffectPrefab = Instantiate(_characterInfo.bloodFlowEffect_particle, _characterInfo.headLimbBehaviour.transform.position,
        _characterInfo.headLimbBehaviour.transform.rotation);
        _characterInfo.headLimbBehaviour.bloodFlowEffectPrefab.transform.parent = _characterInfo.headLimbBehaviour.transform;

        _characterInfo.upperArmBehaviour.bloodFlowEffectPrefab = Instantiate(_characterInfo.bloodFlowEffect_particle, _characterInfo.upperArmBehaviour.transform.position,
        _characterInfo.upperArmBehaviour.transform.rotation);
        _characterInfo.upperArmBehaviour.bloodFlowEffectPrefab.transform.parent = _characterInfo.upperArmBehaviour.transform;

        _characterInfo.lowerArmBehaviour.bloodFlowEffectPrefab = Instantiate(_characterInfo.bloodFlowEffect_particle, _characterInfo.lowerArmBehaviour.transform.position,
        _characterInfo.lowerArmBehaviour.transform.rotation);
        _characterInfo.lowerArmBehaviour.bloodFlowEffectPrefab.transform.parent = _characterInfo.lowerArmBehaviour.transform;

        _characterInfo.upperLegBehaviour.bloodFlowEffectPrefab = Instantiate(_characterInfo.bloodFlowEffect_particle, _characterInfo.upperLegBehaviour.transform.position,
        _characterInfo.upperLegBehaviour.transform.rotation);
        _characterInfo.upperLegBehaviour.bloodFlowEffectPrefab.transform.parent = _characterInfo.upperLegBehaviour.transform;

        _characterInfo.lowerLegBehaviour.bloodFlowEffectPrefab = Instantiate(_characterInfo.bloodFlowEffect_particle, _characterInfo.lowerLegBehaviour.transform.position,
        _characterInfo.lowerLegBehaviour.transform.rotation);
        _characterInfo.lowerLegBehaviour.bloodFlowEffectPrefab.transform.parent = _characterInfo.lowerLegBehaviour.transform;

        //============================================================== LEFT ===========================================================================

        //============================================================== RIGHT ==========================================================================

        _characterInfo.upperArmBehaviour_L.bloodFlowEffectPrefab = Instantiate(_characterInfo.bloodFlowEffect_particle, _characterInfo.upperArmBehaviour_L.transform.position,
        _characterInfo.upperArmBehaviour_L.transform.rotation);
        _characterInfo.upperArmBehaviour_L.bloodFlowEffectPrefab.transform.parent = _characterInfo.upperArmBehaviour_L.transform;

        _characterInfo.lowerArmBehaviour_L.bloodFlowEffectPrefab = Instantiate(_characterInfo.bloodFlowEffect_particle, _characterInfo.lowerArmBehaviour_L.transform.position,
        _characterInfo.lowerArmBehaviour_L.transform.rotation);
        _characterInfo.lowerArmBehaviour_L.bloodFlowEffectPrefab.transform.parent = _characterInfo.lowerArmBehaviour_L.transform;

        _characterInfo.upperLegBehaviour_L.bloodFlowEffectPrefab = Instantiate(_characterInfo.bloodFlowEffect_particle, _characterInfo.upperLegBehaviour_L.transform.position,
        _characterInfo.upperLegBehaviour_L.transform.rotation);
        _characterInfo.upperLegBehaviour_L.bloodFlowEffectPrefab.transform.parent = _characterInfo.upperLegBehaviour_L.transform;

        _characterInfo.lowerLegBehaviour_L.bloodFlowEffectPrefab = Instantiate(_characterInfo.bloodFlowEffect_particle, _characterInfo.lowerLegBehaviour_L.transform.position,
        _characterInfo.lowerLegBehaviour_L.transform.rotation);
        _characterInfo.lowerLegBehaviour_L.bloodFlowEffectPrefab.transform.parent = _characterInfo.lowerLegBehaviour_L.transform;

        //============================================================== RIGHT ==========================================================================

        #endregion


        #region Enable all limbs behaviour

        _characterInfo.headLimbBehaviour.enabled = true;
        _characterInfo.upperArmBehaviour.enabled = true;
        _characterInfo.lowerArmBehaviour.enabled = true;
        _characterInfo.upperLegBehaviour.enabled = true;
        _characterInfo.lowerLegBehaviour.enabled = true;

        _characterInfo.upperArmBehaviour_L.enabled = true;
        _characterInfo.lowerArmBehaviour_L.enabled = true;
        _characterInfo.upperLegBehaviour_L.enabled = true;
        _characterInfo.lowerLegBehaviour_L.enabled = true;

        #endregion


        #region Assign Limb Senesitivity 

        _characterInfo.headLimbBehaviour.limbForceReq = head_BS;

        _characterInfo.upperArmBehaviour.limbForceReq = upperArm_BS;
        _characterInfo.lowerArmBehaviour.limbForceReq = lowerArm_BS;
        _characterInfo.upperLegBehaviour.limbForceReq = upperArm_BS;
        _characterInfo.lowerLegBehaviour.limbForceReq = lowerLeg_BS;

        _characterInfo.upperArmBehaviour_L.limbForceReq = upperArm_BS;
        _characterInfo.lowerArmBehaviour_L.limbForceReq = lowerArm_BS;
        _characterInfo.upperLegBehaviour_L.limbForceReq = upperArm_BS;
        _characterInfo.lowerLegBehaviour_L.limbForceReq = lowerLeg_BS;

        #endregion


        #region Assign Limb HingeJoint

        _characterInfo.headLimbBehaviour.mainJoint = head_HJ;

        _characterInfo.upperArmBehaviour.mainJoint = upperArm_HJ_R;
        _characterInfo.lowerArmBehaviour.mainJoint = lowerArm_HJ_R;
        _characterInfo.upperLegBehaviour.mainJoint = upperLeg_HJ_R;
        _characterInfo.lowerLegBehaviour.mainJoint = lowerLeg_HJ_R;

        _characterInfo.upperArmBehaviour_L.mainJoint = upperArm_HJ_L;
        _characterInfo.lowerArmBehaviour_L.mainJoint = lowerArm_HJ_L;
        _characterInfo.upperLegBehaviour_L.mainJoint = upperLeg_HJ_L;
        _characterInfo.lowerLegBehaviour_L.mainJoint = lowerLeg_HJ_L;

        #endregion


        #region  Assign Limb HingeJoint Limitation

        head_CT.hingeJointLimitation = head_RL;

        upperArm_CT_R.hingeJointLimitation = upperArm_RL;
        upperArm_CT_L.hingeJointLimitation = upperArm_RL;

        lowerArm_CT_R.hingeJointLimitation = lowerARm_RL;
        lowerArm_CT_L.hingeJointLimitation = lowerARm_RL;

        upperLeg_CT_R.hingeJointLimitation = upperLeg_RL;
        upperLeg_CT_L.hingeJointLimitation = upperLeg_RL;

        lowerLeg_CT_R.hingeJointLimitation = lowerLeg_RL;
        lowerLeg_CT_L.hingeJointLimitation = lowerLeg_RL;

        #endregion


        #region Assign holder joints into character behaviour

        //if there are holder joints
        if (holderJoins.Length > 0)
        {
            //give holder joints
            _characterInfo.characterBehaviour.holderJoint = holderJoins;
            //set holder joints index
            _characterInfo.characterBehaviour._currentHolderJointIndex = holderJoins.Length - 1;
        }


        #endregion


        #region  Assign limb health

        LimbManager.limbInfo.Add(LimbType.Head, headHealth_H);

        LimbManager.limbInfo.Add(LimbType.Hand_Upper, upperArmHealth_H);
        LimbManager.limbInfo.Add(LimbType.Hand_Lower, lowerArmHealth_H);

        LimbManager.limbInfo.Add(LimbType.Leg_Upper, upperlegHealth_H);
        LimbManager.limbInfo.Add(LimbType.Leg_Lower, lowerLegHealth_H);

        LimbManager._limbInfoDefaultValue = LimbManager.limbInfo;

        #endregion


        #region  Assign Limb Type

        head_CT.limbType = LimbType.Head;

        upperArm_CT_R.limbType = LimbType.Hand_Upper;
        lowerArm_CT_R.limbType = LimbType.Hand_Lower;
        upperLeg_CT_R.limbType = LimbType.Leg_Upper;
        lowerLeg_CT_R.limbType = LimbType.Leg_Lower;

        upperArm_CT_L.limbType = LimbType.Hand_Upper;
        lowerArm_CT_L.limbType = LimbType.Hand_Lower;
        upperLeg_CT_L.limbType = LimbType.Leg_Upper;
        lowerLeg_CT_L.limbType = LimbType.Leg_Lower;

        #endregion


        #region  Assign amount of Damage Limb can cause to itself

        head_CT.selfDamage = headDamage_SDMG;

        upperArm_CT_R.selfDamage = upperArmDamage_SDMG;
        lowerArm_CT_R.selfDamage = lowerArmDamage_SDMG;
        upperLeg_CT_R.selfDamage = upperlegDamage_SDMG;
        lowerLeg_CT_R.selfDamage = lowerLegDamage_SDMG;

        upperArm_CT_L.selfDamage = upperArmDamage_SDMG;
        lowerArm_CT_L.selfDamage = lowerArmDamage_SDMG;
        upperLeg_CT_L.selfDamage = upperlegDamage_SDMG;
        lowerLeg_CT_L.selfDamage = lowerLegDamage_SDMG;

        #endregion


        #region  Assign limb ID - OLD - REMOVED

        // headLimbBehaviour.limbID = int.Parse(_characterInfo.head_LID.name);

        // upperArmBehaviour_L.limbID = int.Parse(_characterInfo.upperArm_LID_L.name);
        // lowerArmBehaviour_L.limbID = int.Parse(_characterInfo.lowerArm_LID_L.name);
        // upperLegBehaviour_L.limbID = int.Parse(_characterInfo.upperleg_LID_L.name);
        // lowerLegBehaviour_L.limbID = int.Parse(_characterInfo.lowerLeg_LID_L.name);

        // upperArmBehaviour.limbID = int.Parse(_characterInfo.upperArm_LID_R.name);
        // lowerArmBehaviour.limbID = int.Parse(_characterInfo.lowerArm_LID_R.name);
        // upperLegBehaviour.limbID = int.Parse(_characterInfo.upperleg_LID_R.name);
        // lowerLegBehaviour.limbID = int.Parse(_characterInfo.lowerLeg_LID_R.name);

        #endregion


        #region  Assign required limbs REMOVED

        // upperArm_CT_R.requiredLimb = upperArm_CT_L;
        // upperArm_CT_L.requiredLimb = upperArm_CT_R;

        // lowerArm_CT_R.requiredLimb = lowerArm_CT_L;
        // lowerArm_CT_L.requiredLimb = lowerArm_CT_R;

        // upperLeg_CT_R.requiredLimb = upperLeg_CT_L;
        // upperLeg_CT_L.requiredLimb = upperLeg_CT_R;

        // lowerLeg_CT_R.requiredLimb = lowerLeg_CT_L;
        // lowerLeg_CT_L.requiredLimb = lowerLeg_CT_R;

        #endregion


        #region  Assign limb rigibody to character behaviour

        _characterInfo.characterBehaviour.ragdolllimb = new Rigidbody2D[] { headRigi_RG2D,
                                                                            upperArmRigi_RG2D_R,
                                                                            lowerArmRigi_RG2D_R,
                                                                            upperLegRigi_RG2D_R,
                                                                            lowerLegRigi_RG2D_R,
                                                                            upperArmRigi_RG2D_L,
                                                                            lowerArmRigi_RG2D_L,
                                                                            upperLegRigi_RG2D_L,
                                                                            lowerLegRigi_RG2D_L,};

        #endregion


        #region  Assign limb connector slots - REMOVED



        #endregion


        #region  Assign Limb Collider Size

        headCol_LC.radius = _characterInfo.head_CS;
        upperArmCol_LC_R.size = _characterInfo.upperArm_CS;
        upperArmCol_LC_L.size = _characterInfo.upperArm_CS;

        lowerArmCol_LC_R.size = _characterInfo.lowerArm_CS;
        lowerArmCol_LC_L.size = _characterInfo.lowerArm_CS;

        upperLegCol_LC_R.size = _characterInfo.upperLeg_CS;
        upperLegCol_LC_L.size = _characterInfo.upperLeg_CS;

        lowerLegCol_LC_R.size = _characterInfo.lowerLeg_CS;
        lowerLegCol_LC_L.size = _characterInfo.lowerLeg_CS;

        #endregion


        #region  Assign limb collider Offset

        headCol_LC.offset = _characterInfo.head_CO;
        upperArmCol_LC_R.offset = _characterInfo.upperArm_CO;
        upperArmCol_LC_L.offset = _characterInfo.upperArm_CO;

        lowerArmCol_LC_R.offset = _characterInfo.lowerArm_CO;
        lowerArmCol_LC_L.offset = _characterInfo.lowerArm_CO;

        upperLegCol_LC_R.offset = _characterInfo.upperLeg_CO;
        upperLegCol_LC_L.offset = _characterInfo.upperLeg_CO;

        lowerLegCol_LC_R.offset = _characterInfo.lowerLeg_CO;
        lowerLegCol_LC_L.offset = _characterInfo.lowerLeg_CO;

        #endregion


        #region Assign limb maximum height value

        head_CT.maximumHeight = _characterInfo.maximumHeight;

        upperArm_CT_R.maximumHeight = _characterInfo.maximumHeight;
        lowerArm_CT_R.maximumHeight = _characterInfo.maximumHeight;

        upperArm_CT_L.maximumHeight = _characterInfo.maximumHeight;
        lowerArm_CT_L.maximumHeight = _characterInfo.maximumHeight;

        upperLeg_CT_R.maximumHeight = _characterInfo.maximumHeight;
        lowerLeg_CT_R.maximumHeight = _characterInfo.maximumHeight;

        upperLeg_CT_L.maximumHeight = _characterInfo.maximumHeight;
        lowerLeg_CT_L.maximumHeight = _characterInfo.maximumHeight;

        #endregion


        #region Assign limb id

        upperArm_CT_R.limbID = arm_LID_R;
        upperArm_CT_L.limbID = arm_LID_L;

        lowerArm_CT_R.limbID = arm_LID_R;
        lowerArm_CT_L.limbID = arm_LID_L;

        upperLeg_CT_R.limbID = leg_LID_R;
        upperLeg_CT_L.limbID = leg_LID_L;

        lowerLeg_CT_R.limbID = leg_LID_R;
        lowerLeg_CT_L.limbID = leg_LID_L;

        #endregion


        #region  Assign limb contact

        head_CT.limbBhaviour = _characterInfo.headLimbBehaviour;

        //========================= RIGHT ===========================
        upperArm_CT_R.limbBhaviour = _characterInfo.upperArmBehaviour;
        lowerArm_CT_R.limbBhaviour = _characterInfo.lowerArmBehaviour;
        upperLeg_CT_R.limbBhaviour = _characterInfo.upperLegBehaviour;
        lowerLeg_CT_R.limbBhaviour = _characterInfo.lowerLegBehaviour;
        //========================= RIGHT ===========================

        //========================= LEFT ===========================
        upperArm_CT_L.limbBhaviour = _characterInfo.upperArmBehaviour_L;
        lowerArm_CT_L.limbBhaviour = _characterInfo.lowerArmBehaviour_L;
        upperLeg_CT_L.limbBhaviour = _characterInfo.upperLegBehaviour_L;
        lowerLeg_CT_L.limbBhaviour = _characterInfo.lowerLegBehaviour_L;
        //========================= LEFT ===========================

        #endregion


        #region  Assign limb contact to 3D limb behaviour

        _characterInfo.headLimbBehaviour.limbContact = head_CT;

        _characterInfo.upperArmBehaviour.limbContact = upperArm_CT_R;
        _characterInfo.lowerArmBehaviour.limbContact = lowerArm_CT_R;

        _characterInfo.upperLegBehaviour.limbContact = upperLeg_CT_R;
        _characterInfo.lowerLegBehaviour.limbContact = lowerLeg_CT_R;

        _characterInfo.upperArmBehaviour_L.limbContact = upperArm_CT_L;
        _characterInfo.lowerArmBehaviour_L.limbContact = lowerArm_CT_L;

        _characterInfo.upperLegBehaviour_L.limbContact = upperLeg_CT_L;
        _characterInfo.lowerLegBehaviour_L.limbContact = lowerLeg_CT_L;

        #endregion


        #region  Assign extra accessory

        _characterInfo.headLimbBehaviour.extraAccessory = _characterInfo.head_EA;

        _characterInfo.upperArmBehaviour.extraAccessory = _characterInfo.upperArm_R_EA;
        _characterInfo.lowerArmBehaviour.extraAccessory = _characterInfo.lowerArm_R_EA;

        _characterInfo.upperLegBehaviour.extraAccessory = _characterInfo.upperLeg_R_EA;
        _characterInfo.lowerLegBehaviour.extraAccessory = _characterInfo.lowerLeg_R_EA;


        _characterInfo.upperArmBehaviour_L.extraAccessory = _characterInfo.upperArm_L_EA;
        _characterInfo.lowerArmBehaviour_L.extraAccessory = _characterInfo.lowerArm_L_EA;

        _characterInfo.upperLegBehaviour_L.extraAccessory = _characterInfo.upperLeg_L_EA;
        _characterInfo.lowerLegBehaviour_L.extraAccessory = _characterInfo.lowerLeg_L_EA;


        #endregion


        #region FINAL SETUP

        //center character
        GameManager.Instance.centerOfCharacter = _characterInfo.centerOfBody;
        //enable character bone behaviour to make all our bones follow 2d bones
        _characterBone.enabled = true;

        //=============================== DESTROY ALL UNECCESARRY CLASS ================================

        //destroy bone template to save some rams
        Destroy(boneParentHolder.gameObject);
        //destroy limb mesh holder
        Destroy(_characterInfo);
        //destroy this class since we no need this during gameplay
        Destroy(this);

        //=============================== DESTROY ALL UNECCESARRY CLASS ================================

        #endregion


    }


    #region HELPER FUNCTIONS


    //======================================================= HELPER FUNCTIONS =================================================================

    /// <summary>
    /// Helper function to pose our character
    /// </summary>
    /// <param name="root"> character bone </param>
    /// <param name="target"> template bone </param>
    private void BonePoser(Transform root, Transform target, bool _samePosition = true)
    {
        //set character bone to be the same as template bone in position and rotation
        root.localEulerAngles = target.localEulerAngles;
        root.localScale = target.localScale;

        if (_samePosition == false) return;
        root.localPosition = target.localPosition;

    }


    /// <summary>
    /// function to check whether there are animation to follow in a vehicle
    /// </summary>
    /// <param name="_anims"> character animation follower class </param>
    /// <param name="_target"> target to follow for animation follower class target varible </param>
    private void AnimationChecker(CharacterAnimationFollower _anims, Transform _target, Action _animsAction)
    {
        //if animation does exist
        if (_target != null)
        {
            //assign target to them
            _anims.target = _target;

            if (_animsAction == null) return;
            _animsAction();
        }
        else //animation does not exist
        {
            //destroy that class to save some space 
            //or we can deactivate it
            //Destroy(_anims);

            _anims.enabled = false;
        }

    }

    //======================================================= HELPER FUNCTIONS =================================================================


    #endregion
}
