 using UnityEngine;
 
namespace Kamgam.BikeAndCharacter25D
{

    public class CharacterBones : MonoBehaviour
    {
        [Header("2D BONES")]
        public Transform Head;
        public Transform Torso;
        public Transform hips;

        [Header("2D BONES_RIGHT")]
        public Transform UpperArm_R;
        public Transform LowerArm_R;
        public Transform UpperLeg_R;
        public Transform LowerLeg_R;

        [Header("2D BONES_LEFT")]
        public Transform UpperArm_L;
        public Transform LowerArm_L;
        public Transform UpperLeg_L;
        public Transform LowerLeg_L;

        [Space(10)]
        [Header("BONE TO TRACK")]
        public bool headTrack = true;
        public bool torsoTrack = true;
        public bool hipTrack = true;   
        public bool upperArmTrack = true;
        public bool lowerArmTrack = true;
        public bool upperLegTrack = true;   
        public bool lowerLegTrack = true;
        //[Header("2D BONES_LEFT")]
        //public Transform UpperArm_L;
        //public Transform LowerArm_L;
        //public Transform UpperLeg_L;
        //public Transform LowerLeg_L;
        //[Space(10)]

        protected float HeadMemory;
        protected float TorsoMemory;
        protected float hipMemory;
        //======================= RIGHT ================================
        protected float UpperArmMemory;
        protected float LowerArmMemory;
        protected float UpperLegMemory;
        protected float LowerLegMemory;
        //======================= LEFT ================================
        protected float UpperArmMemory_L;
        protected float LowerArmMemory_L;
        protected float UpperLegMemory_L;
        protected float LowerLegMemory_L;


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

        protected Quaternion HeadBoneMemory;
        protected Quaternion TorsoBoneMemory;
        public Quaternion HipBoneMemory;
        protected Quaternion RightUpperArmBoneMemory;
        protected Quaternion RightLowerArmBoneMemory;
        protected Quaternion RightUpperLegBoneMemory;
        protected Quaternion RightLowerLegBoneMemory;

        protected Quaternion LeftUpperArmBoneMemory;
        protected Quaternion LeftLowerArmBoneMemory;
        protected Quaternion LeftUpperLegBoneMemory;
        protected Quaternion LeftLowerLegBoneMemory;

        protected Character character;

        private void Start()
        {
            memorize();
            character = this.GetComponentInParent<Character>();
        }

        public void FixedUpdate()
        {
            updateBones();
        }

        protected void memorize()
        {
            HeadMemory = deltaAngle2D(Torso, Head);
            TorsoMemory = deltaAngle2D(hips,Torso); // torso is the root and therefore delta is always 0
            hipMemory = 0;
            //====================== RIGHT ===============================
            UpperArmMemory = deltaAngle2D(Torso, UpperArm_R);
            LowerArmMemory = deltaAngle2D(UpperArm_R, LowerArm_R);
            UpperLegMemory = deltaAngle2D(Torso, UpperLeg_R);
            LowerLegMemory = deltaAngle2D(UpperLeg_R, LowerLeg_R);

            //====================== LEFT ===============================
            UpperArmMemory_L = deltaAngle2D(Torso, UpperArm_L);
            LowerArmMemory_L = deltaAngle2D(UpperArm_L, LowerArm_L);
            UpperLegMemory_L = deltaAngle2D(Torso, UpperLeg_L);
            LowerLegMemory_L = deltaAngle2D(UpperLeg_L, LowerLeg_L);

            HeadBoneMemory = HeadBone.localRotation;
            TorsoBoneMemory = TorsoBone.localRotation;
         
            RightUpperArmBoneMemory = RightUpperArmBone.localRotation;
            RightLowerArmBoneMemory = RightLowerArmBone.localRotation;
            RightUpperLegBoneMemory = RightUpperLegBone.localRotation;
            RightLowerLegBoneMemory = RightLowerLegBone.localRotation;

            LeftUpperArmBoneMemory = LeftUpperArmBone.localRotation;
            LeftLowerArmBoneMemory = LeftLowerArmBone.localRotation;
            LeftUpperLegBoneMemory = LeftUpperLegBone.localRotation;
            LeftLowerLegBoneMemory = LeftLowerLegBone.localRotation;
        }

        protected float deltaAngle2D(Transform a, Transform b)
        {
            return Mathf.DeltaAngle(a.localRotation.eulerAngles.z, b.localRotation.eulerAngles.z);
        }

        protected void updateBones()
        {
            updateBone(HeadBone, HeadBoneMemory, deltaAngle2D(Torso, Head) - HeadMemory, true,headTrack);
            updateBone(TorsoBone, TorsoBoneMemory,deltaAngle2D(hips,Torso) - TorsoMemory,true,torsoTrack);

            updateBone(RightUpperArmBone, RightUpperArmBoneMemory, deltaAngle2D(Torso, UpperArm_R) - UpperArmMemory, true,upperArmTrack);
            updateBone(RightLowerArmBone, RightLowerArmBoneMemory, deltaAngle2D(UpperArm_R, LowerArm_R) - LowerArmMemory, true,lowerArmTrack);
            updateBone(RightUpperLegBone, RightUpperLegBoneMemory, deltaAngle2D(Torso, UpperLeg_R) - UpperLegMemory, true, upperLegTrack);
            updateBone(RightLowerLegBone, RightLowerLegBoneMemory, deltaAngle2D(UpperLeg_R, LowerLeg_R) - LowerLegMemory, true, lowerLegTrack);

            updateBone(LeftUpperArmBone, LeftUpperArmBoneMemory, deltaAngle2D(Torso, UpperArm_L) - UpperArmMemory_L, true, upperArmTrack);
            updateBone(LeftLowerArmBone, LeftLowerArmBoneMemory, deltaAngle2D(UpperArm_L, LowerArm_L) - LowerArmMemory_L, true, lowerArmTrack);
            updateBone(LeftUpperLegBone, LeftUpperLegBoneMemory, deltaAngle2D(Torso, UpperLeg_L) - UpperLegMemory_L, true, upperLegTrack);
            updateBone(LeftLowerLegBone, LeftLowerLegBoneMemory, deltaAngle2D(UpperLeg_L, LowerLeg_L) - LowerLegMemory_L, true, lowerLegTrack);
        }

        protected void updateBone(Transform bone, Quaternion boneMemory, float deltaAngle, bool rightSide, bool track)
        {
            if (track == false) return;
            // reset
            bone.localRotation = boneMemory;
            if (rightSide)
            {
                bone.Rotate(Vector3.forward, deltaAngle, Space.World);
            }
            else
            {
                bone.Rotate(Vector3.back, deltaAngle, Space.World);
            }
        }
    }
}